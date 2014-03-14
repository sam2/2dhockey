using UnityEngine;
using System.Collections;

public class PlayerWaitState : FSMState<Player> {

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

		if(p.team.mPlayerClosestToPuck == p && p.team.mReceivingPlayer == null)
		{
			p.ChangeState(p.chaseState);
			return;
		}

		if(!p.isAtDestination())
		{
			p.rigidbody2D.AddForce(p.steering.Arrive(p.destinationPosition, SteeringBehavior.Deceleration.normal));
			return;
		}

		else
		{
			p.rigidbody2D.velocity = Vector2.zero;
			//player.trackball;
		}

		//if this player's team is controlling AND this player is not the attacker
		/*AND is farther up the field than the attacker he should request a pass.
		if ( player->Team()->InControl() &&
		    (!player->isControllingPlayer()) &&
		    player->isAheadOfAttacker() )
		{
			player->Team()->RequestPass(player);
			return;
		}
		*/





		//TEMP
		if(p.team.InControl() && Puck.puck.controllingPlayer != p )
		   p.ChangeState(p.returnState);
		//
		
	}
	
	public override void Exit(Player p)
	{
		
	}
	
	
}
