using UnityEngine;
using System.Collections;

public class PlayerHasPuckState : FSMState<Player> {
	
	
	public override void Enter(Player p)
	{
		if(!p.team.AI)
		{
			p.ChangeState(p.controlledState);
		}
	}
	
	public override void Execute(Player p)
	{
		if(!p.team.AI)
		{
			Debug.LogError(p.name+": human player in AI state");
		}
		if(Puck.puck.controllingPlayer != p)
		{
			p.ChangeState(p.returnState);
			return;
		}
		p.LookForShot();
		if(p.IsThreatened())
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
		
	}
	
}
