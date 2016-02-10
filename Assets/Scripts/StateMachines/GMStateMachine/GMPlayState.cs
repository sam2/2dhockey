using UnityEngine;
using System.Collections;

public class GMPlayState : FSMState<GameManager> {
	
	
	public override void Enter(GameManager gm)
	{
		
	}
	
	public override void Execute(GameManager gm)
	{

        gm.UpdateGameTime();
		
	}
	
	public override void Exit(GameManager gm)
	{
	
	}
	
}
