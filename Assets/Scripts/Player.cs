using UnityEngine;
using System.Collections;



public class Player : PlayerBase {

	public bool AI;

	public Vector3 facing;

	//*****************************************************************************

	TouchControls controls;

	//AI***********************************************************************

	public float manualControlTime;
	public float checkedTime;
	public float separationDistance;
	public float threatRange;
	public float passRange;
	public float shotRange;

	//*****************************************************************************


	public LineRenderer destDrawer;

	//*****************************************************************************

	FiniteStateMachine<Player> FSM;

	public PlayerWaitState waitState = new PlayerWaitState();
	public PlayerFallenState fallenState = new PlayerFallenState();
	public PlayerReceivePuckState receiveState = new PlayerReceivePuckState();
	public PlayerShootPuckState shootState = new PlayerShootPuckState();
	public PlayerCarryPuckState carryState = new PlayerCarryPuckState();
	public PlayerChasePuckState chaseState = new PlayerChasePuckState();
	public PlayerReturnState returnState = new PlayerReturnState();
	public PlayerSupportState supportState = new PlayerSupportState();
	public PlayerFaceoffState faceoffState = new PlayerFaceoffState();

	public PlayerControlledState controlledState = new PlayerControlledState();

	//*****************************************************************************

	public bool fallen;




	//*****************************************************************************

	void RandomizeAttributes()
	{
		speed = Random.Range(0.75f, 1.25f)*speed;
		turnSpeed = Random.Range(0.75f, 1.25f)*turnSpeed;
		shotPower = Random.Range(0.75f, 1.25f)*shotPower;

	}
	//*****************************************************************************

	public void DrawLine(Vector2 start, Vector2 end)
	{
		destDrawer.SetVertexCount(2);
		destDrawer.SetPosition(0, start);
		destDrawer.SetPosition(1, end);
	}

	public void ClearLine()
	{
		destDrawer.SetVertexCount(0);
	}

	//*****************************************************************************
	void Awake()
	{
		controls = GetComponentInChildren<TouchControls>();
		FSM = new FiniteStateMachine<Player>();
		FSM.Init();
		FSM.Configure(this, waitState);
	}
	void Start () 
	{
		base.Init();

		SetControllable(!AI);
		puckCtrl = new Vector2(puckCtrl.x*-(int)team.side, puckCtrl.y);
		RandomizeAttributes();
		steering = new SteeringBehavior(rigidbody2D, speed);
		facing = transform.forward;



		destinationPosition = mHomePosition;
		destDrawer = GetComponent<LineRenderer>();
	}

	//*****************************************************************************
	
	// Update is called once per frame
	void FixedUpdate () 
	{	
		FSM.UpdateStateMachine();
		facing = rigidbody2D.velocity.normalized;
		if(!fallen)
		{
			foreach(Player p in team.mPlayers)
			{
				rigidbody2D.AddForce(steering.Evade(p.rigidbody2D, separationDistance)*0.50f);
			}

			if(team.InControl())
			{
				foreach(Player p in team.opponent.mPlayers)
				{
					rigidbody2D.AddForce(steering.Evade(p.rigidbody2D, separationDistance)*0.5f);
				}

			
			}
		}


	}

	public void SetControllable(bool ctrl)
	{
		if(controls)
			controls.gameObject.SetActive(ctrl);
		else
			Debug.LogError("No controls found on player");
	}



	//*****************************************************************************

	void OnCollisionStay2D(Collision2D other)
	{

		if (Puck.puck.controllingPlayer && !Puck.puck.controllingPlayer.fallen 
		    && !fallen && other.collider == Puck.puck.controllingPlayer.collider2D
		    && Puck.puck.controllingPlayer.team != team )
		{
			Vector2 dir = other.collider.transform.position - transform.position;
			if(RollForChecked())
			{

				Puck.puck.controllingPlayer.GetChecked(dir);

			}
			else
			{
				GetChecked(dir);
			}
			
		}
		/*
		Player p = other.gameObject.GetComponent<Player>();
		if (p && !fallen)
		{

				Vector2 dir = other.collider.transform.position - transform.position;
				p.GetChecked();
				if(Puck.puck.controllingPlayer == p)
					Puck.puck.Shoot(dir.normalized*shotPower*Random.Range(.1f, .25f));
			
		}
		*/
	}



	//*****************************************************************************

	public void GetChecked(Vector2 dir)
	{
		if(Puck.puck.controllingPlayer == this)
			Puck.puck.Shoot(dir.normalized*shotPower*Random.Range(.1f, .25f));
		FSM.ChangeState(fallenState);
	}

	//*****************************************************************************

	public void ChangeState(FSMState<Player> s)
	{
		FSM.ChangeState(s);
	}

	//*****************************************************************************

	public void DisableBox(float time)
	{
		StartCoroutine(DisableBoxCoroutine(time));
	}

	//*****************************************************************************
	               
	IEnumerator DisableBoxCoroutine(float time)
	{
		int init = gameObject.layer;
		gameObject.layer = 8;
		collider2D.enabled = false;
		collider2D.enabled = true;
		yield return new WaitForSeconds(time);
		gameObject.layer = init;
		collider2D.enabled = false;
		collider2D.enabled = true;
	}

	//*****************************************************************************
	public void FindPass()
	{
		foreach(Player p in team.mPlayers)
		{
			float puckSpeed = (shotPower/1.5f)*Time.fixedDeltaTime/Puck.puck.rigidbody2D.mass;
			float timeToReachPlayer = Vector2.Distance(transform.position, p.transform.position) / puckSpeed;
			Vector2 pos2D = p.transform.position;
			Vector2 futurePos = pos2D + p.rigidbody2D.velocity * timeToReachPlayer;

			if(p != this && team.IsPassSafe(futurePos) 
			   && Vector2.Distance(p.transform.position, transform.position) > threatRange 
			   && !p.IsThreatened()
			   && Vector2.Distance(p.transform.position, transform.position) < passRange)
			{
				Vector2 puckPos2D = Puck.puck.transform.position;
				Puck.puck.Shoot((futurePos - puckPos2D).normalized*shotPower/1.5f);
				return;
			}
		}
	}

	public bool IsThreatened()
	{
		foreach(Player p in team.opponent.mPlayers)
		{
			if(Vector2.Distance(transform.position, p.transform.position) < threatRange && !p.fallen)
				return true;
		}
		return false;
	}

	public bool CanScore(Collider2D net)
	{

		Vector2 dir = net.transform.position - transform.position;
		RaycastHit2D hit = Physics2D.Raycast(Puck.puck.transform.position, dir);
		if(hit.collider.tag != "Player")
		{
			if(Mathf.Sign(transform.position.x - (net.transform.position.x + 2*(int)team.side)) == (int)team.side)
				return true;
		}
		return false;
	}

	public void LookForShot()
	{
		Collider2D net;
		if((int)team.side == -1)
		{
			net = GameManager.rightGoal.collider2D;
		}
		else
			net = GameManager.leftGoal.collider2D;

		if(Vector2.Distance(net.transform.position, transform.position) < shotRange && CanScore(net) && Puck.puck.controllingPlayer == this)
		{
			Vector3 netPoint = net.transform.position + new Vector3(net.transform.localScale.x/2*(int)team.side, Random.Range(-net.transform.localScale.y*1.5f, net.transform.localScale.y*1.5f) , 0);
			Debug.DrawRay(Puck.puck.transform.position, (netPoint-Puck.puck.transform.position));
			Puck.puck.Shoot((netPoint-Puck.puck.transform.position).normalized*shotPower);
		}
	}

	public void LookForPass()
	{

	}

	//*****************************************************************************

	//messages
	public void ReceivePuck()
	{
		destinationPosition = Puck.puck.transform.position;
		ChangeState(receiveState);
	}

	//find good area to be passed to
	public void SupportAttacker()
	{
		//TODO
	}

	//used for kickoffs
	public void GoHome()
	{
		//setdefaulthomeregion
		ChangeState(returnState);
	}

	public void Wait()
	{
		ChangeState (waitState);
	}

	public void PassToMe()
	{
		//TODO
	}

	
}
