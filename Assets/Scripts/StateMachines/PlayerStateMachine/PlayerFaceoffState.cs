using UnityEngine;
using System.Collections;

public class PlayerFaceoffState : FSMState<Player> {
	
	
	public override void Enter(Player p)
	{
	
		p.gameObject.layer = 8; //fallen layer
		p.GetComponent<Collider2D>().enabled = false;
		p.GetComponent<Collider2D>().enabled = true;
	}
	
	public override void Execute(Player p)
	{

		p.GetComponent<Rigidbody2D>().AddForce(p.steering.Arrive(p.destinationPosition, SteeringBehavior.Deceleration.fast)*2f);


	}
	
	public override void Exit(Player p)
	{
		p.gameObject.layer = 0; //fallen layer
		p.GetComponent<Collider2D>().enabled = false;
		p.GetComponent<Collider2D>().enabled = true;
	}
	
}
