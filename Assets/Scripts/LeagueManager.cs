using UnityEngine;
using System.Collections;

public class LeagueManager : MonoBehaviour {
	/*
	public League league = new League();
	public GameManager manager;
	
	public GameObject gamePrefab;
	GameObject game;
	

	
	bool inGame = false;
	
	
	int team = 0;
	
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	
	void Start()
	{
		league = League.CreateNewLeague(8);
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

	void GetCurrentGame()
	{

	}
	*/
}
