using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSequence : MonoBehaviour {

    //-------------------------------------
    //statemachine
    //-------------------------------------

    public enum State
    {
        Init,
        Routine,
        Pause,

    }
    Statemachine<State> _statemachine = new Statemachine<State>();

    //-------------------------------------
    // property
    //-------------------------------------

    private int        _time = 0;//時間（ステップ数）
    private PlayerData _player = new PlayerData(); //リソース
    public GameDate     Date { get; set; }    //時間(年 月(12) 週(4) 時間(24))
    //-------------------------------------
    //states
    //-------------------------------------

    void Awake()
    {
        _statemachine.Init(this);
    }

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

            AdvanceTime(); //時間を進める
            yield return null;
        }
    }
    IEnumerator Pause()
    {
        yield return null;
    }

    //-------------------------------------
    //function
    //-------------------------------------


    void AdvanceTime()
    {
        _time++;
        Date.Advance(1);
    }

	
	// Update is called once per frame
	void Update ()
	{
	    _statemachine.Update();
	}
    public void OnTapJob(){
        _player.Monay += 1;
    }

    public void OnTapQuest()
    {

    }

    /// <summary>
    /// debug
    /// </summary>
    private Rect rect = new Rect(0, 0, Screen.width, Screen.height);

    void OnGUI()
    {

        rect = GUILayout.Window(0, rect, Debug, "Game Debug");
    }

    private Vector2 btnscr;
    private Vector2 lblscr;
    void Debug(int id)
    {
        
        lblscr = GUILayout.BeginScrollView(lblscr, GUILayout.Height(100));
        GUILayout.BeginHorizontal(GUILayout.Height(50));

        DebugLabel(Date._year.ToString(), "年");
        DebugLabel(Date._month.Value.ToString(), "月");
        DebugLabel(Date._week.Value.ToString(),"週");
        DebugLabel(Date._hour.Value.ToString(),"時");
        DebugLabel("所持金", _player.Monay.ToString());
        DebugLabel("ポイント", _player.Point.ToString());
        DebugLabel("ランク", _player.Rank.ToString());
        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();
        btnscr = GUILayout.BeginScrollView(btnscr, GUILayout.Height(80));
        GUILayout.BeginHorizontal();
        DebugButton("内職", OnTapJob);
        DebugButton("採用", OnTapQuest);
        DebugButton("クエスト", OnTapQuest);


        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();
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
        if (GUILayout.Button(label, GUILayout.Width(80), GUILayout.Height(40)))
        {
            action();
        }
    }
}


