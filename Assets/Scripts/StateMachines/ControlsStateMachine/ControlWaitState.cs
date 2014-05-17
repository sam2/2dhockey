using UnityEngine;
using System.Collections;

public class ControlWaitState : FSMState<PlayerControls> {
	
	
	public override void Enter(PlayerControls c)
	{
		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02f*Time.timeScale;
	}
	
	public override void Execute(PlayerControls c)
	{
		
		
	}
	
	public override void Exit(PlayerControls c)
	{
		
	}
	
}
