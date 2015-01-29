using UnityEngine;
using System.Collections;

public class SaveChecker : MonoBehaviour {

	public float disableTime;
	Goalie goalie;
	// Use this for initialization
	void Start () {
		goalie = transform.parent.GetComponent<Goalie>();
		if(goalie == null)
		{
			Debug.LogError(gameObject.name+": Unable to find goalie component");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject == Puck.Instance.gameObject)
		{
			if(Random.Range(0f,1f) > goalie.saveChance)
				StartCoroutine(MakePassable());
		}
	}

	IEnumerator MakePassable()
	{
		goalie.collider2D.isTrigger = true;
		yield return new WaitForSeconds(disableTime);
		goalie.collider2D.isTrigger = false;
	}
}
