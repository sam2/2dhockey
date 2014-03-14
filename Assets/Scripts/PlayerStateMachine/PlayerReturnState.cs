using UnityEngine;
using System.Collections;

public class PlayerReturnState : FSMState<Player> {
	
	
	public override void Enter(Player p)
	{

	}
	
	public override void Execute(Player p)
	{
		p.LookForShot();
		if(Puck.puck.controllingPlayer == p )
		{

			if(p.IsThreatened())
			{
				//Player passTarget = p.FindPass();
				//if(passTarget != null)
				//{
					//Puck.puck.Shoot((passTarget.transform.position - Puck.puck.transform.position).normalized*p.shotPower/1.5f);
				//}
				p.FindPass();
			}
		}


		if(p.team.mPlayerClosestToPuck == p && p.team.mReceivingPlayer == null && Puck.puck.controllingPlayer != p)
		{
			p.ChangeState(p.chaseState);
			return;
		}

		if(!p.isAtHomePosition())
		{
			p.destinationPosition = p.mHomePosition;
			p.rigidbody2D.AddForce(p.steering.Arrive(p.destinationPosition, SteeringBehavior.Deceleration.normal));
			return;
		}
		
	}
	
	public override void Exit(Player p)
	{
		
	}
	
}
