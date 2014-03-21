using UnityEngine;
using System.Collections;

public class LeagueTest : MonoBehaviour {

	public League al = new League();
	public GameManager manager;

	void Start()
	{
		al = League.CreateNewLeague(4);
		LGame game = new LGame(al.mCurrentSeason.mTeams[0], al.mCurrentSeason.mTeams[1]);
		manager.LoadGame(game);
		manager.StartGame();
	}
}
