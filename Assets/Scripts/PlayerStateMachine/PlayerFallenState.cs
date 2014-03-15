using UnityEngine;
using System.Collections;

public class PlayerFallenState : FSMState<Player> {


	public override void Enter(Player p)
	{
		p.fallen = true;
		p.gameObject.layer = 8; //fallen layer
		p.collider2D.enabled = false;
		p.collider2D.enabled = true;
		//p.collider2D.enabled = false;
		p.StartCoroutine(Fall (p));

		p.GetComponentInChildren<SpriteRenderer>().color = Color.gray;

	}
	
	public override void Execute(Player p)
	{
		p.gameObject.layer = 8; //fallen layer
		p.collider2D.enabled = false;
		p.collider2D.enabled = true;
	}
	
	public override void Exit(Player p)
	{
		p.fallen = false;
		p.gameObject.layer = 0;
		p.collider2D.enabled = false;
		p.collider2D.enabled = true;
		//p.collider2D.enabled = true;
		p.GetComponentInChildren<SpriteRenderer>().color = Color.white;

	}

	public IEnumerator Fall(Player p)
	{
		yield return new WaitForSeconds(p.checkedTime);
		p.ChangeState(p.waitState);
	}
	
	
}
