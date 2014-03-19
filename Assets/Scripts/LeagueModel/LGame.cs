using UnityEngine;
using System.Collections;

public class LGame
{
	public readonly int ID;

	LTeam mTeamA;
	LTeam mTeamB;

	int mScoreA;
	int mScoreB;

	public LGame(LTeam teamA, LTeam teamB, int scoreA, int scoreB, int id)
	{
		mTeamA = teamA;
		mTeamB = teamB;
		mScoreA = scoreA;
		mScoreB = scoreB;
		ID = id;
	}

	public LTeam GetWinner()
	{
		if(mScoreA > mScoreB)
		{
			return mTeamA;
		}
		else if(mScoreB > mScoreA)
		{
			return mTeamB;
		}
		else
			return null;
	}


}
