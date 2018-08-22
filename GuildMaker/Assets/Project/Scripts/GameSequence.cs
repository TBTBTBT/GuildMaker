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

    //時間(月 週 時間)
    private int _time = 0;
    //
    IEnumerator Init()
    {
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
        LogViewer.Instance.Add(_time.ToString());
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


    public void OnTapQuest()
    {

    }
    /// <summary>
    /// debug
    /// </summary>

    void OnGUI()
    {

        Debug(new Rect(0,300,300,200));
    }

    private Vector2 scr;
    void Debug(Rect area)
    {
        GUILayout.BeginArea(area);

        scr = GUILayout.BeginScrollView(scr);
        GUILayout.BeginHorizontal();
        DebugLabel("所持金", "0");
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        DebugButton("クエスト", OnTapQuest);
        DebugButton("クエスト", OnTapQuest);
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
        GUILayout.BeginVertical();
        foreach (var s in str)
        {
            GUILayout.Label(s);
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
