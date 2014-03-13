using UnityEngine;
using System.Collections;

public class GoalZone : MonoBehaviour {



	public GUIText scoreText;
	public ParticleSystem goalLight;
	BoxCollider2D colder;
	public int score;
	// Use this for initialization
	void Start () {
		colder = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = ""+score;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Puck")
		{
			score++;
			goalLight.Play();
			StartCoroutine(ResetPuck(1));
		}
	}

	IEnumerator ResetPuck(float delay)
	{
		colder.enabled = false;
		yield return new WaitForSeconds(delay);
		colder.enabled = true;
		Puck.puck.Reset();
	}


}
