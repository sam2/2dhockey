using UnityEngine;
using System.Collections;

public class TeamDefendingState : FSMState<TeamAI> {
	
	public override void Enter(TeamAI t)
	{
		//t.SetHomePositions(t.mDefensivePositions);
		//t.SetDestinationPositionsToHome();
		//if a player is in either the Wait or ReturnToHomeRegion states, its
		//steering target must be updated to that of its new home region
		//team->UpdateTargetsOfWaitingPlayers();
		foreach(PlayerAI p in t.mPlayerAIs)
		{
			p.mTeamState = PlayerAI.TeamState.Defending;
		}
		t.SetHomePositions(t.mDefensivePositions);
	}
	
	public override void Execute(TeamAI t)
	{
		//if((Mathf.Sign(Puck.puck.transform.position.x)*(-(int)t.mTeam.side) > 0) && Puck.puck.controllingPlayer == null)
		//{
		//	t.ChangeState(t.attackState);
		//}
		if(t.InControl())
		{
			t.ChangeState(t.attackState);
		}
	}
	
	public override void Exit(TeamAI t)
	{
		
	}
	
	
}