using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TimePauseState : FSMState<TimeManager> {
	
	float enterTime= Mathf.Infinity;
	public override void Enter(TimeManager t)
	{
		enterTime = Time.realtimeSinceStartup;
		t.paused = true;
		Time.timeScale = 0.25f;
		Time.fixedDeltaTime = 0.02f*Time.timeScale;
	}
	
	public override void Execute(TimeManager t)
	{
		if((enterTime+5) < Time.realtimeSinceStartup)
		{
			t.ChangeState(t.normalState);
			
		}	
		
	}
	
	public override void Exit(TimeManager t)
	{
		enterTime = Mathf.Infinity;
		t.paused = false;

	

		Debug.Log("exit pause");
	}
	
	
}
