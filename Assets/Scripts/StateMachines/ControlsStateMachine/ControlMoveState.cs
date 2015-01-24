using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ControlMoveState : FSMState<MousePlayerControls> {
	

	float enterTime = Mathf.Infinity;

	float timescale;

	bool queued = false;
	public override void Enter(MousePlayerControls c)
	{
		c.view.ChangeLineColor(Color.blue);
		queued = false;
		enterTime = Time.realtimeSinceStartup;
	}
	
	public override void Execute(MousePlayerControls c)
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

			if(Input.GetKeyDown(KeyCode.Space))
			{
				queued = true;
				c.path.Enqueue(mousePos);
				c.ChangeState(c.moveState);

			}
			*/
			DrawControl(c);
		}
		//release
		else
		{
		
			c.player.ChangeState(c.player.controlledState);
			c.path.Enqueue(mousePos);
			c.ChangeState(c.waitState);
		}
		
	}
	
	public override void Exit(MousePlayerControls c)
	{
		enterTime = Mathf.Infinity;
	}

	void DrawControl(MousePlayerControls c)
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
