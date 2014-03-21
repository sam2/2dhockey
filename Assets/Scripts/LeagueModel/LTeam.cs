using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LTeam
{
	//public int ID;
	public string mName;
	public List<LPlayer> mRoster;

	public LTeam(string name, List<LPlayer> roster)
	{
		//ID = id;
		mName = name;
		mRoster = roster;
	}

}
