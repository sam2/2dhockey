using UnityEngine;
using System.Collections;

public delegate void GameEndedHandler(LGame game);

public class GameManager : MonoBehaviour {

	//temp
	public GUIText goalText;


	public GameObject netPrefab;
	public event GameEndedHandler GameEnded;

	public static GoalZone leftGoal;
	public static GoalZone rightGoal;

	LGame mScore;

	public Team teamA; //left team
	public Team teamB; //right team
	public GameObject teamAPrefab;
	public GameObject teamBPrefab;

	public float mGameLength;
	public float timeLeft;
	public GameView view;

	FiniteStateMachine<GameManager> fsm;
	public GMFaceoffState gmFaceoffState = new GMFaceoffState();
	public GMPlayState gmPlayState = new GMPlayState();

	public void ChangeState(FSMState<GameManager> state)
	{
		fsm.ChangeState(state);
	}


	void CreateTeams(LTeam lteamA, LTeam lteamB)
	{
		GameObject gA = (GameObject)Instantiate(teamAPrefab);
		teamA = gA.GetComponent<Team>();
		gA.transform.parent = this.transform;

		GameObject gB = (GameObject)Instantiate(teamBPrefab);
		teamB = gB.GetComponent<Team>();
		gB.transform.parent = this.transform;

		teamA.opponent = teamB;
		teamB.opponent = teamA;
		teamA.SpawnPlayers(lteamA);
		teamA.Init();
		teamB.SpawnPlayers(lteamB);
		teamB.Init();
	}

	void PlaceNets()
	{
		GameObject leftNet = (GameObject)Instantiate(netPrefab, new Vector3(-50/2 + (50/20) + .5f, 0, 0), Quaternion.Euler(0,0,180));
		leftGoal = leftNet.GetComponentInChildren<GoalZone>();
		leftNet.transform.parent = this.transform;
		GameObject rightNet = (GameObject)Instantiate(netPrefab, new Vector3(50/2 - (50/20) - .5f, 0, 0), Quaternion.identity);
		rightGoal = rightNet.GetComponentInChildren<GoalZone>();
		rightNet.transform.parent = this.transform;
		leftGoal.Goal+= new GoalHandler(LeftGoalScoredOn);
		rightGoal.Goal+= new GoalHandler(RighttGoalScoredOn);

	}

	void LeftGoalScoredOn()
	{
		goalText.gameObject.SetActive(true);
		ChangeState(gmFaceoffState);
		mScore.mScoreB++;
		view.UpdateScores (mScore.mScoreA, mScore.mScoreB);
	}

	void RighttGoalScoredOn()
	{
		goalText.gameObject.SetActive(true);
		ChangeState(gmFaceoffState);
		mScore.mScoreA++;
		view.UpdateScores (mScore.mScoreA, mScore.mScoreB);
	}

	// Use this for initialization
	public void LoadGame(LGame game, LTeam teamA, LTeam teamB)
	{
		timeLeft = mGameLength;
		mScore = new LGame();
		mScore.mTeamA = game.mTeamA;
		mScore.mTeamB = game.mTeamB;
		mScore.mScoreA = 0;
		mScore.mScoreB = 0;
		CreateTeams(teamA, teamB);
		PlaceNets();
		fsm = new FiniteStateMachine<GameManager>();
		fsm.Init();
		fsm.Configure(this, gmFaceoffState);
		gmFaceoffState.Enter(this);
	}


	// Update is called once per frame
	void Update () 
	{
		fsm.UpdateStateMachine();

	}

	public void EndGame()
	{

		GameEnded(mScore);
	}

	
}
