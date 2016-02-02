using UnityEngine;
using System.Collections;

public class PlayerControlledState : FSMState<Skater> {
	
	float enterTime = Mathf.Infinity;
	public override void Enter(Skater p)
	{
		enterTime = Time.time;
		p.controlled = true;
		Debug.Log("entering controlled state");
	}
	
	public override void Execute(Skater p)
	{


		if(Time.time > enterTime + 5)
			p.ChangeState(p.playState);


		
	}

	//exit called by playerControls
	public override void Exit(Skater p)
	{
		enterTime = Mathf.Infinity;
		p.controlled = false;
		Debug.Log("exiting controlled state");
	}
	
}