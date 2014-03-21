using UnityEngine;
using System.Collections;

public class LGame
{
	public LTeam mTeamA;
	public LTeam mTeamB;

	int mScoreA;
	int mScoreB;

	public LGame(LTeam teamA, LTeam teamB, int scoreA, int scoreB)
	{
		mTeamA = teamA;
		mTeamB = teamB;
		mScoreA = scoreA;
		mScoreB = scoreB;
	}

	public LGame(LTeam teamA, LTeam teamB)
	{
		mTeamA = teamA;
		mTeamB = teamB;
		mScoreA = 0;
		mScoreB = 0;
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
