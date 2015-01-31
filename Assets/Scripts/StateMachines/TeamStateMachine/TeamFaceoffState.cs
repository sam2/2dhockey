using UnityEngine;
using System.Collections;

public class TeamFaceoffState : FSMState<TeamAI> {
	
	public override void Enter(TeamAI t)
	{
		TouchControls tc = t.GetComponent<TouchControls>();
		if(tc!=null)
		{
			tc.Toggle(false);
		}
	
		t.SetHomePositions(t.mDefensivePositions);
		t.SetDestToHomePositions();
		foreach(Player p in t.mTeam.mPlayers)
		{
			p.ChangeState(p.faceoffState);
		}

	}
	
	public override void Execute(TeamAI t)
	{

	}
	
	public override void Exit(TeamAI t)
	{
		foreach(Player p in t.mTeam.mPlayers)
		{
			p.ChangeState(p.playState);
		}
		TouchControls tc = t.GetComponent<TouchControls>();
		if(tc!=null)
		{
			tc.Toggle(true);
		}
	
	}
	
	
}