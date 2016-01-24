using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LeagueTest : MonoBehaviour {

	public League LeagueData = new League();
	public GameManager GameManager;

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
			view.SetNextGame(LeagueData.CurrentSeason.Games[LeagueData.CurrentSeason.CurGameIndex]);
		view.SetSchedule(GetSchedule(0), curGame, LeagueData.CurrentSeason.Teams);
		view.SetStandings(LeagueData.CurrentSeason.Standings);
		view.SetLeagueSchedule(LeagueData);
	}

	void Start()
	{
        LeagueData = GameData.Instance.Load("test");
        if(LeagueData == null)
            LeagueData = League.CreateNewLeague(8);
        if(GameData.Instance.HasData)
        {
            GetResult(GameData.Instance.Game);
        }
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
                LeagueData = GameData.Instance.Load("test");
				curGame = PlayerPrefs.GetInt("curGame");
				UpdateView();
			}
			if(Input.GetKeyDown(KeyCode.S))
			{
                GameData.Instance.Save(LeagueData, "test");
				PlayerPrefs.SetInt("curGame", curGame);
			}
		}

	}

	void GetResult(LGame lgame)
	{
		LeagueData.CurrentSeason.GamePlayed(lgame);
		UpdateView();		
		SimToNextGame();
	}

	void LoadGameLevel()
	{		
		LGame lgame = LeagueData.CurrentSeason.GetCurrentGame();
        GameData.Instance.SetGameData(lgame, LeagueData.CurrentSeason.GetTeam(lgame.TeamA_ID), LeagueData.CurrentSeason.GetTeam(lgame.TeamB_ID));     
        SceneManager.LoadScene("Hockey");

	}

	int GetNextGame(int team)
	{
		for(int i = LeagueData.CurrentSeason.CurGameIndex; i < LeagueData.CurrentSeason.Games.Count; i++)
		{
			if(LeagueData.CurrentSeason.Games[i].TeamA_ID == team || LeagueData.CurrentSeason.Games[i].TeamB_ID == team)
			{
				return i;
			}
		}
		return -1;
	}

	List<LGame> GetSchedule(int team)
	{
		List<LGame> sched = new List<LGame>();
		foreach(LGame game in LeagueData.CurrentSeason.Games)
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
		while(LeagueData.CurrentSeason.CurGameIndex < GetNextGame(0))
		{
			LeagueData.CurrentSeason.GetCurrentGame().SimGame();
			GetResult(LeagueData.CurrentSeason.GetCurrentGame());
		}
	}
}
