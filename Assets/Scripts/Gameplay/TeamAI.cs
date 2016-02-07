using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamAI : MonoBehaviour {

    public float SEPARATION_DISTANCE;
    public float WANDER_FACTOR;
    public Vector2 mDZoneSize;

    public List<Vector2> DefensivePositions;
	public List<Vector2> OffensivePositions;
	public List<Vector2> FaceoffPositions;

	public TeamAttackingState attackState = new TeamAttackingState();
	public TeamDefendingState defendState = new TeamDefendingState();
	public TeamFaceoffState faceoffState = new TeamFaceoffState();
	
	private FiniteStateMachine<TeamAI> FSM;

	public Team Team;
    public List<PlayerAI> mPlayerAIs;
	
	public void UpdateAI () 
	{
		FSM.UpdateStateMachine();
	}

    public void Init()
    {
        FSM = new FiniteStateMachine<TeamAI>();
        FSM.Init();
        FSM.Configure(this, faceoffState);
        Team = GetComponent<Team>();        
    }

	void Start()
	{        
        Puck.Instance.PuckControlChanged += new PuckControlChangedHandler(OnPlayerRecievedPuck);
    }
		
	public bool TeamIsAtDest()
	{
		foreach(Player p in Team.mPlayers)
		{
            if ((p.DestinationPosition - new Vector2(p.transform.position.x, p.transform.position.y)).sqrMagnitude > 1)
            {
				return false;
			}
		}
		return true;
	}

	public void ChangeState(FSMState<TeamAI> state)
	{
        Debug.Log(name + "->" + state.ToString());
		FSM.ChangeState(state);
	}
	
	public bool InControl()
	{
		if(Puck.Instance.controllingPlayer != null)
		{
			if(Puck.Instance.controllingPlayer.team == this.Team)
			{
				return true;
			}
		}
		return false;
	}
	
	void OnPlayerRecievedPuck(Skater p)
	{

	}

	public bool IsOnTeam(Skater p)
	{
		return p.team == Team;
	}

	Skater ClosestToPuck()
	{
		float distance = 9999;
		Skater closest = null;
		foreach(Skater p in Team.mPlayers)
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
			Skater hit = ray.collider.GetComponent<Skater>();
			if(hit && hit.team != this)
			{
				return false;
			}
		}
		return true;
	}

    public Vector2 CalculateDefensivePosition(int playerIndex)
    {
        Vector2 homePos = DefensivePositions[playerIndex];
        Vector2 puckVector = (Vector2)Puck.Instance.transform.position - homePos;
        Vector2 pos = Vector2.zero;

        if (PointIsInRectangle(Puck.Instance.transform.position, homePos, mDZoneSize)
            || (Puck.Instance.controllingPlayer != null))
            pos = Puck.Instance.transform.position;
        else
        {
            pos = homePos + (puckVector * WANDER_FACTOR);
        }

        if (!PointIsInRectangle(pos, homePos, mDZoneSize))
        {
            pos = (Vector2)homePos + pos.normalized * mDZoneSize.x / 2;
        }
        return pos;
    }

    public Vector2 CalculateOffensivePosition(int playerIndex)
    {
        Vector2 homePos = OffensivePositions[playerIndex];
        Vector2 puckVector = (Vector2)Puck.Instance.transform.position - homePos;
        Vector2 pos = Vector2.zero;

        if (PointIsInRectangle(Puck.Instance.transform.position, homePos, mDZoneSize)
            || (Puck.Instance.controllingPlayer != null))
            pos = Puck.Instance.transform.position;
        else
        {
            pos = homePos + (puckVector * WANDER_FACTOR);
        }

        if (!PointIsInRectangle(pos, homePos, mDZoneSize))
        {
            pos = (Vector2)homePos + pos.normalized * mDZoneSize.x / 2;
        }
        return pos;
    }

    public Vector2 CalculateFaceOffPosition(int playerIndex)
    {
        return FaceoffPositions[playerIndex];
    }


    bool PointIsInRectangle(Vector2 point, Vector2 rCenter, Vector2 rExtents)
    {
        float leftSide = rCenter.x - rExtents.x;
        float rightSide = rCenter.x + rExtents.x;
        float topSide = rCenter.y + rExtents.y;
        float botSide = rCenter.y - rExtents.y;

        return (point.x < rightSide && point.x > leftSide) && (point.y < topSide && point.y > botSide);
    }

    
}
