using UnityEngine;
using System.Collections;

public class LeagueManager : MonoBehaviour {

	public LeagueManager Instance;
	
	public League league;
	// Use this for initialization
	void Awake () {
		Instance = this;
	}

	void Init()
	{
		league = League.CreateNewLeague(8);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
