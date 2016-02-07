using UnityEngine;
using System.Collections;

public class GMFaceoffState : FSMState<GameManager> {

	public override void Enter(GameManager gm)
	{

        gm.TeamA.ChangeState(gm.TeamA.faceoffState);
        gm.TeamB.ChangeState(gm.TeamB.faceoffState);

    }
	
	public override void Execute(GameManager gm)
	{
		if(gm.TeamA.TeamIsAtDest() && gm.TeamB.TeamIsAtDest())
		{
            gm.TeamA.ChangeState(gm.TeamA.defendState);
            gm.TeamB.ChangeState(gm.TeamB.attackState);
            gm.ChangeState(gm.gmPlayState);
		}
	
	}
	
	public override void Exit(GameManager gm)
	{	
		gm.view.goalText.gameObject.SetActive(false);
	}
	
}
