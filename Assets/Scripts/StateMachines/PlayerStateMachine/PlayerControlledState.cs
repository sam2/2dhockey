using UnityEngine;
using System.Collections;

public class PlayerControlledState : FSMState<Skater> {
	
	float m_EnterTime = Mathf.Infinity;
	public override void Enter(Skater p)
	{
		m_EnterTime = Time.time;
		p.Controlled = true;
		Debug.Log("entering controlled state");
	}
	
	public override void Execute(Skater p)
	{
		if(Time.time > m_EnterTime + 3)
			p.ChangeState(p.playState);	
	}

	//exit called by playerControls
	public override void Exit(Skater p)
	{
		m_EnterTime = Mathf.Infinity;
		p.Controlled = false;
		Debug.Log("exiting controlled state");
	}
	
}