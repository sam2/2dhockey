using UnityEngine;
using System.Collections;

public class Goalie : PlayerBase {


	public float saveChance;
	public Vector2 anchor;

	public float challengeDistance;

	public float attackDistance;
	public float leashdistance;

	public int side = 1;
	// Use this for initialization
	void Start () {
		base.Init();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector2 puckVector = new Vector2(Puck.puck.transform.position.x, Puck.puck.transform.position.y) - GetRearInterposeTarget();
		
		Vector2 desiredPos = GetRearInterposeTarget();


		if(Vector2.Distance(Puck.puck.transform.position, anchor) <= attackDistance && Puck.puck.controllingPlayer == null)
		{
			rigidbody2D.AddForce(steering.Pursuit(Puck.puck.rigidbody2D));
		}
		else
			rigidbody2D.AddForce(steering.Arrive(desiredPos, SteeringBehavior.Deceleration.fast));


	
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(GetRearInterposeTarget(), attackDistance);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.tag == "Player")
		{
			Player p = collision.collider.GetComponent<Player>();

			if(p && p.team!= team)
			{
				Vector2 dir = p.transform.position - transform.position;
				p.GetChecked(dir);
			}
		}
	}

	Vector2 GetRearInterposeTarget()
	{
		float arenaWidth = 60;
		float arenaHeight = 30;
		float netheight = 3;
		float netWidth = 3;
		float goalLine = 26*side;
		float y = Puck.puck.transform.position.y * (netWidth/2) / (arenaHeight/2);
		float x = goalLine + (side*-1);
		Vector2 target = new Vector2(x, y);
		return target;

	}
}