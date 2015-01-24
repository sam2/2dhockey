using UnityEngine;
using System.Collections;

public class ControlShootState : FSMState<MousePlayerControls> {

	float enterTime = Mathf.Infinity;
	bool slapper = false;
	public override void Enter(MousePlayerControls c)
	{
		slapper = false;
		enterTime = Time.realtimeSinceStartup;
		Puck.puck.highlightCircle.SetActive(true);
		c.path.Clear();
		multiplier = 1;
		c.view.ChangeLineColor(Color.red);

		if(!TimeManager.Instance.paused)
			TimeManager.Instance.ChangeState(TimeManager.Instance.slowState);
	}

	float multiplier = 1;
	public override void Execute(MousePlayerControls c)
	{



		if(Time.realtimeSinceStartup - enterTime > c.shotHoldTime && !slapper)
		{
			c.view.slapShot.Play();
			slapper = true;
			multiplier = 1.5f;
			c.player.rigidbody2D.velocity = Vector2.zero;

		}

		c.view.DrawShot(new Vector2(c.player.transform.position.x, c.player.transform.position.y) + c.player.puckCtrl);

		if(!Input.GetMouseButton(1))
		{
			if(c.player != Puck.puck.controllingPlayer)
			{
				c.ChangeState(c.oneTimer);
			}
			else
			{
		
				Vector3 mousePos3d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector2 mousePos = new Vector2(mousePos3d.x, mousePos3d.y);
				Vector2 pos2D = Puck.puck.transform.position;
				Vector2 shotVector = (mousePos - pos2D).normalized;
				Puck.puck.Shoot(shotVector*c.player.shotPower*multiplier);
				c.ChangeState(c.waitState);

			}

		}
		c.player.slapshot = slapper;

		/*
		if(c.player != Puck.puck.controllingPlayer)
		{
			c.ChangeState(c.waitState);
		}
		*/
	}
	
	public override void Exit(MousePlayerControls c)
	{
		if(!TimeManager.Instance.paused)
			TimeManager.Instance.ChangeState(TimeManager.Instance.normalState);
		Puck.puck.highlightCircle.SetActive(false);
		slapper = false;
		c.player.slapshot = slapper;

	}
	
}
