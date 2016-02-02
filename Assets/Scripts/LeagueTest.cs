using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LeagueTest : MonoBehaviour {
        public LeagueTestView view;


	int curGame = 0; //in schedule

	void Awake()
	{
		
	}

	void UpdateView()
	{
		if(GameData.Instance.LeagueData.CurrentSeason.HasNextGame())
			view.SetNextGame(GameData.Instance.LeagueData.GetCurrentGame());
		view.SetSchedule(GameData.Instance.GetSchedule(0), curGame, GameData.Instance.LeagueData.CurrentSeason.Teams);
		view.SetStandings(GameData.Instance.LeagueData.CurrentSeason.Standings);
		view.SetLeagueSchedule(GameData.Instance.LeagueData);
	}

	void Start()
	{
        GameData.Instance.LeagueData = GameData.Instance.Load("test");
        if(GameData.Instance.LeagueData == null)
		UpdateView();
		view.gameObject.SetActive(true);
	}
	
	void Update()
	{
		if((Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0) && GameData.Instance.LeagueData.GetCurrentGame()!=null)
		{
			SimToNextGame();

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
            GameData.Instance.LeagueData = GameData.Instance.Load("test");
			curGame = PlayerPrefs.GetInt("curGame");
			UpdateView();
		}
		if(Input.GetKeyDown(KeyCode.S))
		{
            GameData.Instance.Save("test");
			PlayerPrefs.SetInt("curGame", curGame);
		}
	}

	void GetResult(LGame lgame)	{
		
		UpdateView();		
		SimToNextGame();
	}

	void LoadGameLevel()
	{		
        GameData.Instance.LoadCurrentGameData();     
        SceneManager.LoadScene("Hockey");

	}	

	void SimToNextGame()
	{
		while(GameData.Instance.LeagueData.CurrentSeason.CurGameIndex < GameData.Instance.GetNextGameIndex(0))
		{
			GameData.Instance.LeagueData.GetCurrentGame().SimGame();
            GameData.Instance.LeagueData.CurrentSeason.GamePlayed(GameData.Instance.LeagueData.GetCurrentGame());
		}
	}
}
