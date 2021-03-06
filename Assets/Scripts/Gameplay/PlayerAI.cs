﻿
using UnityEngine;
using System.Collections;

//TODO: REMOVE THIS CLASS, INCORPORATE INTO TEAMAI
public class PlayerAI : MonoBehaviour {





}


/*

using UnityEngine;
using System.Collections;

public class PlayerAI : MonoBehaviour {
	public float separationDistance;
	public float threatRange;
	public float passRange;
	public float shotRange;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void AvoidPlayers()
	{
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
				Puck.puck.Shoot((futurePos - puckPos2D).normalized*shotPower);
				return;
			}
		}
	}

	public Vector2 GetPassVector(Player to)
	{
		float puckSpeed = (shotPower/1.5f)*Time.fixedDeltaTime/Puck.puck.rigidbody2D.mass;
		float timeToReachPlayer = Vector2.Distance(transform.position, to.transform.position) / puckSpeed;
		Vector2 pos2D = to.transform.position;
		Vector2 futurePos = pos2D + to.rigidbody2D.velocity * timeToReachPlayer;
		Vector2 puckPos2D = Puck.puck.transform.position;
		return ((futurePos - puckPos2D).normalized);
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

		if(Vector2.Distance(net.transform.position, transform.position) < shotRange && CanScore(net))
		{
			Vector3 netPoint = net.transform.position + new Vector3(net.transform.localScale.x/2*(int)team.side, Random.Range(-net.transform.localScale.y*1.5f, net.transform.localScale.y*1.5f) , 0);
			Puck.puck.Shoot((netPoint-Puck.puck.transform.position).normalized*shotPower);
		}
	}
}
*/
