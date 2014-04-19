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

	void UpdateView()
	{
		if(al.mCurrentSeason.HasNextGame())
			view.SetNextGame(al.mCurrentSeason.mGames[al.mCurrentSeason.mCurGameIndex]);
		view.SetSchedule(al.mCurrentSeason.mGames, al.mCurrentSeason.mCurGameIndex);
		view.SetStandings(al.mCurrentSeason.mStandings);
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
			if(Input.GetKeyDown(KeyCode.Space) && al.mCurrentSeason.GetCurrentGame()!=null)
			{
				inGame = true;
				LoadGameLevel();
			}
			if(Input.GetKeyDown(KeyCode.X) && al.mCurrentSeason.GetCurrentGame()!=null)
			{
				inGame = true;
				al.mCurrentSeason.GetCurrentGame().SimGame();
				GetResult(al.mCurrentSeason.GetCurrentGame());
			}
			if(Input.GetKeyDown(KeyCode.L))
			{
				al = al.Load();
				UpdateView();
			}
			if(Input.GetKeyDown(KeyCode.S))
			{
				al.Save();
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
}
