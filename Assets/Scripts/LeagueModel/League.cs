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
	public List<LSeason> mSeasons;
	[ProtoMember(2)]
	public LSeason mCurrentSeason;

	public League()
	{
		mSeasons = new List<LSeason>();
		mCurrentSeason = new LSeason();
	}

	public League(List<LSeason> seasons, LSeason currentSeason)
	{
		mSeasons = seasons;
		mCurrentSeason = currentSeason;
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

	public void Save()
	{
		MemoryStream msString = new MemoryStream();
		Serializer.Serialize<League>(msString, this);
		Debug.Log("Saving...");
		string strbase64 = Convert.ToBase64String(msString.ToArray());
		PlayerPrefs.SetString("save", strbase64);

	}

	public League Load()
	{
		League l = new League();
		if(PlayerPrefs.GetString("save").Length > 5)
		{
			byte[] byteAfter64 = Convert.FromBase64String(PlayerPrefs.GetString("save"));
			MemoryStream afterStream = new MemoryStream(byteAfter64);
			Debug.Log("Loading...");
			l = Serializer.Deserialize<League>(afterStream);
		}
		return l;

	}

}
