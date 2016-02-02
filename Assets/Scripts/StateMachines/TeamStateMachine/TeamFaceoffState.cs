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
	
		t.SetHomePositions(t.mDefensivePositions);
		t.SetDestToHomePositions();
		foreach(Skater p in t.mTeam.mPlayers)
		{
			p.ChangeState(p.faceoffState);
		}

	}
	
	public override void Execute(TeamAI t)
	{

	}
	
	public override void Exit(TeamAI t)
	{
		foreach(Skater p in t.mTeam.mPlayers)
		{
			p.ChangeState(p.playState);
		}
		PlayerControls tc = t.GetComponent<PlayerControls>();
		if(tc!=null)
		{
			tc.enabled = true;
		}
	
	}
	
	
}