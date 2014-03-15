using UnityEngine;
using System.Collections;

public class PlayerBase : MonoBehaviour {

	public Team team;

	public float speed;
	public float turnSpeed;
	public float shotPower;
	public Vector2 puckCtrl;

	public SteeringBehavior steering;

	public Vector2 destinationPosition;
	public Vector2 mHomePosition;

	int checkPower;
	int balance;

	// Use this for initialization
	public void Init () {
		steering = new SteeringBehavior(rigidbody2D, speed);
		checkPower = Random.Range (10, 20);
		balance = Random.Range(10, 20);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//*****************************************************************************
	
	float destinationTolerance = 0.1f;
	
	public bool isAtDestination()
	{
		if((destinationPosition - new Vector2(transform.position.x, transform.position.y)).magnitude < destinationTolerance)
		{
			return true;
		}
		return false;
	}
	
	public bool isAtHomePosition()
	{
		if((mHomePosition - new Vector2(transform.position.x, transform.position.y)).magnitude < destinationTolerance)
		{
			return true;
		}
		return false;
	}

	//DND RULES
	public bool RollForChecked()
	{
		int balanceRoll = (Puck.puck.controllingPlayer.balance - 10)/2 + Random.Range(0,20);
		int checkRoll = (checkPower - 10)/2 + Random.Range(0,20);
		if(checkRoll + 2 > balanceRoll)
			return true;
		return false;
	}
}
