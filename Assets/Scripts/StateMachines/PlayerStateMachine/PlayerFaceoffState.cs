using UnityEngine;
using System.Collections;

public class PlayerFaceoffState : FSMState<Skater> {
	
	
	public override void Enter(Skater p)
	{
	
		p.gameObject.layer = 8; //fallen layer
		p.GetComponent<Collider2D>().enabled = false;
		p.GetComponent<Collider2D>().enabled = true;
	}
	
	public override void Execute(Skater p)
	{

		


	}
	
	public override void Exit(Skater p)
	{
		p.gameObject.layer = 0; //fallen layer
		p.GetComponent<Collider2D>().enabled = false;
		p.GetComponent<Collider2D>().enabled = true;
	}
	
}
