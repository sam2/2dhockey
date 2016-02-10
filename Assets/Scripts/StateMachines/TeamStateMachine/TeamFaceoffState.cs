using UnityEngine;
using System.Collections;

public class TeamFaceoffState : FSMState<TeamAI> {
	
	public override void Enter(TeamAI t)
	{
        for (int i = 0; i < t.Team.mPlayers.Count; i++)
        {
            t.Team.mPlayers[i].ChangeState(t.Team.mPlayers[i].faceoffState);
            t.Team.mPlayers[i].MoveTo(t.CalculateFaceOffPosition(i));
        }

    }
	
	public override void Execute(TeamAI t)
	{
        if(!GameManager.Instance.FaceoffInProgress)
        {
            t.ChangeState(t.attackState);
        }
        
    }
	
	public override void Exit(TeamAI t)
	{
		foreach(Skater p in t.Team.mPlayers)
		{
			p.ChangeState(p.playState);
		}
    }


}