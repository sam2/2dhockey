using UnityEngine;
using System.Collections;

public class GMFaceoffState : FSMState<GameManager> {

	public override void Enter(GameManager gm)
	{

		gm.TeamA.GoToFaceoff();
		gm.TeamB.GoToFaceoff();

	}
	
	public override void Execute(GameManager gm)
	{
		if(gm.TeamA.IsReady() && gm.TeamB.IsReady())
		{
			gm.TeamA.DropPuck();
			gm.TeamB.DropPuck();
			gm.ChangeState(gm.gmPlayState);
		}
	
	}
	
	public override void Exit(GameManager gm)
	{	
		gm.view.goalText.gameObject.SetActive(false);
	}
	
}
