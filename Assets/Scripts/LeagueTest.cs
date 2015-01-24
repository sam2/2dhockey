using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeagueTest : MonoBehaviour {

	public League al = new League();
	public GameManager manager;

	public GameObject gamePrefab;
	GameObject game;

	public LeagueTestView view;

	bool inGame = false;


	int curGame = 0; //in schedule

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	void UpdateView()
	{
		if(al.mCurrentSeason.HasNextGame())
			view.SetNextGame(al.mCurrentSeason.mGames[al.mCurrentSeason.mCurGameIndex]);
		view.SetSchedule(GetSchedule(0), curGame, al.mCurrentSeason.mTeams);
		view.SetStandings(al.mCurrentSeason.mStandings);
		view.SetLeagueSchedule(al);
	}

	void Start()
	{
		al = League.CreateNewLeague(8);
		UpdateView();
		view.gameObject.SetActive(true);
	}
	
	void Update()
	{
		if(!inGame)
		{
			if((Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0) && al.mCurrentSeason.GetCurrentGame()!=null)
			{
				SimToNextGame();
				inGame = true;
				LoadGameLevel();
				curGame++;
			}
			/*
			if(Input.GetKeyDown(KeyCode.X) && al.mCurrentSeason.GetCurrentGame()!=null)
			{
				inGame = true;
				al.mCurrentSeason.GetCurrentGame().SimGame();
				GetResult(al.mCurrentSeason.GetCurrentGame());
			}
			*/
			if(Input.GetKeyDown(KeyCode.L))
			{
				al = al.Load();
				curGame = PlayerPrefs.GetInt("curGame");
				UpdateView();
			}
			if(Input.GetKeyDown(KeyCode.S))
			{
				al.Save();
				PlayerPrefs.SetInt("curGame", curGame);
			}
		}

	}

	void GetResult(LGame lgame)
	{
		al.mCurrentSeason.GamePlayed(lgame);
		view.gameObject.SetActive(true);
		UpdateView();
		inGame = false;
		Destroy(game);
		SimToNextGame();
	}

	void LoadGameLevel()
	{
		view.gameObject.SetActive(false);
		game = (GameObject)Instantiate(gamePrefab);
		manager = game.GetComponent<GameManager>();
		manager.GameEnded += new GameEndedHandler(GetResult);
		LGame lgame = al.mCurrentSeason.GetCurrentGame();
		manager.LoadGame(lgame, al.mCurrentSeason.GetTeam(lgame.mTeamA), al.mCurrentSeason.GetTeam(lgame.mTeamB));

	}

	int GetNextGame(int team)
	{
		for(int i = al.mCurrentSeason.mCurGameIndex; i < al.mCurrentSeason.mGames.Count; i++)
		{
			if(al.mCurrentSeason.mGames[i].mTeamA == team || al.mCurrentSeason.mGames[i].mTeamB == team)
			{
				return i;
			}
		}
		return -1;
	}

	List<LGame> GetSchedule(int team)
	{
		List<LGame> sched = new List<LGame>();
		foreach(LGame game in al.mCurrentSeason.mGames)
		{
			if(game.mTeamA == team || game.mTeamB == team)
			{
				sched.Add(game);
			}
		}
		return sched;
	}

	void SimToNextGame()
	{
		while(al.mCurrentSeason.mCurGameIndex < GetNextGame(0))
		{
			al.mCurrentSeason.GetCurrentGame().SimGame();
			GetResult(al.mCurrentSeason.GetCurrentGame());
		}
	}
}
