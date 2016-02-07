using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GMEndGameState : FSMState<GameManager> {


    public override void Enter(GameManager gm)
    {
        gm.StartCoroutine(EndGamePresentation(gm));
        gm.TeamA.ChangeState(gm.TeamA.faceoffState);
        gm.TeamB.ChangeState(gm.TeamB.faceoffState);
    }

    public override void Execute(GameManager gm)
	{

	}
	
	public override void Exit(GameManager gm)
	{

	}

	public IEnumerator EndGamePresentation(GameManager manager)
	{
		manager.view.SetGoalText("GAME OVER");
		manager.view.goalText.gameObject.SetActive(true);
        GameData.Instance.LeagueData.CurrentSeason.GamePlayed(GameData.Instance.CurrentGame);
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("FrontEnd");
	}
	
}
