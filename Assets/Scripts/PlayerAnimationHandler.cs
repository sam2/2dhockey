﻿using UnityEngine;
using System.Collections;

public class PlayerAnimationHandler : MonoBehaviour {


	Animator animator;
	Skater p;
	// Use this for initialization
	void Start () {
		p = GetComponent<Skater>();
		animator = GetComponentInChildren<Animator>();
	}

	public enum Facing {upRight, right, downRight, upLeft, left, downLeft, fallen}
	public Facing face;
	
	// Update is called once per frame
	void Update () {
		UpdateFacing();
		if(p.Fallen && face != Facing.fallen)
		{
			face = Facing.fallen;
			animator.SetInteger("Facing", (int)Facing.fallen);
		}
		else
			animator.SetInteger("Facing", (int)face%3);

		animator.SetBool("slapshot", p.Slapshot);
	
	}

	void UpdateFacing()
	{
		if(p.Facing.x > 0.0f)
		{
			face = Facing.right;
			animator.transform.localScale = new Vector3((int)p.team.side*-1f, 1, 1);
			if(p.Facing.y >= 0.5f)
			{
				face = Facing.upRight;
			}
			else if ( p.Facing.y < -0.5f)
			{
				face = Facing.downRight;
			}
		}
		else if(p.Facing.x <= 0.0f)
		{
			face = Facing.left;
			animator.transform.localScale = new Vector3((int)p.team.side*1, 1, 1);
			if(p.Facing.y >= 0.5f)
			{
				face = Facing.upLeft;
			}
			else if ( p.Facing.y < -0.5f)
			{
				
				face = Facing.downLeft;
			}
		}
	}
	
}
