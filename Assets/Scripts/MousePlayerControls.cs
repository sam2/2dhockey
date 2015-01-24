using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MousePlayerControls : MonoBehaviour, IPlayerControls {

	public Player player;

	public PlayerControlsView view;

	FiniteStateMachine<MousePlayerControls> controls = new FiniteStateMachine<MousePlayerControls>();
	public ControlMoveState moveState = new ControlMoveState();
	public ControlShootState shootState = new ControlShootState();
	public ControlWaitState waitState = new ControlWaitState();
	public ControlOneTimerState oneTimer = new ControlOneTimerState();



	//tunables
	public float shotHoldTime;

	public Queue<Vector2> path = new Queue<Vector2>();

	public GameObject GetGameObject()
	{
		return gameObject;
	}

	public PlayerControlsView GetView()
	{
		return view;
	}

	public Queue<Vector2> GetPath()
	{
		return path;
	}


	bool paused = false;
	void Awake () 
	{
		controls.Init();
		controls.Configure(this, waitState);

	}

	void FixedUpdate()
	{
		controls.UpdateStateMachine();
	}

	
	// Update is called once per frame
	void Update () 
	{

		if(path.Count > 0)
			view.DrawPath(path);
		if(player.fallen)
		{

			ChangeState(waitState);
			path.Clear();
			view.ClearPlayerView();
		}
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		RaycastHit hit = new RaycastHit();

		if(Input.GetMouseButtonDown(1) && Physics.Raycast(ray, out hit))
		{
			if(hit.collider.gameObject == this.gameObject)
				ChangeState(shootState);
		}

	





	}

	void OnMouseDown () 
	{
		path.Clear();
		controls.ChangeState(moveState);

	}

	public void ChangeState(FSMState<MousePlayerControls> s)
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
