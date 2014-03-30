using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeagueTestView : MonoBehaviour {

	public GUIText nextGame;
	public GUIText schedule;
	public GUIText standingsdisplay;

	public void SetNextGame(LGame game)
	{

		nextGame.text = game.mTeamA+ " vs " +game.mTeamB;
	}

	public void SetStandings(List<LTeam> standings)
	{
		string text = "Standings\n";
		foreach(LTeam team in standings)
		{
			text+=team.mName+"    "+team.Points()+"  ("+team.mWins+"-"+team.mLosses+"-"+team.mTies+")\n";
		}
		standingsdisplay.text = text;
	}

	public void SetSchedule(List<LGame> games, int curGame)
	{
		string text = "Upcoming Games\n";
		for(int  i = 0; i < games.Count; i++)
		{
			text+=games[i].mTeamA+ " vs " +games[i].mTeamB;
			if(i == curGame)
				text+=" <--- ";
			else if(i < curGame)
			{
				text+=" ("+games[i].mScoreA+" - "+games[i].mScoreB+")";
			}
			text+='\n';
		}
		schedule.text = text;
	}
}
