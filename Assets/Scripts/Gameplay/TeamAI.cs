using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamAI : MonoBehaviour {

	public List<Vector2> mDefensivePositions;
	public List<Vector2> mOffensivePositions;
	public List<Vector2> mFaceoffPositions;


	public TeamAttackingState attackState = new TeamAttackingState();
	public TeamDefendingState defendState = new TeamDefendingState();
	public TeamFaceoffState faceoffState = new TeamFaceoffState();
	
	private FiniteStateMachine<TeamAI> FSM;

	public Team mTeam;
	public List<PlayerAI> mPlayerAIs = new List<PlayerAI>();
	// Use this for initialization
	void Awake () 
	{


	}
	
	// Update is called once per frame
	void Update () 
	{
		FSM.UpdateStateMachine();
	}

	public void Init()
	{
		foreach(Player p in mTeam.mPlayers)
		{
			mPlayerAIs.Add(p.GetComponent<PlayerAI>());
		}
		//SpawnPlayers();
		Puck.Instance.PuckControlChanged += new PuckControlChangedHandler(OnPlayerRecievedPuck);
		mTeam = GetComponent<Team>();
		FSM = new FiniteStateMachine<TeamAI>();
		FSM.Init();
		FSM.Configure(this, faceoffState);


	}

	
	public void SetHomePositions(List<Vector2> positions)
	{
		for(int i = 0; i < mPlayerAIs.Count;i++)
		{
			mPlayerAIs[i].mHomePosition = positions[i];
		}
	}
		
	public bool TeamIsAtDest()
	{
		foreach(PlayerAI p in mPlayerAIs)
		{
			if(!p.IsAtPoint(p.mHomePosition))
			{
				return false;
			}
		}
		return true;
	}

	public void ChangeState(FSMState<TeamAI> state)
	{
		FSM.ChangeState(state);
	}
	
	public bool InControl()
	{
		if(Puck.Instance.controllingPlayer != null)
		{
			if(Puck.Instance.controllingPlayer.team == this.mTeam)
			{
				return true;
			}
		}
		return false;
	}
	
	void OnPlayerRecievedPuck(Player p)
	{

	}

	public bool IsOnTeam(Player p)
	{
		return p.team == mTeam;
	}

	Player ClosestToPuck()
	{
		float distance = 9999;
		Player closest = null;
		foreach(Player p in mTeam.mPlayers)
		{
			float newDist = Vector2.Distance(p.transform.position, Puck.Instance.transform.position);
			if( newDist < distance && !p.fallen)
			{
				closest = p;
				distance = newDist;
			}
		}
		
		return closest;
	}
	

	public bool IsPassSafe(Vector2 target)
	{
		//Debug.DrawLine(Puck.puck.transform.position, target);
		RaycastHit2D ray = Physics2D.Linecast(Puck.Instance.transform.position, target);
		if(ray)
		{
			Player hit = ray.collider.GetComponent<Player>();
			if(hit && hit.team != this)
			{
				return false;
			}
		}
		return true;
	}
}
