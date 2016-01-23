using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LeagueTest : MonoBehaviour {

	public League LeagueData = new League();
	public GameManager GameManager;

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
		if(LeagueData.CurrentSeason.HasNextGame())
			view.SetNextGame(LeagueData.CurrentSeason.mGames[LeagueData.CurrentSeason.mCurGameIndex]);
		view.SetSchedule(GetSchedule(0), curGame, LeagueData.CurrentSeason.mTeams);
		view.SetStandings(LeagueData.CurrentSeason.mStandings);
		view.SetLeagueSchedule(LeagueData);
	}

	void Start()
	{
		LeagueData = League.CreateNewLeague(8);
		UpdateView();
		view.gameObject.SetActive(true);
	}
	
	void Update()
	{
		if(!inGame)
		{
			if((Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0) && LeagueData.CurrentSeason.GetCurrentGame()!=null)
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
				LeagueData = LeagueData.Load();
				curGame = PlayerPrefs.GetInt("curGame");
				UpdateView();
			}
			if(Input.GetKeyDown(KeyCode.S))
			{
				LeagueData.Save();
				PlayerPrefs.SetInt("curGame", curGame);
			}
		}

	}

	void GetResult(LGame lgame)
	{
		LeagueData.CurrentSeason.GamePlayed(lgame);
		view.gameObject.SetActive(true);
		UpdateView();
		inGame = false;
		Destroy(game);
		SimToNextGame();
	}

	void LoadGameLevel()
	{		
		LGame lgame = LeagueData.CurrentSeason.GetCurrentGame();       
        GameData.Instance.Game = lgame;
        GameData.Instance.TeamA = LeagueData.CurrentSeason.GetTeam(lgame.TeamA_ID);
        GameData.Instance.TeamB = LeagueData.CurrentSeason.GetTeam(lgame.TeamB_ID);
        SceneManager.LoadScene("Hockey");

	}

	int GetNextGame(int team)
	{
		for(int i = LeagueData.CurrentSeason.mCurGameIndex; i < LeagueData.CurrentSeason.mGames.Count; i++)
		{
			if(LeagueData.CurrentSeason.mGames[i].TeamA_ID == team || LeagueData.CurrentSeason.mGames[i].TeamB_ID == team)
			{
				return i;
			}
		}
		return -1;
	}

	List<LGame> GetSchedule(int team)
	{
		List<LGame> sched = new List<LGame>();
		foreach(LGame game in LeagueData.CurrentSeason.mGames)
		{
			if(game.TeamA_ID == team || game.TeamB_ID == team)
			{
				sched.Add(game);
			}
		}
		return sched;
	}

	void SimToNextGame()
	{
		while(LeagueData.CurrentSeason.mCurGameIndex < GetNextGame(0))
		{
			LeagueData.CurrentSeason.GetCurrentGame().SimGame();
			GetResult(LeagueData.CurrentSeason.GetCurrentGame());
		}
	}
}
