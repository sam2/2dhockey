using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArenaGenerator : MonoBehaviour {

	public GameObject scoredisplayPrefab;
	public GameObject boardPrefab;


	public float mWidth;
	public float mHeight;

	public float sampleDensity;


	public void PlaceBoards(float width, float height)
	{
		GameObject leftSide = (GameObject)Instantiate(boardPrefab, new Vector3(-width/2, 0, 0), Quaternion.identity);
		leftSide.transform.localScale = new Vector3(1,height,1);
		leftSide.transform.parent = this.transform;

		GameObject rightSide = (GameObject)Instantiate(boardPrefab, new Vector3(width/2, 0, 0), Quaternion.identity);
		rightSide.transform.localScale = new Vector3(1,height,1);
		rightSide.transform.parent = this.transform;

		GameObject top = (GameObject)Instantiate(boardPrefab, new Vector3(0, height/2, 0), Quaternion.identity);
		top.transform.localScale = new Vector3(width,1,1);
		top.transform.parent = this.transform;

		GameObject bot = (GameObject)Instantiate(boardPrefab, new Vector3(0, -height/2, 0), Quaternion.identity);
		bot.transform.localScale = new Vector3(width,1,1);
		bot.transform.parent = this.transform;
	}

	void SetCameraSize()
	{
		Camera.main.orthographicSize = mHeight/2;
	}



	void Awake () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
