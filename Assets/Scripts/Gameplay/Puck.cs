using UnityEngine;
using System.Collections;

public delegate void PuckControlChangedHandler(Player p);

public class Puck : MonoBehaviour {

	public event PuckControlChangedHandler PuckControlChanged;


	public static Puck puck;
	public static bool beingShot;
	public Player controllingPlayer;
	public Player lastControllingPlayer;

	public GameObject highlightCircle;
	// Use this for initialization
	void Awake(){
		controllingPlayer = null;
		puck = this;	
	}
	void Start () {
		
		beingShot = false;
	}

	public void InPlay(bool inplay)
	{
		gameObject.SetActive(inplay);
	}

	// Update is called once per frame
	void Update () {
		if(controllingPlayer){

			int flip = (int)Mathf.Sign(controllingPlayer.facing.x) * -(int)controllingPlayer.team.side;
			transform.position = controllingPlayer.transform.position + new Vector3(flip*controllingPlayer.puckCtrl.x, controllingPlayer.puckCtrl.y, 0);

		}
		else
		{
			collider2D.enabled = true;
		}
		
	}
	
	public void Shoot(Vector2 forceVector)
	{
		if(controllingPlayer != null)
		{
			Debug.Log("Adding force to puck: "+forceVector);
			rigidbody2D.AddForce(forceVector);
			controllingPlayer.DisableBox(.25f);
			lastControllingPlayer = controllingPlayer;
			controllingPlayer = null;
		}

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.collider.tag == "Player"){

			Player p = other.collider.GetComponent<Player>();
			if(controllingPlayer == null && !p.fallen)
			{
				rigidbody2D.velocity = Vector2.zero;
				collider2D.enabled = false;
				controllingPlayer = p;
				if(PuckControlChanged != null)
				{
					PuckControlChanged(controllingPlayer);
				}
			}
		}
	}

	public void Reset()
	{
		controllingPlayer = null;
		rigidbody2D.velocity = Vector2.zero;
		transform.position =Vector3.zero;
		InPlay(false);
	}

	public Vector2 FuturePosition(float time)
	{
		Vector2 ut = rigidbody2D.velocity*time;
		float halfATSquared = 0.5f*rigidbody2D.drag*time*time;

		Vector2 scalar2Vector = halfATSquared*rigidbody2D.velocity.normalized;
		Vector2 pos2d = transform.position;
		return (pos2d + ut + scalar2Vector);
	}
	/*
	public float TimeToCoverDistance(Vector2 a, Vector2 b, float force)
	{
		float speed = force/rigidbody2D.mass;
		float distance = Vector2.Distance(a,b);
		float term = speed*speed + 2.0f*distance*rigidbody2D.collider.material.fricti
	}
	*/
}
