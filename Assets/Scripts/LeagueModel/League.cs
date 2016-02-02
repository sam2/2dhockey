using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;
using System.IO;
using System;

[Serializable]
[ProtoContract]
public class League 
{
	[ProtoMember(1)]
	public List<LSeason> Seasons;
	[ProtoMember(2)]
	public LSeason CurrentSeason;

    public League()
	{
		Seasons = new List<LSeason>();
		CurrentSeason = new LSeason();
	}

	public League(List<LSeason> seasons, LSeason currentSeason)
	{
		Seasons = seasons;
		CurrentSeason = currentSeason;
	}

    public LGame GetCurrentGame()
    {
        if (CurrentSeason.Games.Count > CurrentSeason.CurGameIndex)
            return CurrentSeason.Games[CurrentSeason.CurGameIndex];
        else return null;
    }

    public static League CreateNewLeague(int numTeams)
	{
		List<LTeam> teams = new List<LTeam>();
		List<LPlayer> roster = new List<LPlayer>();
		for(int i = 0; i < 5; i++)
		{
			LPlayer player = new LPlayer();
			roster.Add (player);
		}
		teams.Add(new LTeam("Hometeam",roster));
		for(int j = 1; j < numTeams; j++)
		{
			roster = new List<LPlayer>();
			for(int i = 0; i < 5; i++)
			{
				LPlayer player = new LPlayer();
				roster.Add (player);
			}
			teams.Add(new LTeam("newTeam"+j,roster));
		}

		LSeason season = new LSeason(teams, null);
		List<LSeason> seasons = new List<LSeason>();
		seasons.Add(season);
		League league = new League(seasons, season);
		return league;
	}

	

}
