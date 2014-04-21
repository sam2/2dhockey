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

	public void SetSchedule(List<LGame> games, int curGame, List<LTeam> teams)
	{
		string text = "Team Schedule\n";
		for(int  i = 0; i < games.Count; i++)
		{
			text+=teams[games[i].mTeamA].mName+ " vs " +teams[games[i].mTeamB].mName;
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

	public void SetLeagueSchedule(League league)
	{
		string text = "League Schedule\n";
		List<LGame> games = league.mCurrentSeason.mGames;
		int curGame = league.mCurrentSeason.mCurGameIndex;
		List<LTeam> teams = league.mCurrentSeason.mTeams;
		for(int  i = 0; i < games.Count; i++)
		{
			text+=teams[games[i].mTeamA].mName+ " vs " +teams[games[i].mTeamB].mName;
			if(i == curGame)
				text+=" <--- ";
			else if(i < curGame)
			{
				text+=" ("+games[i].mScoreA+" - "+games[i].mScoreB+")";
			}
			text+='\n';
		}
		leagueSchedule.text = text;
	}
}
