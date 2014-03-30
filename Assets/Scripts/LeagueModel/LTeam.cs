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
	public int mWins;
	[ProtoMember(4)]
	public int mLosses;
	[ProtoMember(5)]
	public int mTies;

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
		return(mWins*2 + mTies);
	}

	public void WinGame()
	{
		mWins++;
	}

	public void LoseGame()
	{
		mLosses++;
	}

	public void TieGame()
	{
		mTies++;
	}




}
