using UnityEngine;
using System.Collections;

public class PlayerPlayState : FSMState<Player> {

	PlayerAI pAI;
	int i = 0;
	int mNumFramesToSkip;
	public override void Enter(Player p)
	{
		pAI = p.GetComponent<PlayerAI>();
		mNumFramesToSkip = Random.Range(15,30);
	}
	
	public override void Execute(Player p)
	{
		p.rigidbody2D.AddForce(p.steering.Seek(p.destinationPosition));
		i++;
		pAI.UpdateAI();



		/*
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

		if(!p.isAtDestination())
		{
			p.ChangeState(p.returnState);
		}
		*/

	}
	
	public override void Exit(Player p)
	{
		
	}
	
	
}
