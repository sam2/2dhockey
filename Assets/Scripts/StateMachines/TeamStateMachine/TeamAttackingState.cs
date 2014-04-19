using UnityEngine;
using System.Collections;

public class TeamAttackingState : FSMState<Team> {

	public override void Enter(Team t)
	{
		t.SetHomePositions(t.mOffensivePositions);
		t.SetDestinationPositionsToHome(t.AI);
	}

	public override void Execute(Team t)
	{
		if(!t.InControl() && Puck.puck.controllingPlayer != null)
		{
			t.ChangeState(t.defendState);
		}

		//calculate best support spot
	}

	public override void Exit(Team t)
	{
		
	}


}
