using UnityEngine;
using System.Collections;

public class TeamFaceoffState : FSMState<Team> {
	
	public override void Enter(Team t)
	{
		t.mControllingPlayer = null;
		t.mSupportingPlayer = null;
		t.mReceivingPlayer = null;
		t.mPlayerClosestToPuck = null;
		t.SetHomePositions(t.mDefensivePositions);
		t.SetDestinationPositionsToHome();
	}
	
	public override void Execute(Team t)
	{
		//if all players at home
		t.ChangeState(t.defendState);
	}
	
	public override void Exit(Team t)
	{
		
	}
	
	
}