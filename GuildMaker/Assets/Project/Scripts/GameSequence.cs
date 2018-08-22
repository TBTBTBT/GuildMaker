using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSequence : MonoBehaviour {
    public enum State
    {
        Init,
        Routine,
        Pause,


    }
    Statemachine<State> _statemachine = new Statemachine<State>();
    // property

    //時間(年 月(12) 週(4) 時間(24))

    private int _time = 0;
    public GameDate Date { get; set; }
    //リソース
    private int _monay = 0;
    //雇用した人
    //_employee

    //
    IEnumerator Init()
    {
        Date = new GameDate(1,1,1,0);
        _statemachine.Next(State.Routine);
        yield return null;
    }

    IEnumerator Routine()
    {
        while (true)
        {
            //時間を進める
            AdvanceTime();

            //
            yield return null;
        }
    }

    void AdvanceTime()
    {
        _time++;
        Date.Advance(1);


        //LogViewer.Instance.Add(_time.ToString());
    }
    IEnumerator Pause()
    {
        yield return null;
    }
	void Awake () {
		_statemachine.Init(this);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _statemachine.Update();
	}
    public void OnTapJob(){
        _monay += 1;
    }

    public void OnTapQuest()
    {

    }
    /// <summary>
    /// debug
    /// </summary>

    void OnGUI()
    {

        Debug(new Rect(0,300,Screen.width,200));
    }

    private Vector2 scr;
    void Debug(Rect area)
    {
        GUILayout.BeginArea(area);

        scr = GUILayout.BeginScrollView(scr);
        GUILayout.BeginHorizontal(GUILayout.Height(50));
        DebugLabel(Date._year.ToString(), "年");
        DebugLabel(Date._month.Value.ToString(), "月");
        DebugLabel(Date._week.Value.ToString(),"週");
        DebugLabel(Date._hour.Value.ToString(),"時");
        DebugLabel("所持金", _monay.ToString());
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        DebugButton("内職", OnTapJob);
        DebugButton("採用", OnTapQuest);
        DebugButton("クエスト", OnTapQuest);
        DebugButton("クエスト", OnTapQuest);
        DebugButton("クエスト", OnTapQuest);
        DebugButton("クエスト", OnTapQuest);
        DebugButton("クエスト", OnTapQuest);
        DebugButton("クエスト", OnTapQuest);
        DebugButton("クエスト", OnTapQuest);

        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    void DebugLabel(params string[] str)
    {
        GUILayout.BeginVertical("box",GUILayout.Width(50));
        foreach (var s in str)
        {
            GUILayout.Box(s);
        }
        GUILayout.EndVertical();
    }
    void DebugButton(string label,UnityAction action)
    {
        if (GUILayout.Button(label))
        {
            action();
        }
    }
}



//ゲーム内時間



public class GameDate{
    public class LoopRange{
        //min以上 max未満
        int _min;
        int _max;
       
        public int Value { get; set; }

        public LoopRange(int min,int max){
            _min = min;
            _max = max;
            Value = _min;

        }
        public int Advance(int next){
            int _isLoop = 0;
            Value += next;
            if ( Value >= _max)
            {
                Value = _min + (Value - _max);
                _isLoop = 1;
            }
            if (Value< _min)
            {
                Value= _max + (Value - _min);
                _isLoop = -1;
            }
            return _isLoop;
        }
    }
    public int _year { get; set; }
    public LoopRange _month { get; set; }
    public LoopRange _week { get; set; }
    public LoopRange _hour { get; set; }
    public GameDate(int y = 1, int m = 1, int w = 1,int h = 0){
        InitDate(y, m, w, h);
    }
    void InitDate(int y , int m , int w , int h ){
        _year = y;
        _month = new LoopRange(1, 13);
        _month.Value = m;
        _week = new LoopRange(1, 5);
        _week.Value = w;
        _hour = new LoopRange(0, 24);
        _hour.Value = h;
    }
    //時間を進める next時間
    public void Advance(int next = 1){
        _year += _month.Advance(_week.Advance(_hour.Advance(next)));
    }
}