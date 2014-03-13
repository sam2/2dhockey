using UnityEngine;
using System.Collections;

public class PlayerChasePuckState : FSMState<Player> {
	
	
	public override void Enter(Player p)
	{
		
	}
	
	public override void Execute(Player p)
	{
		//if the ball is within kicking range the player changes state to KickBall.
		//if (player->BallWithinKickingRange())
		//{
		//	player->ChangeState(player, KickBall::Instance());
		//	return;
		//}

		if(p.team.mPlayerClosestToPuck == p && Puck.puck.controllingPlayer != p)
		{
			p.destinationPosition = Puck.puck.transform.position;
			if(Puck.puck.controllingPlayer != null)
				p.rigidbody2D.AddForce(p.steering.Pursuit(Puck.puck.controllingPlayer.rigidbody2D));
			else
				p.rigidbody2D.AddForce(p.steering.Seek(p.destinationPosition));
			return;
		}

		p.ChangeState(p.returnState);

	}
	
	public override void Exit(Player p)
	{

	}
	

}
