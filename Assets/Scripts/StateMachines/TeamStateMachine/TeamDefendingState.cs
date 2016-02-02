using UnityEngine;
using System.Collections;

public class TeamDefendingState : FSMState<TeamAI> {
	
	public override void Enter(TeamAI t)
	{
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