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
	public List<LTeam> mTeams;
	[ProtoMember(2)]
	public List<LGame> mGames;
	[ProtoMember(3)]
	public List<LTeam> mStandings;
	[ProtoMember(4)]
	public int mCurGameIndex = 0;

	public LSeason()
	{
		mTeams = new List<LTeam>();
		mGames = new List<LGame>();
		mStandings = new List<LTeam>();
		mCurGameIndex = 0;
	}

	public LSeason(List<LTeam> teams, List<LGame> games)
	{
		mTeams = teams;
		if(games != null && games.Count > 0)
			mGames = games;
		else
			mGames = CreateRoundRobin(teams.Count);
		mStandings = teams;
	}

	public bool HasNextGame()
	{
		if(mCurGameIndex >= mGames.Count)
			return false;
		return true;
	}

	public void GamePlayed(LGame game)
	{

		if(mCurGameIndex < mGames.Count)
		{
			mGames[mCurGameIndex] = game;
			if(mGames[mCurGameIndex].TeamA_Score > mGames[mCurGameIndex].TeamB_Score)
			{
				mTeams[mGames[mCurGameIndex].TeamA_ID].mWins++;
				mTeams[mGames[mCurGameIndex].TeamB_ID].mLosses++;
			}
			else if(mGames[mCurGameIndex].TeamA_Score < mGames[mCurGameIndex].TeamB_Score)
			{
				mTeams[mGames[mCurGameIndex].TeamB_ID].mWins++;
				mTeams[mGames[mCurGameIndex].TeamA_ID].mLosses++;
			}
			else
			{
				mTeams[mGames[mCurGameIndex].TeamA_ID].mTies++;
				mTeams[mGames[mCurGameIndex].TeamB_ID].mTies++;
			}
			Debug.Log ("Game played: "+mCurGameIndex+" "+mTeams[mGames[mCurGameIndex].TeamA_ID].mName+" vs "+mTeams[mGames[mCurGameIndex].TeamB_ID].mName);
			mCurGameIndex++;
		}
		else
			Debug.Log("No Games left");

		UpdateStandings();

	}

	public void UpdateStandings()
	{
		mStandings = new List<LTeam>();
		foreach(LTeam t in mTeams)
		{
			mStandings.Add (t);
		}
		mStandings.Sort( delegate(LTeam x, LTeam y) 
		                { return (x.Points().CompareTo(y.Points())); }
		);
		mStandings.Reverse();
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
	public LGame GetCurrentGame()
	{
		if(mGames.Count > mCurGameIndex)
			return mGames[mCurGameIndex];
		else return null;
	}
	
	public LGame GetGame(int index)
	{
		return mGames[index];
	}
	
	public LTeam GetTeam(int index)
	{
		return mTeams[index];
	}

	public int GetNextGame(int team)
	{
		for(int i = mCurGameIndex; i < mGames.Count; i++)
		{
			if(mGames[i].TeamA_ID == team || mGames[i].TeamB_ID == team)
				return i;
		}
		return -1;
	}


}


