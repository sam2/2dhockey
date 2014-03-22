using UnityEngine;
using System.Collections;

public class LeagueTest : MonoBehaviour {

	public League al = new League();
	public GameManager manager;

	void Start()
	{
		al = League.CreateNewLeague(4);
		LGame game = new LGame(al.mCurrentSeason.mTeams[0], al.mCurrentSeason.mTeams[1]);
		manager.LoadGame(al.mCurrentSeason.GetCurGame());
		manager.StartGame();
		manager.GameEnded += new GameEndedHandler(GetResult);
	}

	void GetResult(LGame g)
	{
		Debug.Log ("Game End: "+g.mTeamA.mName+" - "+g.mScoreA+", "+g.mTeamB.mName+" - "+g.mScoreB+"\n");
		al.mCurrentSeason.GamePlayed(g);

	}


}
