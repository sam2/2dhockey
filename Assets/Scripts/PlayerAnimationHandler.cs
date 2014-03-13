using UnityEngine;
using System.Collections;

public class PlayerAnimationHandler : MonoBehaviour {


	Animator animator;
	Player p;
	public int flip;
	// Use this for initialization
	void Start () {
		p = GetComponent<Player>();
		animator = GetComponentInChildren<Animator>();
	}

	public enum Facing {upRight, right, downRight, upLeft, left, downLeft, fallen}
	public Facing face;
	
	// Update is called once per frame
	void Update () {
		UpdateFacing();
		if(p.fallen && face != Facing.fallen)
		{
			face = Facing.fallen;
			animator.SetInteger("Facing", (int)Facing.fallen);
		}
		else
			animator.SetInteger("Facing", (int)face%3);
	
	}

	void UpdateFacing()
	{
		if(p.facing.x > 0.0f)
		{
			face = Facing.right;
			animator.transform.localScale = new Vector3(flip*1, 1, 1);
			if(p.facing.y >= 0.5f)
			{
				face = Facing.upRight;
			}
			else if ( p.facing.y < -0.5f)
			{
				face = Facing.downRight;
			}
		}
		else if(p.facing.x <= 0.0f)
		{
			face = Facing.left;
			animator.transform.localScale = new Vector3(flip*-1, 1, 1);
			if(p.facing.y >= 0.5f)
			{
				face = Facing.upLeft;
			}
			else if ( p.facing.y < -0.5f)
			{
				
				face = Facing.downLeft;
			}
		}
	}
	
}
