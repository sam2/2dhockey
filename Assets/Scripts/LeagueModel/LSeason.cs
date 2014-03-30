﻿using UnityEngine;
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

	public void GamePlayed(bool sim)
	{

		if(mCurGameIndex < mGames.Count)
		{
			if(sim)
			{
				mGames[mCurGameIndex].SimGame();
			}
			if(mGames[mCurGameIndex].mScoreA > mGames[mCurGameIndex].mScoreB)
			{
				mTeams[mGames[mCurGameIndex].mTeamA].mWins++;
				mTeams[mGames[mCurGameIndex].mTeamB].mLosses++;
			}
			else if(mGames[mCurGameIndex].mScoreA < mGames[mCurGameIndex].mScoreB)
			{
				mTeams[mGames[mCurGameIndex].mTeamB].mWins++;
				mTeams[mGames[mCurGameIndex].mTeamA].mLosses++;
			}
			else
			{
				mTeams[mGames[mCurGameIndex].mTeamA].mTies++;
				mTeams[mGames[mCurGameIndex].mTeamB].mTies++;
			}
			Debug.Log ("Game played: "+mCurGameIndex+" "+mTeams[mGames[mCurGameIndex].mTeamA].mName+" vs "+mTeams[mGames[mCurGameIndex].mTeamB].mName);
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



}


