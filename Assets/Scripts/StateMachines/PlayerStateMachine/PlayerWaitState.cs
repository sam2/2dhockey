using UnityEngine;
using System.Collections;

public class PlayerWaitState : FSMState<Player> {

	public override void Enter(Player p)
	{

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

		if(!p.isAtDestination())
		{
			p.ChangeState(p.returnState);
		}

	}
	
	public override void Exit(Player p)
	{
		
	}
	
	
}
