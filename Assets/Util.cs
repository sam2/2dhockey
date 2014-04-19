using UnityEngine;
using System.Collections;

public class Util 
{

	public static IEnumerator ChangeTime(float from, float to, float duration)
	{
		float lastTime = Time.realtimeSinceStartup;
		float elapsedTime = 0;
		AnimationCurve curve = AnimationCurve.EaseInOut(0, from, duration, to);
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
