using UnityEngine;
using ProtoBuf;
using System;
using System.Collections;
using System.IO;

public class GameData : MonoBehaviour {

    public static GameData Instance;

    public LGame Game;
    public LTeam TeamA;
    public LTeam TeamB;

    public bool HasData;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            Game = new LGame();
            TeamA = new LTeam();
            TeamB = new LTeam();
            HasData = false;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this);
    }


    public void SetGameData(LGame game, LTeam teamA, LTeam teamB)
    {
        Game = game;
        TeamA = teamA;
        TeamB = teamB;
        HasData = true;
    }

    public void ClearGameData()
    {
        Game = new LGame();
        TeamA = new LTeam();
        TeamB = new LTeam();
        HasData = false;
    }



    public void Save(League league, string leagueName)
    {
        MemoryStream msString = new MemoryStream();
        Serializer.Serialize<League>(msString, league);
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
}
