using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class League 
{
	public List<LSeason> mSeasons;
	public LSeason mCurrentSeason;

	public League()
	{
		mSeasons = null;
		mCurrentSeason = null;
	}

	public League(List<LSeason> seasons, LSeason currentSeason)
	{
		mSeasons = seasons;
		mCurrentSeason = currentSeason;
	}

	public static League CreateNewLeague(int numTeams)
	{
		List<LTeam> teams = new List<LTeam>();
		for(int j = 0; j < numTeams; j++)
		{
			List<LPlayer> roster = new List<LPlayer>();
			for(int i = 0; i < 5; i++)
			{
				LPlayer player = new LPlayer(i);
				roster.Add (player);
			}
			teams.Add(new LTeam("newTeam"+(j+1),roster));
		}

		LSeason season = new LSeason(teams, null);
		List<LSeason> seasons = new List<LSeason>();
		seasons.Add(season);
		League league = new League(seasons, season);
		return league;
	}

}
