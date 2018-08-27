using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForFrameCount : CustomYieldInstruction {
    int _count = 0;
	public override bool keepWaiting
	{
		get
		{
            return Next();
		}
	}
    bool Next(){
        _count--;
        return _count >= 0;
    }
    public WaitForFrameCount(int count){
        _count = count;
    }
}
