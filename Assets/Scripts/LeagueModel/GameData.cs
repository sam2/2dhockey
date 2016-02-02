using UnityEngine;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;

public class GameData : Singleton<GameData> {

    public League LeagueData; //TODO: make private once refactor done
    public LGame CurrentGame;

    void Awake()
    {
        if(LeagueData == null)
        {
            LeagueData = League.CreateNewLeague(2);
            LoadCurrentGameData();
        }
    }

    public void LoadCurrentGameData()
    {
        CurrentGame = GameData.Instance.LeagueData.GetCurrentGame();
    }

    public void ClearGameData()
    {
        CurrentGame = null;
    }

    public LTeam GetTeam(int teamID)
    {
        return LeagueData.CurrentSeason.Teams[teamID];
    }

    //using playerprefs not performant. TODO: write save system.
    public void Save(string leagueName)
    {
        MemoryStream msString = new MemoryStream();
        Serializer.Serialize<League>(msString, LeagueData);
        string strbase64 = Convert.ToBase64String(msString.ToArray());
        PlayerPrefs.SetString(leagueName, strbase64);
    }

    public League Load(string leagueName)
    {
        League l = null;
        if (PlayerPrefs.GetString(leagueName).Length > 5)
        {
            byte[] byteAfter64 = Convert.FromBase64String(PlayerPrefs.GetString(leagueName));
            MemoryStream afterStream = new MemoryStream(byteAfter64);
            l = Serializer.Deserialize<League>(afterStream);
        }
        return l;
    }

    public List<LGame> GetSchedule(int team)
    {
        List<LGame> sched = new List<LGame>();
        foreach (LGame game in LeagueData.CurrentSeason.Games)
        {
            if (game.TeamA_ID == team || game.TeamB_ID == team)
            {
                sched.Add(game);
            }
        }
        return sched;
    }

    public int GetNextGameIndex(int team)
    {
        for (int i = LeagueData.CurrentSeason.CurGameIndex; i < LeagueData.CurrentSeason.Games.Count; i++)
        {
            if (LeagueData.CurrentSeason.Games[i].TeamA_ID == team || LeagueData.CurrentSeason.Games[i].TeamB_ID == team)
            {
                return i;
            }
        }
        return -1;
    }
}
