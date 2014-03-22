using UnityEngine;
using System.Collections;

public delegate void GameEndedHandler(LGame game);

public class GameManager : MonoBehaviour {

	public GameObject netPrefab;
	public event GameEndedHandler GameEnded;

	public static GoalZone leftGoal;
	public static GoalZone rightGoal;

	LGame mGame;

	Team teamA; //left team
	Team teamB; //right team
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

	void PlaceNets()
	{
		GameObject leftNet = (GameObject)Instantiate(netPrefab, new Vector3(-50/2 + (50/20), 0, 0), Quaternion.Euler(0,0,180));
		leftGoal = leftNet.GetComponentInChildren<GoalZone>();
		GameObject rightNet = (GameObject)Instantiate(netPrefab, new Vector3(50/2 - (50/20), 0, 0), Quaternion.identity);
		rightGoal = rightNet.GetComponentInChildren<GoalZone>();
		leftGoal.Goal+= new GoalHandler(LeftGoalScoredOn);
		rightGoal.Goal+= new GoalHandler(RighttGoalScoredOn);

	}

	void LeftGoalScoredOn()
	{
		mGame.mScoreB++;
		Debug.Log("Score: "+mGame.mScoreA+" - "+ mGame.mScoreB);
	}

	void RighttGoalScoredOn()
	{
		mGame.mScoreA++;
		Debug.Log("Score: "+mGame.mScoreA+" - "+ mGame.mScoreB);
	}

	// Use this for initialization
	public void LoadGame(LGame game)
	{
		mGame = game;
		timeLeft = mGameLength;
		CreateTeams(game.mTeamA, game.mTeamB);
		PlaceNets();
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

		GameEnded(mGame);
	}

	
}
