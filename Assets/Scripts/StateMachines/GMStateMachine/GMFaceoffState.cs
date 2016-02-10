using UnityEngine;
using System.Collections;

public class GMFaceoffState : FSMState<GameManager> {

	public override void Enter(GameManager gm)
	{
        gm.FaceoffInProgress = true;
        gm.TeamA.ChangeState(gm.TeamA.faceoffState);
        gm.TeamB.ChangeState(gm.TeamB.faceoffState);

    }
	
	public override void Execute(GameManager gm)
	{
		if(gm.TeamA.TeamIsAtDest() && gm.TeamB.TeamIsAtDest())
		{
            gm.ChangeState(gm.gmPlayState);
		}
	
	}
	
	public override void Exit(GameManager gm)
	{	
		gm.View.goalText.gameObject.SetActive(false);
        Puck.Instance.InPlay(true);
        gm.FaceoffInProgress = false;
    }
	
}
