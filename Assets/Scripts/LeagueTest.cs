using UnityEngine;
using System.Collections;

public class LeagueTest : MonoBehaviour {

	public League al = new League();
	public GameManager manager;

	public GameObject gamePrefab;
	GameObject game;

	public LeagueTestView view;

	bool inGame = false;


	//debug:
	League templeague = new League();

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
			if(Input.GetKeyDown(KeyCode.Space))
			{
				inGame = true;
				LoadGameLevel();
			}
			if(Input.GetKeyDown(KeyCode.X))
			{
				inGame = true;
				al.mCurrentSeason.GamePlayed(true);
				GetResult();

			}
			if(Input.GetKeyDown(KeyCode.L))
			{
				templeague = al;
				al = al.Load();
				UpdateView();
			}
			if(Input.GetKeyDown(KeyCode.S))
			{
				al.Save();
			}
		}

	}

	void GetResult()
	{
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
		manager.LoadGame(al.mCurrentSeason);
		manager.StartGame();

	}
}
