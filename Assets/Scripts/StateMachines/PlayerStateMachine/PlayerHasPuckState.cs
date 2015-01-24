using UnityEngine;
using System.Collections;

public class PlayerHasPuckState : FSMState<Player> {

	float enterTime = Mathf.Infinity;
	public override void Enter(Player p)
	{
		enterTime = Time.time;
	}
	
	public override void Execute(Player p)
	{
		if(Puck.puck.controllingPlayer != p)
		{
			p.ChangeState(p.returnState);
			return;
		}
		p.LookForShot();
		if(p.IsThreatened() && (Time.time - enterTime) > 0.5f)
		{
			p.FindPass();
		}
		else if(!p.isAtDestination())
		{
			p.rigidbody2D.AddForce(p.steering.Arrive(p.destinationPosition, SteeringBehavior.Deceleration.normal));
			return;
		}
	}
	
	public override void Exit(Player p)
	{
		enterTime = Mathf.Infinity;
	}
	
}
