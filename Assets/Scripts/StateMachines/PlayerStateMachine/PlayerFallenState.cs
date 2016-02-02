using UnityEngine;
using System.Collections;

public class PlayerFallenState : FSMState<Skater> {


	public override void Enter(Skater p)
	{
		p.fallen = true;
		p.gameObject.layer = 8; //fallen layer
		p.GetComponent<Collider2D>().enabled = false;
		p.GetComponent<Collider2D>().enabled = true;
		//p.collider2D.enabled = false;
		p.StartCoroutine(Fall (p));

		//p.GetComponentInChildren<SpriteRenderer>().color = Color.gray;

	}
	
	public override void Execute(Skater p)
	{
		p.gameObject.layer = 8; //fallen layer
		p.GetComponent<Collider2D>().enabled = false;
		p.GetComponent<Collider2D>().enabled = true;
	}
	
	public override void Exit(Skater p)
	{
		p.fallen = false;
		p.gameObject.layer = 0;
		p.GetComponent<Collider2D>().enabled = false;
		p.GetComponent<Collider2D>().enabled = true;
		//p.collider2D.enabled = true;
		//p.GetComponentInChildren<SpriteRenderer>().color = Color.white;

	}

	public IEnumerator Fall(Skater p)
	{
		yield return new WaitForSeconds(p.CHECKED_TIME);
		p.ChangeState(p.playState);
	}
	
	
}
