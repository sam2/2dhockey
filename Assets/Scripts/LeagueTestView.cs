using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeagueTestView : MonoBehaviour {

	public GUIText nextGame;
	public GUIText schedule;
	public GUIText leagueSchedule;
	public GUIText standingsdisplay;

	public void SetNextGame(LGame game)
	{

		nextGame.text = game.TeamA_ID+ " vs " +game.TeamB_ID;
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

	public void SetSchedule(List<LGame> games, int curGame, List<LTeam> teams)
	{
		string text = "Team Schedule\n";
		for(int  i = 0; i < games.Count; i++)
		{
			text+=teams[games[i].TeamA_ID].mName+ " vs " +teams[games[i].TeamB_ID].mName;
			if(i == curGame)
				text+=" <--- ";
			else if(i < curGame)
			{
				text+=" ("+games[i].TeamA_Score+" - "+games[i].TeamB_Score+")";
			}
			text+='\n';
		}
		schedule.text = text;
	}

	public void SetLeagueSchedule(League league)
	{
		string text = "League Schedule\n";
		List<LGame> games = league.CurrentSeason.Games;
		int curGame = league.CurrentSeason.CurGameIndex;
		List<LTeam> teams = league.CurrentSeason.Teams;
		for(int  i = 0; i < games.Count; i++)
		{
			text+=teams[games[i].TeamA_ID].mName+ " vs " +teams[games[i].TeamB_ID].mName;
			if(i == curGame)
				text+=" <--- ";
			else if(i < curGame)
			{
				text+=" ("+games[i].TeamA_Score+" - "+games[i].TeamB_Score+")";
			}
			text+='\n';
		}
		leagueSchedule.text = text;
	}
}
