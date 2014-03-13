using UnityEngine;
using System.Collections;

public class Goalie : MonoBehaviour {

	SteeringBehavior steering;
	public float maxSpeed;

	public Vector2 anchor;

	public float challengeDistance;

	public float attackDistance;
	public float leashdistance;
	// Use this for initialization
	void Start () {
		steering = new SteeringBehavior(rigidbody2D, maxSpeed);

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector2 puckVector = new Vector2(Puck.puck.transform.position.x, Puck.puck.transform.position.y) - anchor;
		
		Vector2 desiredPos = anchor +  (puckVector * challengeDistance);


		if(Vector2.Distance(Puck.puck.transform.position, anchor) <= attackDistance)
		{
			rigidbody2D.AddForce(steering.Pursuit(Puck.puck.rigidbody2D));
		}
		else
			rigidbody2D.AddForce(steering.Arrive(desiredPos, SteeringBehavior.Deceleration.fast));

	
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(anchor, attackDistance);
	}
}
