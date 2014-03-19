using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	int id;

	int mScoreA;
	int mScoreB;

	public float mGameLength;
	float timeLeft;

	bool stopped = true;

	// Use this for initialization
	void Start () {
		timeLeft = mGameLength;
	}
	
	// Update is called once per frame
	void Update () {

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

	}

	public LGame GetGameStats()
	{
		LTeam teamA = new LTeam(null);
		LTeam teamB = new LTeam(null);

		LGame game = new LGame(teamA, teamB, mScoreA, mScoreB, id);
		return game;

	}
}
