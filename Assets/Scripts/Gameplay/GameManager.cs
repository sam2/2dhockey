using UnityEngine;
using System.Collections;

public delegate void GameEndedHandler(LGame game);

public class GameManager : Singleton<GameManager>
{
	public GoalZone LeftGoal;
	public GoalZone RightGoal;

	public TeamAI TeamA; //left team
	public TeamAI TeamB; //right team

	public float GAME_LENGTH;
	private float m_TimeLeft;
	public GameView View;

	FiniteStateMachine<GameManager> m_FSM;
	public GMFaceoffState gmFaceoffState = new GMFaceoffState();
	public GMPlayState gmPlayState = new GMPlayState();
	public GMEndGameState gmEndGameState = new GMEndGameState();

    public bool FaceoffInProgress;

	public void ChangeState(FSMState<GameManager> state)
	{
		m_FSM.ChangeState(state);
	}

	void LeftGoalScoredOn()
	{
		View.goalText.gameObject.SetActive(true);
		ChangeState(gmFaceoffState);
		GameData.Instance.CurrentGame.TeamB_Score++;
		View.UpdateScores (GameData.Instance.CurrentGame.TeamA_Score, GameData.Instance.CurrentGame.TeamB_Score);
        StartCoroutine(DelayedFaceoff(1));
    }

	void RighttGoalScoredOn()
	{
		View.goalText.gameObject.SetActive(true);
		ChangeState(gmFaceoffState);
        GameData.Instance.CurrentGame.TeamA_Score++;
		View.UpdateScores (GameData.Instance.CurrentGame.TeamA_Score, GameData.Instance.CurrentGame.TeamB_Score);
        StartCoroutine(DelayedFaceoff(1));
    }

	void Awake()
	{
		m_TimeLeft = GAME_LENGTH;
		m_FSM = new FiniteStateMachine<GameManager>();		
        LeftGoal.Goal += new GoalHandler(LeftGoalScoredOn);
        RightGoal.Goal += new GoalHandler(RighttGoalScoredOn);
	}

    void Start()
    {
        TeamA.Init();
        TeamB.Init();
        m_FSM.Init();
        m_FSM.Configure(this, gmFaceoffState);
    }


	void Update () 
	{
        m_FSM.UpdateStateMachine();
        TeamA.UpdateAI();
        TeamB.UpdateAI();
	}

    public void UpdateGameTime()
    {
        m_TimeLeft -= Time.deltaTime;
        if (m_TimeLeft <= 0)
        {
            ChangeState(gmEndGameState);
        }
        View.UpdateTimer(m_TimeLeft);
    }

    IEnumerator DelayedFaceoff(float time)
    {
        yield return new WaitForSeconds(time);
        Puck.Instance.Reset();
    }
}
