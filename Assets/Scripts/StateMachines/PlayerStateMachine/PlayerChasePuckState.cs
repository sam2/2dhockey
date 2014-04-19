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
		if(p.HasPossession())
		{
			p.ChangeState(p.hasPuckState);
			return;
		}

		if(p.team.mPlayerClosestToPuck == p)
		{
			p.destinationPosition = Puck.puck.transform.position;
			p.rigidbody2D.AddForce(p.steering.Seek(p.destinationPosition));
			return;
		}
		else
		{
			p.ChangeState(p.returnState);
		}

	}
	
	public override void Exit(Player p)
	{

	}
	

}
