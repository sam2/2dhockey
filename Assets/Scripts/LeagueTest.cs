using UnityEngine;
using System.Collections;

public class LeagueTest : MonoBehaviour {

	public League al = new League();
	public GameManager manager;

	public GameObject gamePrefab;
	GameObject game;

	public LeagueTestView view;

	bool inGame = false;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		al = League.CreateNewLeague(8);
		view.SetNextGame(al.mCurrentSeason.GetCurGame());
		view.SetSchedule(al.mCurrentSeason.mUnplayedGames);
		view.SetStandings(al.mCurrentSeason.mStandings);
		view.gameObject.SetActive(true);
	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space) && !inGame)
		{
			inGame = true;
			LoadGameLevel();
		}
		if(Input.GetKeyDown(KeyCode.X) &&!inGame)
		{
			inGame = true;
			LGame game = new LGame(al.mCurrentSeason.GetCurGame().mTeamA, al.mCurrentSeason.GetCurGame().mTeamB);
			game.SimGame();
			GetResult(game);
		}

	}

	void GetResult(LGame g)
	{
		Debug.Log ("Game End: "+g.mTeamA.mName+" - "+g.mScoreA+", "+g.mTeamB.mName+" - "+g.mScoreB+"\n");
		al.mCurrentSeason.GamePlayed(g);
		UpdateStandings(g);
		al.Save();
		view.SetNextGame(al.mCurrentSeason.GetCurGame());
		view.SetSchedule(al.mCurrentSeason.mUnplayedGames);
		view.SetStandings(al.mCurrentSeason.mStandings);
		inGame = false;
		Destroy(game);
		view.gameObject.SetActive(true);
	}

	void UpdateStandings(LGame g)
	{
		LTeam winner = g.GetWinner();
		if(winner!=null)
		{
			winner.mWins++;
			g.GetLoser().mLosses++;
		}
		else
		{
			g.mTeamA.mTies++;
			g.mTeamB.mTies++;
		}
		al.mCurrentSeason.UpdateStandings();

	}

	void LoadGameLevel()
	{
		view.gameObject.SetActive(false);
		game = (GameObject)Instantiate(gamePrefab);
		manager = game.GetComponent<GameManager>();
		manager.GameEnded += new GameEndedHandler(GetResult);
		manager.LoadGame(al.mCurrentSeason.GetCurGame());
		manager.StartGame();

	}
}
