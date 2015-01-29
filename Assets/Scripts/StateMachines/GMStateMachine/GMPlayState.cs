using UnityEngine;
using System.Collections;

public class GMPlayState : FSMState<GameManager> {
	
	
	public override void Enter(GameManager gm)
	{
		Puck.Instance.InPlay(true);
	}
	
	public override void Execute(GameManager gm)
	{

		gm.timeLeft -= Time.deltaTime;
		if(gm.timeLeft <= 0)
		{
			gm.ChangeState(gm.gmEndGameState);
		}
		gm.view.UpdateTimer(gm.timeLeft);
		
	}
	
	public override void Exit(GameManager gm)
	{
	
	}
	
}
