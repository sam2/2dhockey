﻿using UnityEngine;
using System.Collections;

public class TeamAttackingState : FSMState<Team> {

	public override void Enter(Team t)
	{
		t.SetHomePositions(t.mOffensivePositions);
		t.SetDestinationPositionsToHome(t.AI);
	}

	public override void Execute(Team t)
	{
		if(Mathf.Sign(Puck.puck.transform.position.x)*(-(int)t.side) < 0 && Puck.puck.controllingPlayer == null && Puck.puck.lastControllingPlayer != null && Puck.puck.lastControllingPlayer.team != t)
		{
			t.ChangeState(t.defendState);
		}

		//calculate best support spot
	}

	public override void Exit(Team t)
	{
		
	}


}
