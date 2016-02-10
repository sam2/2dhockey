using UnityEngine;
using System.Collections;

public class TeamDefendingState : FSMState<TeamAI> {
	
	public override void Enter(TeamAI t)
	{
		
	}
	
	public override void Execute(TeamAI t)
	{
        //if((Mathf.Sign(Puck.puck.transform.position.x)*(-(int)t.mTeam.side) > 0) && Puck.puck.controllingPlayer == null)
        //{
        //	t.ChangeState(t.attackState);
        //}

        for(int i = 0; i < t.mPlayerAIs.Count; i++)
        {
            if(!t.Team.mPlayers[i].Controlled)
                t.Team.mPlayers[i].MoveTo(t.CalculateDefensivePosition(i));
        }
		if(t.InControl())
		{
			t.ChangeState(t.attackState);
		}
	}
	
	public override void Exit(TeamAI t)
	{
		
	}
	
	
}