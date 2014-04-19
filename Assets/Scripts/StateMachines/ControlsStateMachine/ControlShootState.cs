using UnityEngine;
using System.Collections;

public class ControlShootState : FSMState<PlayerControls> {
	
	
	public override void Enter(PlayerControls c)
	{

		Puck.puck.highlightCircle.SetActive(true);
		c.StartCoroutine(Util.ChangeTime(Time.timeScale, 0.25f, .25f));
		c.path.Clear();
	}
	
	public override void Execute(PlayerControls c)
	{

		c.view.DrawShot(Puck.puck.transform.position);

		if(!Input.GetMouseButton(0))
		{
			Time.timeScale = 1f;
			Time.fixedDeltaTime = 0.02f*Time.timeScale;
		
			Vector3 mousePos3d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos = new Vector2(mousePos3d.x, mousePos3d.y);
			Vector2 pos2D = Puck.puck.transform.position;
			Vector2 shotVector = (mousePos - pos2D).normalized;
			Puck.puck.Shoot(shotVector*c.player.shotPower);
			c.ChangeState(c.waitState);

		}
		
	}
	
	public override void Exit(PlayerControls c)
	{
		Puck.puck.highlightCircle.SetActive(false);

	}
	
}
