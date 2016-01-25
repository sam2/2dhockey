using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;
using System;

[Serializable]
[ProtoContract]
public class LTeam
{
	[ProtoMember(1)]
	public string mName;
	[ProtoMember(2)]
	public List<LPlayer> mRoster;
	[ProtoMember(3)]
	public int Wins;
	[ProtoMember(4)]
	public int Losses;
	[ProtoMember(5)]
	public int Ties;

	public LTeam()
	{
		//ID = id;
		mName = "teamName";
		mRoster = new List<LPlayer>();
		//mWins = 0;
		//mLosses = 0;
		//mTies = 0;
	}

	public LTeam(string name, List<LPlayer> roster)
	{
		//ID = id;
		mName = name;
		mRoster = roster;		 
	}

	public int Points()
	{
		return(Wins*2 + Ties);
	}

	public void WinGame()
	{
		Wins++;
	}

	public void LoseGame()
	{
		Losses++;
	}

	public void TieGame()
	{
		Ties++;
	}




}
