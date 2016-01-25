using UnityEngine;
using System.Collections;

public class PlayerPlayState : FSMState<Player> {

	PlayerAI pAI;
	public override void Enter(Player p)
	{
		pAI = p.GetComponent<PlayerAI>();
	}
	
	public override void Execute(Player p)
	{
		p.GetComponent<Rigidbody2D>().AddForce(p.steering.Seek(p.destinationPosition));
		pAI.UpdateAI();
	}
	
	public override void Exit(Player p)
	{
		
	}
	
	
}
