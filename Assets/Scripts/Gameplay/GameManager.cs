using UnityEngine;
using System.Collections;

public delegate void GameEndedHandler(LGame game);

public class GameManager : MonoBehaviour
{
	public GameObject netPrefab;

	public static GoalZone leftGoal;
	public static GoalZone rightGoal;

	public Team TeamA; //left team
	public Team TeamB; //right team
	public GameObject TeamAPrefab;
	public GameObject TeamBPrefab;

	public float GAME_LENGTH;
	public float timeLeft;
	public GameView view;

	FiniteStateMachine<GameManager> fsm;
	public GMFaceoffState gmFaceoffState = new GMFaceoffState();
	public GMPlayState gmPlayState = new GMPlayState();
	public GMEndGameState gmEndGameState = new GMEndGameState();

	bool mLoading = false;

	public void ChangeState(FSMState<GameManager> state)
	{
		fsm.ChangeState(state);
	}


	void CreateTeams(LTeam lteamA, LTeam lteamB)
	{
		GameObject gA = (GameObject)Instantiate(TeamAPrefab);
		TeamA = gA.GetComponent<Team>();
		gA.transform.parent = this.transform;

		GameObject gB = (GameObject)Instantiate(TeamBPrefab);
		TeamB = gB.GetComponent<Team>();
		gB.transform.parent = this.transform;

		TeamA.opponent = TeamB;
		TeamB.opponent = TeamA;
		TeamA.SpawnPlayers(lteamA,5);
		TeamB.SpawnPlayers(lteamB,5);
		TeamA.GetComponent<TeamAI>().Init();
		TeamB.GetComponent<TeamAI>().Init();
	}

	void PlaceNets()
	{
		GameObject leftNet = (GameObject)Instantiate(netPrefab, new Vector3(-60/2 + (60/20) - .2f, -.25f, 0), Quaternion.Euler(0,0,180));
		leftGoal = leftNet.GetComponentInChildren<GoalZone>();
		leftNet.transform.parent = this.transform;
		leftGoal.Goal+= new GoalHandler(LeftGoalScoredOn);

		GameObject rightNet = (GameObject)Instantiate(netPrefab, new Vector3(60/2 - (60/20) + .2f, -.25f, 0), Quaternion.identity);
		rightGoal = rightNet.GetComponentInChildren<GoalZone>();
		rightNet.transform.parent = this.transform;
		rightGoal.Goal+= new GoalHandler(RighttGoalScoredOn);

	}

	void LeftGoalScoredOn()
	{

		view.goalText.gameObject.SetActive(true);
		ChangeState(gmFaceoffState);
		GameData.Instance.CurrentGame.TeamB_Score++;
		view.UpdateScores (GameData.Instance.CurrentGame.TeamA_Score, GameData.Instance.CurrentGame.TeamB_Score);
	}

	void RighttGoalScoredOn()
	{
		view.goalText.gameObject.SetActive(true);
		ChangeState(gmFaceoffState);
        GameData.Instance.CurrentGame.TeamA_Score++;
		view.UpdateScores (GameData.Instance.CurrentGame.TeamA_Score, GameData.Instance.CurrentGame.TeamB_Score);
	}

	// Use this for initialization
	public void Awake()
	{
		mLoading = true;
		timeLeft = GAME_LENGTH;       
        CreateTeams(GameData.Instance.GetTeam(GameData.Instance.CurrentGame.TeamA_ID), GameData.Instance.GetTeam(GameData.Instance.CurrentGame.TeamB_ID));
		PlaceNets();
		fsm = new FiniteStateMachine<GameManager>();
		fsm.Init();
		fsm.Configure(this, gmFaceoffState);
		gmFaceoffState.Enter(this);
		mLoading = false;
	}


	// Update is called once per frame
	void Update () 
	{
		if(!mLoading)
			fsm.UpdateStateMachine();
	}	
}
