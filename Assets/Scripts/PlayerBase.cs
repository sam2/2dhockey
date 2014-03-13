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
	public Vector3 facing;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
