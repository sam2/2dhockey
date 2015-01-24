﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TimeSlowState : FSMState<TimeManager> {
	
	
	public override void Enter(TimeManager t)
	{
		Time.timeScale = .25f;
		Time.fixedDeltaTime = 0.02f*Time.timeScale;
	}
	
	public override void Execute(TimeManager t)
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			t.ChangeState(t.normalState);
			
		}
		
		
	}
	
	public override void Exit(TimeManager t)
	{
		
	}
	
	
}
