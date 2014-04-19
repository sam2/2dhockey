using UnityEngine;
using System.Collections;

public class TeamDefendingState : FSMState<Team> {
	
	public override void Enter(Team t)
	{
		t.SetHomePositions(t.mDefensivePositions);
		t.SetDestinationPositionsToHome(t.AI);
		//if a player is in either the Wait or ReturnToHomeRegion states, its
		//steering target must be updated to that of its new home region
		//team->UpdateTargetsOfWaitingPlayers();
	}
	
	public override void Execute(Team t)
	{
		if(t.InControl())
		{
			t.ChangeState(t.attackState);
		}
	}
	
	public override void Exit(Team t)
	{
		
	}
	
	
}