using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSequence : MonoBehaviour
{

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

    private int _time = 0;//時間（ステップ数）

    private PlayerData _player = new PlayerData(); //リソース
    public GameDate Date { get; set; }    //時間(年 月(12) 週(4) 時間(24))
    public List<MstQuestData.DataFormat> PlayableQuest;//掲載されているクエスト 掲載数はギルドの信頼度に依存
    public List<Recruit> Recruit = new List<Recruit>(); //求人情報 ギルドの人気度に依存
    //-------------------------------------
    //states
    //-------------------------------------

    void Awake()
    {
        _statemachine.Init(this);
    }
    void Update()
    {
        _statemachine.Update();
    }
    IEnumerator Init()
    {
        Date = new GameDate(1, 1, 1, 0);
        Date._hour.OnLoop.AddListener(WeeklyEvent);
        Date._week.OnLoop.AddListener(MonthlyEvent);

        _statemachine.Next(State.Routine);
        yield return null;
    }

    IEnumerator Routine()
    {
        while (true)
        {

            AdvanceTime(); //時間を進める
            HourlyEvent();
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
    /// <summary>
    /// 時間ごとのイベント 月ごと
    /// </summary>
    void MonthlyEvent()
    {
        Debug.Log("monthly");
        //クエスト更新
    }
    /// <summary>
    /// 時間ごとのイベント 週ごと
    /// </summary>
    void WeeklyEvent()
    {
        Debug.Log("weekly");
        UpdateQuest();
        UpdateRecruit();

    }
    /// <summary>
    /// 時間ごとのイベント 時間ごと
    /// </summary>
    void HourlyEvent()
    {

    }

    void AdvanceTime()
    {
        _time++;
        Date.Advance(1);
    }
    /// <summary>
    /// クエスト掲載情報更新
    /// </summary>
    void UpdateQuest()
    {
        if (PlayableQuest.Count < 3)
        {
            PlayableQuest.Add(new MstQuestData.DataFormat() { Name = "TEST", Week = 4 });
        }

        foreach (var playable in PlayableQuest)
        {
            playable.Week--;
        }
        PlayableQuest.RemoveAll(q => q.Week < 0);
    }
    /// <summary>
    /// 求人情報更新
    /// </summary>
    void UpdateRecruit()
    {
        if (Recruit.Count < 3)
        {
            Recruit.Add(new Recruit() { Employee = new Employee() { Name = NameGenerator.Generate() ,Param = new ParamTable(){Salary = 10}} });
        }
        foreach (var rec in Recruit)
        {
            rec.Week--;
        }
        Recruit.RemoveAll(q => q.Week < 0);
    }
    /// <summary>
    /// 雇う
    /// </summary>
    void OnTapHire(Recruit recruit)
    {
        if(_player.Monay > recruit.Employee.Param.Salary){
            _player.Monay -= recruit.Employee.Param.Salary;
            _player.Employees.Add(recruit.Employee);
            Recruit.Remove(recruit);
        }

    }
    /// <summary>
    /// 内職する
    /// </summary>
    public void OnTapJob()
    {
        _player.Monay += 1;
    }
    /// <summary>
    /// クエストを確認する
    /// </summary>
    public void OnTapQuest()
    {
        _statemachine.Next(State.Pause);
    }
    /// <summary>
    /// メンバーを確認
    /// </summary>
    public void OntapEmployee()
    {
        //情報ダイアログと行動ボタン
        // _player.Employees.Add(new Employee);
    }




    //-------------------------------------------------------------------------------

    /// <summary>
    /// debug
    /// </summary>
   // private GUIDebug _debug;
    private Rect rect = new Rect(0, 0, Screen.width, Screen.height);

    void OnGUI()
    {

        rect = GUILayout.Window(0, rect, DebugView, "Game Debug");
    }

    private Vector2 btnscr;
    private Vector2 qstscr;
    private Vector2 eplscr;
    private Vector2 lblscr;
    void DebugView(int id)
    {
        PropertyView();
        EmployeeView();
        RecruitView();
        QuestView();
        ButtonView();

    }

    void PropertyView()
    {
        lblscr = GUILayout.BeginScrollView(lblscr, GUILayout.Height(100));
        GUILayout.BeginHorizontal(GUILayout.Height(50));

        DebugLabel(Date._year.ToString(), "年");
        DebugLabel(Date._month.Value.ToString(), "月");
        DebugLabel(Date._week.Value.ToString(), "週");
        DebugLabel(Date._hour.Value.ToString(), "時");
        DebugLabel("所持金", _player.Monay.ToString());
        DebugLabel("ポイント", _player.Point.ToString());
        DebugLabel("ランク", _player.Rank.ToString());
        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();
    }

    void EmployeeView()
    {
        GUILayout.Label("Employee");
        qstscr = GUILayout.BeginScrollView(qstscr, GUILayout.Height(150));
        GUILayout.BeginVertical();
        foreach (var e in _player.Employees)
        {
            DebugButton(e.Name + " , ", OntapEmployee);
        }
        GUILayout.EndVertical();
        GUILayout.EndScrollView();
    }
    void QuestView()
    {
        GUILayout.Label("QUEST");
        qstscr = GUILayout.BeginScrollView(qstscr, GUILayout.Height(150));
        GUILayout.BeginVertical();
        foreach (var quest in PlayableQuest)
        {
            DebugButton(quest.Name + " , " + quest.Week, OnTapQuest);
        }
        GUILayout.EndVertical();
        GUILayout.EndScrollView();
    }
    void RecruitView()
    {
        GUILayout.Label("Recruit");
        eplscr = GUILayout.BeginScrollView(eplscr, GUILayout.Height(150));
        GUILayout.BeginVertical();
        foreach (var r in Recruit.ToArray())
        {//この中で削除するので複製
            DebugButton(r.Employee.Name + " , ", ()=>OnTapHire(r));
        }
        GUILayout.EndVertical();
        GUILayout.EndScrollView();
    }

    void ButtonView(){
        qstscr = GUILayout.BeginScrollView(qstscr, GUILayout.Height(80));
        GUILayout.BeginHorizontal();
        DebugButton("内職", OnTapJob);
        DebugButton("採用", OnTapQuest);
        DebugButton("クエスト", OnTapQuest);
        DebugButton("訓練", OnTapQuest);
        DebugButton("闘技場", OnTapQuest);
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


