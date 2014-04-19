using UnityEngine;
using System.Collections;

public class PlayerReturnState : FSMState<Player> {
	
	
	public override void Enter(Player p)
	{
		p.destinationPosition = p.mHomePosition;
	}
	
	public override void Execute(Player p)
	{
		if(p.HasPossession())
		{
			p.ChangeState(p.hasPuckState);
			return;
		}

		if(p.team.mPlayerClosestToPuck == p)
		{
			p.ChangeState(p.chaseState);
			return;
		}

		else if(p.isAtDestination())
		{
			p.ChangeState(p.waitState);
			return;
		}
		p.rigidbody2D.AddForce(p.steering.Arrive(p.destinationPosition, SteeringBehavior.Deceleration.normal));

		
	}
	
	public override void Exit(Player p)
	{
		
	}
	
}
