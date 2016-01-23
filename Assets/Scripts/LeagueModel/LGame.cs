using System.Collections;
using ProtoBuf;
using System;

[Serializable]
[ProtoContract]
public class LGame
{
	[ProtoMember(1)]
	public int TeamA_ID;
	[ProtoMember(2)]
	public int TeamB_ID;
	
	[ProtoMember(3)]
	public int TeamA_Score;
	[ProtoMember(4)]
	public int TeamB_Score;
	
	public LGame()
	{
		TeamA_ID = 0;
		TeamB_ID = 0;
		TeamA_Score = 0;
		TeamB_Score = 0;
	}

	public LGame(int a, int b)
	{
		TeamA_ID = a;
		TeamB_ID = b;
		TeamA_Score = 0;
		TeamB_Score = 0;
	}
	
	
	int GetWinner()
	{
		if(TeamA_Score > TeamB_Score)
		{
			return TeamA_ID;
		}
		else if(TeamB_Score > TeamA_Score)
		{
			return TeamB_ID;
		}
		else
			return -1;
	}
	
	int GetLoser()
	{
		if(TeamA_Score < TeamB_Score)
		{
			return TeamA_ID;
		}
		else if(TeamB_Score < TeamA_Score)
		{
			return TeamB_ID;
		}
		else
			return -1;
	}
	
	public void SimGame()
	{
		TeamA_Score = UnityEngine.Random.Range(0,6);
		TeamB_Score = UnityEngine.Random.Range(0,6);
	}
	
	
}