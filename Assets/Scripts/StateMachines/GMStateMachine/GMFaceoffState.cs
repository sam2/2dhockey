using UnityEngine;
using System.Collections;

public class GMFaceoffState : FSMState<GameManager> {

	public override void Enter(GameManager gm)
	{

		gm.teamA.GoToFaceoff();
		gm.teamB.GoToFaceoff();

	}
	
	public override void Execute(GameManager gm)
	{
		if(gm.teamA.IsReady() && gm.teamB.IsReady())
		{
			gm.teamA.DropPuck();
			gm.teamB.DropPuck();
			gm.ChangeState(gm.gmPlayState);
		}
	
	}
	
	public override void Exit(GameManager gm)
	{	
		gm.view.goalText.gameObject.SetActive(false);
	}
	
}
