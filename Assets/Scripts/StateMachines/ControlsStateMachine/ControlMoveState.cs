using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ControlMoveState : FSMState<PlayerControls> {
	

	float enterTime = Mathf.Infinity;

	float timescale;

	bool queued = false;
	public override void Enter(PlayerControls c)
	{
		queued = false;
		enterTime = Time.realtimeSinceStartup;
		c.StartCoroutine(Util.ChangeTime(Time.timeScale, 0.0f, .5f));
	}
	
	public override void Execute(PlayerControls c)
	{

		Vector3 mousePos3d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mousePos = new Vector2(mousePos3d.x, mousePos3d.y);

		//drag
		if(Input.GetMouseButton(0))
		{
			//if held long enough switch to shooting mode
			/*
			if(Time.realtimeSinceStartup - enterTime > c.shotHoldTime && c.player.HasPossession() && !queued)
			{
				c.ChangeState(c.shootState);
			}
			*/
			if(Input.GetKeyDown(KeyCode.Space))
			{
				queued = true;
				c.path.Enqueue(mousePos);
				c.ChangeState(c.moveState);

			}

			DrawControl(c);
		}
		//release
		else
		{
		
			c.player.ChangeState(c.player.controlledState);
			c.path.Enqueue(mousePos);
			c.ChangeState(c.waitState);
			c.StartCoroutine(Util.ChangeTime(0, 1, .05f));
		}
		
	}
	
	public override void Exit(PlayerControls c)
	{
		enterTime = Mathf.Infinity;
	}

	void DrawControl(PlayerControls c)
	{
		Vector2 root = c.transform.position;
		if(c.path.Count > 0)
		{
			List<Vector2> temp = new List<Vector2>(c.path);
			root = temp[temp.Count-1];

		}
		c.view.DrawToMouse(root);
		
	}
	
}
