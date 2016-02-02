using UnityEngine;
using System.Collections;

public class TeamAttackingState : FSMState<TeamAI> {

	public override void Enter(TeamAI t)
	{
		t.SetHomePositions(t.mOffensivePositions);
	}

	public override void Execute(TeamAI t)
	{
		//if(Mathf.Sign(Puck.puck.transform.position.x)*(-(int)t.mTeam.side) < 0 && Puck.puck.controllingPlayer == null && Puck.puck.lastControllingPlayer != null && Puck.puck.lastControllingPlayer.team != t)
		//{
		//	t.ChangeState(t.defendState);
		//}

		//calculate best support spot
		if(Puck.Instance.controllingPlayer != null && !t.IsOnTeam(Puck.Instance.controllingPlayer))
		{
			t.ChangeState(t.defendState);
		}
	}

	public override void Exit(TeamAI t)
	{
		
	}


}
