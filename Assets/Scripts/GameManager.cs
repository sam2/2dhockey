using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	int mScoreA;
	int mScoreB;

	Team teamA;
	Team teamB;
	public GameObject teamAPrefab;
	public GameObject teamBPrefab;

	public float mGameLength;
	float timeLeft;

	bool stopped = true;

	void CreateTeams(LTeam lteamA, LTeam lteamB)
	{
		GameObject gA = (GameObject)Instantiate(teamAPrefab);
		teamA = gA.GetComponent<Team>();

		GameObject gB = (GameObject)Instantiate(teamBPrefab);
		teamB = gB.GetComponent<Team>();

		teamA.opponent = teamB;
		teamB.opponent = teamA;
		teamA.SpawnPlayers(lteamA);
		teamA.Init();
		teamB.SpawnPlayers(lteamB);
		teamB.Init();
	}

	// Use this for initialization
	public void LoadGame(LGame game)
	{
		timeLeft = mGameLength;
		CreateTeams(game.mTeamA, game.mTeamB);
		Debug.Log("Loading game: "+game.mTeamA.mName+" vs "+game.mTeamB.mName);
	}

	public void StartGame()
	{
		stopped = false;
	}

	public void StopGame()
	{
		stopped = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!stopped)
		{
			timeLeft -= Time.deltaTime;
			if(timeLeft <= 0)
			{
				stopped = true;
				EndGame();
			}
		}
	}

	void EndGame()
	{
		Debug.Log("game over: score of "+mScoreA+"-"+mScoreB);
	}
	
}
