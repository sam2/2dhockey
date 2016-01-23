using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {

    public static GameData Instance;

    public LGame Game;
    public LTeam TeamA;
    public LTeam TeamB;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            Game = new LGame();
            TeamA = new LTeam();
            TeamB = new LTeam();
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this);
    }
}
