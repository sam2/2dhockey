using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour {

	public Player player;

	public PlayerControlsView view;

	FiniteStateMachine<PlayerControls> controls = new FiniteStateMachine<PlayerControls>();
	public ControlMoveState moveState = new ControlMoveState();
	public ControlShootState shootState = new ControlShootState();
	public ControlWaitState waitState = new ControlWaitState();

	//tunables
	public float shotHoldTime;

	public Queue<Vector2> path = new Queue<Vector2>();

	void Awake () 
	{
		controls.Init();
		controls.Configure(this, waitState);
	}
	
	// Update is called once per frame
	void Update () 
	{
		controls.UpdateStateMachine();
		if(path.Count > 0)
			view.DrawPath(path);
		if(player.fallen)
		{
			Time.timeScale = 1f;
			Time.fixedDeltaTime = 0.02f*Time.timeScale;
			ChangeState(waitState);
			path.Clear();
			view.ClearLineRenderer();
		}
	}

	void OnMouseDown () 
	{
		path.Clear();
		controls.ChangeState(moveState);

	}

	public void ChangeState(FSMState<PlayerControls> s)
	{
		controls.ChangeState(s);
	}

	public void UpdatePath()
	{
		if(path.Count > 0)
		{
			player.destinationPosition = path.Peek();
			if(player.isAtDestination())
			{
				path.Dequeue();
			}
		}
		else if(!player.HasPossession())
		{
			player.ChangeState(player.returnState);

		}
	}



}
