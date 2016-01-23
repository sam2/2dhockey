using UnityEngine;
using System.Collections;

public class PlayerControlledState : FSMState<Player> {
	
	float enterTime = Mathf.Infinity;
	public override void Enter(Player p)
	{
		enterTime = Time.time;
		p.controlled = true;
		Debug.Log("entering controlled state");
	}
	
	public override void Execute(Player p)
	{


		if(Time.time > enterTime + 5)
			p.ChangeState(p.playState);


		p.GetComponent<Rigidbody2D>().AddForce(p.steering.Seek(p.destinationPosition));

		
	}

	//exit called by playerControls
	public override void Exit(Player p)
	{
		enterTime = Mathf.Infinity;
		p.controlled = false;
		Debug.Log("exiting controlled state");
	}
	
}