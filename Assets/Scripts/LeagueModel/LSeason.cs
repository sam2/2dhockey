using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LSeason
{
	public List<LTeam> mTeams;

	public Queue<LGame> mUnplayedGames;
	public Queue<LGame> mPlayedGames;
	LGame _curGame;

	public LSeason(List<LTeam> teams, Queue<LGame> games)
	{
		mTeams = teams;
		if(games != null && games.Count > 0)
			mUnplayedGames = games;
		else
			mUnplayedGames = LSeason.CreateRoundRobin(teams);
		mPlayedGames = new Queue<LGame>();
		_curGame = mUnplayedGames.Dequeue();
	}

	public void GamePlayed(LGame game)
	{
		if(_curGame != null)
		{
			_curGame = mUnplayedGames.Dequeue();
			mPlayedGames.Enqueue(game);
		}
		else
			Debug.LogError("_curGame is null");
	}

	public LGame GetCurGame()
	{
		return _curGame;
	}

	public static Queue<LGame> CreateRoundRobin(List<LTeam> ListTeam)
	{
		Queue<LGame> games = new Queue<LGame>();

		if (ListTeam.Count % 2 != 0)
		{
			return null;
		}
		int numTeams = ListTeam.Count;
		int numDays = (numTeams - 1);
		int halfSize = numTeams / 2;
		
		List<LTeam> teams = new List<LTeam>();
		
		teams.AddRange(ListTeam); // Copy all the elements.
		teams.RemoveAt(0);
		
		int teamsSize = teams.Count;
		
		for (int day = 0; day < numDays; day++)
		{
			//Debug.Log("Day "+(day + 1));
			
			int teamIdx = day % teamsSize;
			
			//Debug.Log(teams[teamIdx].mName+" VS "+ListTeam[0].mName);
			LGame game = new LGame(teams[teamIdx], ListTeam[0]);
			games.Enqueue(game);
			
			for (int idx = 1; idx < halfSize; idx++)
			{   			
				int firstTeam = (day + idx) % teamsSize;
				int secondTeam = (day  + teamsSize - idx) % teamsSize;
				//Debug.Log(teams[firstTeam].mName+" VS "+teams[secondTeam].mName);
				game = new LGame(teams[firstTeam], teams[secondTeam]);
				games.Enqueue(game);
			}
		}
		return games;
	}

}


