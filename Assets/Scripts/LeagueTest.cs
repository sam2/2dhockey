using UnityEngine;
using System.Collections;

public class LeagueTest : MonoBehaviour {

	public League al = new League();
	public GameManager manager;

	public LTeam[] standings;
	public LeagueTestView view;

	void Start()
	{
		al = League.CreateNewLeague(4);
		LGame game = new LGame(al.mCurrentSeason.mTeams[0], al.mCurrentSeason.mTeams[1]);
		manager.LoadGame(al.mCurrentSeason.GetCurGame());
		manager.StartGame();
		manager.GameEnded += new GameEndedHandler(GetResult);
		view.SetNextGame(al.mCurrentSeason.GetCurGame());
		view.SetSchedule(al.mCurrentSeason.mUnplayedGames);
		view.SetStandings(al.mCurrentSeason.mStandings);
		view.gameObject.SetActive(false);
	}

	bool viewOn = false;
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.X))
		{
			viewOn = !viewOn;
			view.gameObject.SetActive(viewOn);

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
		standings = al.mCurrentSeason.mStandings.ToArray();

	}
}
