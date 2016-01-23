using UnityEngine;
using System.Collections;

public class Profile {

	League mLeague;
	int mTeam;
	public Profile(League league, int team)
	{
		mLeague = league;
		mTeam = team;
	}

	public int GetNextGame()
	{
		return mLeague.CurrentSeason.GetNextGame(mTeam);
	}

	 
}
