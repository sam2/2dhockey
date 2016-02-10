using UnityEngine;
using System.Collections;

public class TeamAttackingState : FSMState<TeamAI> {

	public override void Enter(TeamAI t)
	{
		
	}

	public override void Execute(TeamAI t)
	{
        //if(Mathf.Sign(Puck.puck.transform.position.x)*(-(int)t.mTeam.side) < 0 && Puck.puck.controllingPlayer == null && Puck.puck.lastControllingPlayer != null && Puck.puck.lastControllingPlayer.team != t)
        //{
        //	t.ChangeState(t.defendState);
        //}

        //calculate best support spot
        for(int i = 0; i < t.mPlayerAIs.Count; i++)
        {
            if (!t.Team.mPlayers[i].Controlled)
                t.Team.mPlayers[i].MoveTo(t.CalculateOffensivePosition(i));
        }
        if (Puck.Instance.controllingPlayer != null && !t.IsOnTeam(Puck.Instance.controllingPlayer))
		{
			t.ChangeState(t.defendState);
		}
	}

	public override void Exit(TeamAI t)
	{
		
	}


}
