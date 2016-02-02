﻿using UnityEngine;
using System.Collections;

public delegate void GameEndedHandler(LGame game);

public class GameManager : Singleton<GameManager>
{
	public GoalZone LeftGoal;
	public GoalZone RightGoal;

	public Team TeamA; //left team
	public Team TeamB; //right team

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
	void Start()
	{
		mLoading = true;
		timeLeft = GAME_LENGTH;       
       // CreateTeams(GameData.Instance.GetTeam(GameData.Instance.CurrentGame.TeamA_ID), GameData.Instance.GetTeam(GameData.Instance.CurrentGame.TeamB_ID));
		//PlaceNets();
		fsm = new FiniteStateMachine<GameManager>();
		fsm.Init();
		fsm.Configure(this, gmFaceoffState);
		gmFaceoffState.Enter(this);
        LeftGoal.Goal += new GoalHandler(LeftGoalScoredOn);
        RightGoal.Goal += new GoalHandler(RighttGoalScoredOn);
        mLoading = false;
	}


	// Update is called once per frame
	void Update () 
	{
		if(!mLoading)
			fsm.UpdateStateMachine();
	}	
}
