using UnityEngine;
using System.Collections;

public class Skater : Player {

	public Vector3 Facing;

	public PlayerControlsView View;

	public float CHECKED_TIME;

	FiniteStateMachine<Skater> m_FSM;

	public PlayerPlayState playState = new PlayerPlayState();
	public PlayerFallenState fallenState = new PlayerFallenState();
	public PlayerFaceoffState faceoffState = new PlayerFaceoffState();
	public PlayerControlledState controlledState = new PlayerControlledState();
	public bool controlled = false;

	public bool fallen;
	public bool slapshot;
    
	void RandomizeAttributes()
	{
		speed = Random.Range(0.75f, 1.25f)*speed;
	}

	void Awake()
	{
		base.Init();
		m_FSM = new FiniteStateMachine<Skater>();
		m_FSM.Init();
		m_FSM.Configure(this, playState);
		View = GetComponentInChildren<PlayerControlsView>();
	}

	void Start () 
	{
		base.Init();
		puckCtrl = new Vector2(puckCtrl.x*-(int)team.side, puckCtrl.y);
		RandomizeAttributes();
		Facing = transform.forward;	
	}

	void FixedUpdate () 
	{	
		m_FSM.UpdateStateMachine();
		Facing = m_Rigidbody.velocity.normalized;
        m_Rigidbody.AddForce(Steering.Seek(DestinationPosition));
    }

	void OnCollisionStay2D(Collision2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			Vector2 dir = other.collider.transform.position - transform.position;
			other.rigidbody.AddForce(dir.normalized*speed*7.5f);
		}
        
		if (Puck.Instance.controllingPlayer && !Puck.Instance.controllingPlayer.fallen 
		    && !fallen && other.collider == Puck.Instance.controllingPlayer.GetComponent<Collider2D>()
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
	}

	public void GetChecked(Vector2 dir)
	{
		if(Puck.Instance.controllingPlayer == this)
			Puck.Instance.Shoot(dir.normalized*shotPower*0.5f);
		if(Random.Range(0,10) > 7.5f)
			m_FSM.ChangeState(fallenState);
	}

	//*****************************************************************************
	public string curState;
	public bool statedebug = false;
	public void ChangeState(FSMState<Skater> s)
	{
		m_FSM.ChangeState(s);
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
		GetComponent<Collider2D>().enabled = false;
		GetComponent<Collider2D>().enabled = true;
		yield return new WaitForSeconds(time);
		gameObject.layer = init;
		GetComponent<Collider2D>().enabled = false;
		GetComponent<Collider2D>().enabled = true;
	}

	public Vector2 GetPassVector(Skater to)
	{
		float puckSpeed = (shotPower/1.5f)*Time.fixedDeltaTime/Puck.Instance.GetComponent<Rigidbody2D>().mass;
		float timeToReachPlayer = Vector2.Distance(transform.position, to.transform.position) / puckSpeed;
		Vector2 pos2D = to.transform.position;
		Vector2 futurePos = pos2D + to.GetComponent<Rigidbody2D>().velocity * timeToReachPlayer;
		Vector2 puckPos2D = Puck.Instance.transform.position;
		return ((futurePos - puckPos2D).normalized);
	}

	public bool HasPossession()
	{
        return (Puck.Instance.controllingPlayer == this);
	}

	public Vector2 GetPosition()
	{
		return transform.position;
	}	
}
