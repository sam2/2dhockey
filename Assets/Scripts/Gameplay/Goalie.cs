using UnityEngine;
using System.Collections;

public class Goalie : Player {


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

		Vector2 puckVector = new Vector2(Puck.Instance.transform.position.x, Puck.Instance.transform.position.y) - GetRearInterposeTarget();
		Vector2 desiredPos = new Vector2();
		if(puckVector.magnitude <= attackDistance)
		{
			desiredPos = new Vector2((26*side) + (side*-1), Mathf.Clamp(Puck.Instance.transform.position.y, -1.5f, 1.5f));
		}
		else
		{
			float puckYSpeed = Puck.Instance.GetComponent<Rigidbody2D>().velocity.y;
			float y = Mathf.Clamp(Puck.Instance.transform.position.y + (puckYSpeed * 0.25f), -1.5f, 1.5f)*0.9f;
			desiredPos = new Vector2((26*side) + (side*-1), y);
		}




		GetComponent<Rigidbody2D>().AddForce(Steering.Arrive(desiredPos, SteeringBehavior.Deceleration.fast));



	
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(GetRearInterposeTarget(), attackDistance);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.tag == "Player")
		{
			Skater p = collision.collider.GetComponent<Skater>();

			if(p && p.team!= team)
			{
				Vector2 dir = p.transform.position - transform.position;
				p.GetChecked(dir);
			}
		}
	}

	Vector2 GetRearInterposeTarget()
	{
		float arenaHeight = 30;
		float netWidth = 3;
		float goalLine = 26*side;
		float y = Puck.Instance.transform.position.y * (netWidth/2) / (arenaHeight/2);
		float x = goalLine + (side*-1);
		Vector2 target = new Vector2(x, y);
		return target;

	}
}