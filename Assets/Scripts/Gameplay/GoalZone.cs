using UnityEngine;
using System.Collections;

public delegate void GoalHandler();

public class GoalZone : MonoBehaviour {

	public event GoalHandler Goal;
	
	public ParticleSystem goalLight;
	BoxCollider2D colder;
	public int score;
	// Use this for initialization
	void Start () {
		colder = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Puck")
		{
			score++;
			Goal();
			goalLight.Play();
			StartCoroutine(DisableCollision(1));
		}
	}

	IEnumerator DisableCollision(float delay)
	{
		colder.enabled = false;
		yield return new WaitForSeconds(delay);
		colder.enabled = true;
		
	}


}
