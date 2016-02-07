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

    
	public List<Skater> mPlayers;

    /*
	public Team opponent;

	public TeamAI mTeamAI;

	void Start()
	{
		mTeamAI = GetComponent<TeamAI>();
	}

	public void GoToFaceoff()
	{
		mTeamAI.ChangeState(mTeamAI.faceoffState);
	}

	public void DropPuck()
	{
		mTeamAI.ChangeState(mTeamAI.defendState);
	}

	public bool IsReady()
	{
		return mTeamAI.TeamIsAtDest();
	}

	public void SpawnPlayers(LTeam team, int numPlayers)
	{
		mPlayers = new List<Skater>();
		for(int i = 0; i < numPlayers; i++)
		{
			GameObject g = (GameObject) Instantiate(playerPrefab, new Vector2((int)side*5, -8), Quaternion.identity);
			g.GetComponentInChildren<Animator>().runtimeAnimatorController = controller;
			g.transform.parent = transform;
			Skater p = g.GetComponent<Skater>();
			p.team = this;		
			p.MoveTo(p.transform.position);
			mPlayers.Add(p);
		}
	}
    */

}