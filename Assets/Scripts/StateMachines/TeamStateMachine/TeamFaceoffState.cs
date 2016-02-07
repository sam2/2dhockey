using UnityEngine;
using System.Collections;

public class TeamFaceoffState : FSMState<TeamAI> {
	
	public override void Enter(TeamAI t)
	{
		PlayerControls tc = t.GetComponent<PlayerControls>();
		if(tc!=null)
		{
			tc.enabled = false;
		}	

		foreach(Skater p in t.Team.mPlayers)
		{
			p.ChangeState(p.faceoffState);
		}
        foreach(PlayerAI pai in t.mPlayerAIs)
        {
            pai.enabled = false;
        }
        for (int i = 0; i < t.Team.mPlayers.Count; i++)
        {
            Debug.Log(t.Team.mPlayers[i].name + "->" + t.CalculateFaceOffPosition(i));
            t.Team.mPlayers[i].MoveTo(t.CalculateFaceOffPosition(i));
        }

    }
	
	public override void Execute(TeamAI t)
	{
        
    }
	
	public override void Exit(TeamAI t)
	{
		foreach(Skater p in t.Team.mPlayers)
		{
			p.ChangeState(p.playState);
		}
		PlayerControls tc = t.GetComponent<PlayerControls>();
		if(tc!=null)
		{
			tc.enabled = true;
		}
        foreach (PlayerAI pai in t.mPlayerAIs)
        {
            pai.enabled = true;
        }


    }


}