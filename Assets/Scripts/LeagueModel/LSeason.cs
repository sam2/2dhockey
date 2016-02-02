using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;
using System;

[Serializable]
[ProtoContract]
public class LSeason
{
	[ProtoMember(1)]
	public List<LTeam> Teams;
	[ProtoMember(2)]
	public List<LGame> Games;
	[ProtoMember(3)]
	public List<LTeam> Standings;
	[ProtoMember(4)]
	public int CurGameIndex = 0;

	public LSeason()
	{
		Teams = new List<LTeam>();
		Games = new List<LGame>();
		Standings = new List<LTeam>();
		CurGameIndex = 0;
	}

	public LSeason(List<LTeam> teams, List<LGame> games)
	{
		Teams = teams;
		if(games != null && games.Count > 0)
			Games = games;
		else
			Games = CreateRoundRobin(teams.Count);
		Standings = teams;
	}

	public bool HasNextGame()
	{
		if(CurGameIndex >= Games.Count)
			return false;
		return true;
	}

	public void GamePlayed(LGame game)
	{

		if(CurGameIndex < Games.Count)
		{
			Games[CurGameIndex] = game;
			if(Games[CurGameIndex].TeamA_Score > Games[CurGameIndex].TeamB_Score)
			{
				Teams[Games[CurGameIndex].TeamA_ID].Wins++;
				Teams[Games[CurGameIndex].TeamB_ID].Losses++;
			}
			else if(Games[CurGameIndex].TeamA_Score < Games[CurGameIndex].TeamB_Score)
			{
				Teams[Games[CurGameIndex].TeamB_ID].Wins++;
				Teams[Games[CurGameIndex].TeamA_ID].Losses++;
			}
			else
			{
				Teams[Games[CurGameIndex].TeamA_ID].Ties++;
				Teams[Games[CurGameIndex].TeamB_ID].Ties++;
			}
			Debug.Log ("Game played: "+CurGameIndex+" "+Teams[Games[CurGameIndex].TeamA_ID].mName+" vs "+Teams[Games[CurGameIndex].TeamB_ID].mName);
			CurGameIndex++;
		}
		else
			Debug.Log("No Games left");

		UpdateStandings();

	}

	public void UpdateStandings()
	{
		Standings = new List<LTeam>();
		foreach(LTeam t in Teams)
		{
			Standings.Add (t);
		}
		Standings.Sort( delegate(LTeam x, LTeam y) 
		                { return (x.Points().CompareTo(y.Points())); }
		);
		Standings.Reverse();
	}

	public static List<LGame> CreateRoundRobin(int numTeams)
	{
		List<LGame> games = new List<LGame>();
		for(int i = 0; i < numTeams; i++)
		{
			for(int j = i+1; j < numTeams; j++)
			{
				LGame game = new LGame(i, j);
				games.Add(game);
			}
		}
		Shuffle (games);
		return games;
	}
	public static void Shuffle<T>(IList<T> list)  
	{  
		System.Random rng = new System.Random();  
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = rng.Next(n + 1);  
			T value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}  
	}
	
	public LGame GetGame(int index)
	{
		return Games[index];
	}
	
	public LTeam GetTeam(int index)
	{
		return Teams[index];
	}

	public int GetNextGame(int team)
	{
		for(int i = CurGameIndex; i < Games.Count; i++)
		{
			if(Games[i].TeamA_ID == team || Games[i].TeamB_ID == team)
				return i;
		}
		return -1;
	}


}


