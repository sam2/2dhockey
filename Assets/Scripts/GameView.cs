using UnityEngine;
using System.Collections;

public class GameView : MonoBehaviour {

	public GUIText scoreA;
	public GUIText scoreB;
	public GUIText timer;
	public GUIText goalText;

	public void UpdateTimer(float timeLeft)
	{
		int minutes = (int)timeLeft / 60;
		int seconds = (int)timeLeft - (minutes * 60);
		timer.text = minutes + ":";
		if(seconds < 10)
			timer.text+="0";
		timer.text+=""+seconds;
	}

	public void UpdateScores(int a, int b)
	{
		scoreA.text = ""+a;
		scoreB.text = ""+b;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetGoalText(string text)
	{
		goalText.text = text;
	}
}
