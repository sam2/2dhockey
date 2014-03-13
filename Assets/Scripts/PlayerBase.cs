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

	// Use this for initialization
	void Start () {

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
}
