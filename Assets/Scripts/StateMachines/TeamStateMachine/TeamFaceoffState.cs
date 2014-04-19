using UnityEngine;
using System.Collections;

public class TeamFaceoffState : FSMState<Team> {
	
	public override void Enter(Team t)
	{
		t.SetControllable(false);
		t.mControllingPlayer = null;
		t.mSupportingPlayer = null;
		t.mReceivingPlayer = null;
		t.mPlayerClosestToPuck = null;
		t.SetHomePositions(t.mFaceoffPositions);
		t.SetDestinationPositionsToHome(true);
		foreach(Player p in t.mPlayers)
		{
			p.ChangeState(p.faceoffState);
		}

	}
	
	public override void Execute(Team t)
	{

	}
	
	public override void Exit(Team t)
	{
		foreach(Player p in t.mPlayers)
		{
			p.ChangeState(p.returnState);
		}
		if(!t.AI)
			t.SetControllable(true);
	
	}
	
	
}