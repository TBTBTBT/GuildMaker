using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //
    IEnumerator Init()
    {
        yield return null;
    }

    IEnumerator Routine()
    {
        //時間を進める
        //
        yield return null;
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
}
