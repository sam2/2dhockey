using UnityEngine;
using System.Collections;

public class TouchControls : MonoBehaviour {

	enum ControlState
	{
		shooting,
		moving,
		none
	}

	public float shotCancelRange;

	public Player player;
	ControlState state;

	float mClickStartTime;
	public float shotHoldTime;

	public DiskDrawer diskDrawer;

	// Use this for initialization
	Color lineColor;
	void Awake () {
		diskDrawer = GetComponent<DiskDrawer>();
		state = ControlState.none;
		lineColor = diskDrawer.lineRenderer.material.color;
	}
	Player prevController;
	void Update()
	{
		if(Puck.puck.controllingPlayer == player && prevController != player)
		{
			player.ChangeState(player.controlledState);
		}
		prevController = Puck.puck.controllingPlayer;
	}
	
	// Update is called once per frame
	void OnMouseDown () 
	{
		state = ControlState.moving;
		mClickStartTime = Time.realtimeSinceStartup;
		diskDrawer.lineRenderer.enabled = true;
		diskDrawer.lineRenderer.SetPosition(0, transform.position);
		diskDrawer.lineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
		diskDrawer.enabled = true;
		Time.timeScale = 0f;
		Time.fixedDeltaTime = 0f;
	}

	void OnMouseDrag()
	{
		Vector2 lineVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if(Time.realtimeSinceStartup - mClickStartTime > shotHoldTime && Puck.puck.controllingPlayer == player && !isInCancelZone())
		{
			diskDrawer.lineRenderer.enabled = true;
			diskDrawer.lineRenderer.material.color = Color.red;
			state = ControlState.shooting;
			diskDrawer.lineRenderer.SetPosition(0, Puck.puck.transform.position);

			Puck.puck.highlightCircle.SetActive(true);
			Time.timeScale = 0.05f;
			Time.fixedDeltaTime = 0.02f*Time.timeScale;
			Vector2.ClampMagnitude(lineVector, 1);
			//lineVector = (lineVector + new Vector2(Puck.puck.transform.position.x, Puck.puck.transform.position.y));

		}
		else if(state == ControlState.shooting)
		{
			Puck.puck.highlightCircle.SetActive(false);
			diskDrawer.lineRenderer.enabled = false;
			state = ControlState.none;
		}
		diskDrawer.lineRenderer.SetPosition(1, lineVector);
	}

	void OnMouseUp()
	{
		mClickStartTime = 0;
		diskDrawer.enabled = false;
		diskDrawer.lineRenderer.material.color = lineColor;
		Vector3 mousePos3d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mousePos = new Vector2(mousePos3d.x, mousePos3d.y);
		Vector2 pos2D = Puck.puck.transform.position;

		if(state == ControlState.shooting)
		{
			Puck.puck.highlightCircle.SetActive(false);
			Vector2 shotVector = (mousePos - pos2D).normalized;
			Puck.puck.Shoot(shotVector*player.shotPower);
			//SHOOT THE PUCK

			state = ControlState.none;
		}

		if(state == ControlState.moving && !player.fallen)
		{
			player.ChangeState(player.controlledState);
			player.destinationPosition = mousePos;
		}
		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02f*Time.timeScale;

		diskDrawer.lineRenderer.enabled = false;
	}

	bool isInCancelZone()
	{
		Vector3 mousePos3d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mousePos = new Vector2(mousePos3d.x, mousePos3d.y);
		Vector2 puckPos = new Vector2(Puck.puck.transform.position.x, Puck.puck.transform.position.y);
		if(Vector2.Distance(mousePos, puckPos) < shotCancelRange)
		{
			return true;
		}
		return false;
	}



}
