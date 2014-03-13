using UnityEngine;
using System.Collections;

public class PlayerFallenState : FSMState<Player> {


	public override void Enter(Player p)
	{
		p.fallen = true;
		p.gameObject.layer = 8; //fallen layer
		//p.collider2D.enabled = false;
		p.StartCoroutine(Fall (p));
	}
	
	public override void Execute(Player p)
	{
		//p.collider2D.enabled = false;

	}
	
	public override void Exit(Player p)
	{
		p.fallen = false;
		p.gameObject.layer = 0; 
		//p.collider2D.enabled = true;
	}

	public IEnumerator Fall(Player p)
	{
		yield return new WaitForSeconds(p.checkedTime);
		p.ChangeState(p.waitState);
	}
	
	
}
