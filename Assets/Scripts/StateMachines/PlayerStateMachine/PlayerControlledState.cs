using UnityEngine;
using System.Collections;

public class PlayerControlledState : FSMState<Player> {
	
	float enterTime = Mathf.Infinity;
	public override void Enter(Player p)
	{
		p.controlled = true;
	}
	
	public override void Execute(Player p)
	{
		/*
		if(Puck.puck.controllingPlayer != p)
		{
			if(p.isAtDestination() && enterTime == Mathf.Infinity)
			{
				enterTime = Time.time;
			}
			if(Time.time - enterTime >= p.manualControlTime)
			{
				p.ChangeState(p.returnState);
				return;
			}
		}
		*/
		p.controls.UpdatePath();
		if(p.team.AI)
		{
			Debug.LogError(p.name+": AI player in controlled state");
		}

		p.rigidbody2D.AddForce(p.steering.Seek(p.destinationPosition));

		
	}

	//exit called by playerControls
	public override void Exit(Player p)
	{
		p.controlled = false;
		p.controls.view.ClearLineRenderer();
		p.controls.path.Clear();
		enterTime = Mathf.Infinity;
	}
	
}