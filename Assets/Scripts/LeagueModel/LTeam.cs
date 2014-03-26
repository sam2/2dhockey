using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LTeam
{
	//public int ID;
	public string mName;
	public List<LPlayer> mRoster;

	public int mWins;
	public int mLosses;
	public int mTies;

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



}
