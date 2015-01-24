using UnityEngine;
using System.Collections;

public class ControlOneTimerState : FSMState<MousePlayerControls> {

	float enterTime = Mathf.Infinity;

	Vector2 shotVector;
	Vector2 aimPoint;
	
	public override void Enter(MousePlayerControls c)
	{
		shotVector = new Vector2();
		enterTime = Time.time;
		c.path.Clear();
		Vector3 mousePos3d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		aimPoint = new Vector2(mousePos3d.x, mousePos3d.y);
		Vector2 pos2D = c.player.transform.position;
		shotVector = (aimPoint - pos2D).normalized;
	}
	
	public override void Execute(MousePlayerControls c)
	{
		c.view.DrawLine(new Vector2(c.player.transform.position.x, c.player.transform.position.y) + c.player.puckCtrl, aimPoint);
		if(Puck.puck.controllingPlayer == c.player)
		{
			Puck.puck.Shoot(shotVector*c.player.shotPower*1.5f);
			c.view.ClearPlayerView();
			c.ChangeState(c.waitState);
		}
		/*
		else if (Time.time > enterTime + 5)
			c.ChangeState(c.waitState);
			*/

	}
	
	public override void Exit(MousePlayerControls c)
	{
		enterTime = Mathf.Infinity;
	}
	
}
