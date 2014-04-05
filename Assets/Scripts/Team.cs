using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Team : MonoBehaviour{

	public GameObject playerPrefab;
	public RuntimeAnimatorController controller;
	public GameObject goaliePrefab;

	public enum Side
	{
		left = -1,
		right = 1
	}

	public Side side;

	public List<Player> mPlayers;
	public List<Vector2> mDefensivePositions;
	public List<Vector2> mOffensivePositions;

	public Team opponent;

	public Player mControllingPlayer;
	public Player mPlayerClosestToPuck;
	public Player mReceivingPlayer;
	public Player mSupportingPlayer;

	public TeamAttackingState attackState = new TeamAttackingState();
	public TeamDefendingState defendState = new TeamDefendingState();
	public TeamFaceoffState faceoffState = new TeamFaceoffState();

	private FiniteStateMachine<Team> FSM;

	public bool AI;

	public void Init()
	{
		//SpawnPlayers();

		FSM = new FiniteStateMachine<Team>();
		FSM.Init();
		FSM.Configure(this, faceoffState);
	}

	public void ChangeState(FSMState<Team> state)
	{
		FSM.ChangeState(state);
	}

	void Start()
	{
		Puck.puck.PuckControlChanged += new PuckControlChangedHandler(OnPlayerRecievedPuck);
	}

	public void SpawnPlayers(LTeam team)
	{
		mPlayers = new List<Player>();
		for(int i = 0; i < mDefensivePositions.Count; i++)
		{
			GameObject g = (GameObject) Instantiate(playerPrefab, mDefensivePositions[i], Quaternion.identity);
			g.GetComponentInChildren<Animator>().runtimeAnimatorController = controller;
			g.transform.parent = transform;
			Player p = g.GetComponent<Player>();
			p.team = this;
			p.AI = AI;
			p.destinationPosition = p.transform.position;
			mPlayers.Add(p);
		}
	}
	
	public void SetHomePositions(List<Vector2> positions)
	{
		for(int i = 0; i < mPlayers.Count; i++)
		{
			mPlayers[i].mHomePosition = positions[i];
		}
	}

	public void SetDestinationPositionsToHome()
	{
		foreach(Player p in mPlayers)
		{
			p.destinationPosition = p.mHomePosition;
		}
	}

	public bool TeamIsAtDest()
	{
		foreach(Player p in mPlayers)
		{
			if(!p.isAtDestination())
			{
				return false;
			}
		}
		return true;
	}

	public bool InControl()
	{
		if(Puck.puck.controllingPlayer != null)
		{
			if(Puck.puck.controllingPlayer.team == this)
			{
				return true;
			}
		}
		return false;
	}

	void OnPlayerRecievedPuck(Player p)
	{
		if(p.team == this)		                     
			FSM.ChangeState(attackState);
		else
			FSM.ChangeState(defendState);
	}


	void Update()
	{
		mPlayerClosestToPuck = ClosestToPuck();
		FSM.UpdateStateMachine();

	}

	Player ClosestToPuck()
	{
		float distance = 9999;
		Player closest = null;
		foreach(Player p in mPlayers)
		{
			float newDist = Vector2.Distance(p.transform.position, Puck.puck.transform.position);
			if( newDist < distance && !p.fallen)
			{
				closest = p;
				distance = newDist;
			}
		}

		return closest;
	}
	
	public Vector2 DetermineBestSupportingPosition()
	{
		//float bestScore;
		Vector2 spot = Vector2.zero;
		foreach(SupportSpot s in ArenaGenerator.samples)
		{
			s.mScore = 0;
		}
		return spot;
	}

	public bool IsPassSafe(Vector2 target)
	{
		//Debug.DrawLine(Puck.puck.transform.position, target);
		RaycastHit2D ray = Physics2D.Linecast(Puck.puck.transform.position, target);
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

	public void SetControllable(bool controllable)
	{
		foreach(Player p in mPlayers)
			p.SetControllable(controllable);
	}
	
}