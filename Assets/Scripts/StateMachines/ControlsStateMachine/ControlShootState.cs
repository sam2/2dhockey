using UnityEngine;
using System.Collections;

public class ControlShootState : FSMState<PlayerControls> {

	float enterTime = Mathf.Infinity;
	bool slapper = false;
	public override void Enter(PlayerControls c)
	{
		slapper = false;
		enterTime = Time.realtimeSinceStartup;
		Puck.puck.highlightCircle.SetActive(true);
		c.path.Clear();
		c.StartCoroutine(Util.ChangeTime(1,.25f,0.1f));
		multiplier = 1;
		c.view.ChangeLineColor(Color.red);
	}

	float multiplier = 1;
	public override void Execute(PlayerControls c)
	{



		if(Time.realtimeSinceStartup - enterTime > c.shotHoldTime && !slapper)
		{
			c.view.slapShot.Play();
			slapper = true;
			multiplier = 1.5f;
			c.player.rigidbody2D.velocity = Vector2.zero;

		}

		c.view.DrawShot(Puck.puck.transform.position);

		if(!Input.GetMouseButton(1))
		{

		
			Vector3 mousePos3d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos = new Vector2(mousePos3d.x, mousePos3d.y);
			Vector2 pos2D = Puck.puck.transform.position;
			Vector2 shotVector = (mousePos - pos2D).normalized;
			Puck.puck.Shoot(shotVector*c.player.shotPower*multiplier);
			c.ChangeState(c.waitState);

		}
		c.player.slapshot = slapper;

		if(c.player != Puck.puck.controllingPlayer)
		{
			c.ChangeState(c.waitState);
		}
	}
	
	public override void Exit(PlayerControls c)
	{
		Puck.puck.highlightCircle.SetActive(false);
		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02f*Time.timeScale;
		slapper = false;
		c.player.slapshot = slapper;
		c.view.ChangeLineColor(Color.blue);
	}
	
}
