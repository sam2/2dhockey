using UnityEngine;
using System.Collections;

public class GMFaceoffState : FSMState<GameManager> {

	public override void Enter(GameManager gm)
	{

		gm.teamA.ChangeState(gm.teamA.faceoffState);
		gm.teamB.ChangeState(gm.teamB.faceoffState);

	}
	
	public override void Execute(GameManager gm)
	{
		if(gm.teamA.TeamIsAtDest() && gm.teamB.TeamIsAtDest())
		{
			gm.teamA.ChangeState(gm.teamA.attackState);
			gm.teamB.ChangeState(gm.teamB.attackState);
			gm.ChangeState(gm.gmPlayState);
		}
	
	}
	
	public override void Exit(GameManager gm)
	{	
		gm.view.goalText.gameObject.SetActive(false);
	}
	
}
