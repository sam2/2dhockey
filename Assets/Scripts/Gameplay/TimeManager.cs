using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

	public AnimationCurve curve;

	public static TimeManager Instance{get; set;}
	public bool paused = false;

	FiniteStateMachine<TimeManager> time = new FiniteStateMachine<TimeManager>();
	public TimeSlowState slowState = new TimeSlowState();
	public TimePauseState pauseState = new TimePauseState();
	public TimeNormalState normalState = new TimeNormalState();

	void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else
		{
			Debug.LogError("TimeManager already exists");
		}
		time.Init();
		time.Configure(this, normalState);
	}

	void Update()
	{
		time.UpdateStateMachine();
	}

	public void ChangeState(FSMState<TimeManager> s)
	{
		time.ChangeState(s);
	}

	public IEnumerator ChangeTimeScale(float from, float to, float duration)
	{
		float lastTime = Time.realtimeSinceStartup;
		float elapsedTime = 0;
		while(elapsedTime <= duration)
		{
			Time.timeScale = curve.Evaluate(elapsedTime);
			Time.fixedDeltaTime = 0.02f*Time.timeScale;
			elapsedTime+= (Time.realtimeSinceStartup - lastTime);
			yield return null;
		}
		Time.timeScale = to;
		Time.fixedDeltaTime = 0.02f*Time.timeScale;
	}
}
