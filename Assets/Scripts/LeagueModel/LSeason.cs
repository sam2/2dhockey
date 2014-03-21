using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LSeason
{
	public List<LTeam> mTeams;
	public List<LGame> mGames;

	public LSeason(List<LTeam> teams)
	{
		mTeams = teams;
	}

	public void AddGame(LGame game)
	{
		mGames.Add(game);
	}
}
