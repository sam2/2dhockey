using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GMEndGameState : FSMState<GameManager> {
	
	
	public override void Enter(GameManager gm)
	{
		gm.StartCoroutine(EndGamePresentation(gm));
		gm.ChangeState(gm.gmFaceoffState);
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
		yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("FrontEnd");
	}
	
}
