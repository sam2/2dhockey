using UnityEngine;
using System.Collections;



public class Player : PlayerBase {


	public Vector3 facing;

	public PlayerControlsView mView;

	public float checkedTime;

	FiniteStateMachine<Player> FSM;

	public PlayerPlayState playState = new PlayerPlayState();
	public PlayerFallenState fallenState = new PlayerFallenState();
	public PlayerFaceoffState faceoffState = new PlayerFaceoffState();
	public PlayerControlledState controlledState = new PlayerControlledState();
	public bool controlled = false;
	//*****************************************************************************

	public bool fallen;
	public bool slapshot;



	//*****************************************************************************

	void RandomizeAttributes()
	{
		speed = Random.Range(0.75f, 1.25f)*speed;

		//shotPower = Random.Range(0.75f, 1.25f)*shotPower;

	}
	//*****************************************************************************
	
	

	//*****************************************************************************
	void Awake()
	{
		base.Init();
		FSM = new FiniteStateMachine<Player>();
		FSM.Init();
		FSM.Configure(this, playState);
		mView = GetComponentInChildren<PlayerControlsView>();
	}

	void Start () 
	{
		base.Init();
		puckCtrl = new Vector2(puckCtrl.x*-(int)team.side, puckCtrl.y);
		RandomizeAttributes();
		facing = transform.forward;	
	}

	//*****************************************************************************
	
	// Update is called once per frame
	void FixedUpdate () 
	{	
		FSM.UpdateStateMachine();
		facing = rigidbody2D.velocity.normalized;



	}





	//*****************************************************************************

	void OnCollisionStay2D(Collision2D other)
	{

		if(other.gameObject.tag == "Player")
		{
			Vector2 dir = other.collider.transform.position - transform.position;
			other.rigidbody.AddForce(dir.normalized*speed*7.5f);
		}



		if (Puck.Instance.controllingPlayer && !Puck.Instance.controllingPlayer.fallen 
		    && !fallen && other.collider == Puck.Instance.controllingPlayer.collider2D
		    && Puck.Instance.controllingPlayer.team != team )
		{
			Vector2 dir = other.collider.transform.position - transform.position;
			if(RollForChecked())
			{

				Puck.Instance.controllingPlayer.GetChecked(dir);

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
		if(Puck.Instance.controllingPlayer == this)
			Puck.Instance.Shoot(dir.normalized*shotPower*0.5f);
		if(Random.Range(0,10) > 7.5f)
			FSM.ChangeState(fallenState);
	}

	//*****************************************************************************
	public string curState;
	public bool statedebug = false;
	public void ChangeState(FSMState<Player> s)
	{
		FSM.ChangeState(s);
		curState = s.ToString();
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

	public Vector2 GetPassVector(Player to)
	{
		float puckSpeed = (shotPower/1.5f)*Time.fixedDeltaTime/Puck.Instance.rigidbody2D.mass;
		float timeToReachPlayer = Vector2.Distance(transform.position, to.transform.position) / puckSpeed;
		Vector2 pos2D = to.transform.position;
		Vector2 futurePos = pos2D + to.rigidbody2D.velocity * timeToReachPlayer;
		Vector2 puckPos2D = Puck.Instance.transform.position;
		return ((futurePos - puckPos2D).normalized);
	}



	public bool HasPossession()
	{
		if(Puck.Instance.controllingPlayer == this)
		{
			return true;
		}
		return false;
	}

	//*****************************************************************************





	
}
