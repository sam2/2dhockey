using System.Collections;
using ProtoBuf;
using System;

[Serializable]
[ProtoContract]
public class LGame
{
	[ProtoMember(1)]
	public int mTeamA;
	[ProtoMember(2)]
	public int mTeamB;
	
	[ProtoMember(3)]
	public int mScoreA;
	[ProtoMember(4)]
	public int mScoreB;
	
	public LGame()
	{
		mTeamA = 0;
		mTeamB = 0;
		mScoreA = 0;
		mScoreB = 0;
	}

	public LGame(int a, int b)
	{
		mTeamA = a;
		mTeamB = b;
		mScoreA = 0;
		mScoreB = 0;
	}
	
	
	int GetWinner()
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
			return -1;
	}
	
	int GetLoser()
	{
		if(mScoreA < mScoreB)
		{
			return mTeamA;
		}
		else if(mScoreB < mScoreA)
		{
			return mTeamB;
		}
		else
			return -1;
	}
	
	public void SimGame()
	{
		mScoreA = UnityEngine.Random.Range(0,6);
		mScoreB = UnityEngine.Random.Range(0,6);
	}
	
	
}