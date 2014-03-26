using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeagueTestView : MonoBehaviour {

	public GUIText nextGame;
	public GUIText schedule;
	public GUIText standingsdisplay;

	public void SetNextGame(LGame game)
	{

		nextGame.text = "Next Game\n"+game.mTeamA.mName+ " vs " +game.mTeamB.mName;
	}

	public void SetStandings(List<LTeam> standings)
	{
		string text = "Standings\n";
		foreach(LTeam team in standings)
		{
			text+=team.mName+"    "+team.Points()+"\n";
		}
		standingsdisplay.text = text;
	}

	public void SetSchedule(Queue<LGame> games)
	{
		string text = "Schedule\n";
		foreach(LGame game in games)
		{
			text+=game.mTeamA.mName+ " vs " +game.mTeamB.mName+"\n";
		}
		schedule.text = text;
	}
}
