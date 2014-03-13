using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArenaGenerator : MonoBehaviour {

	public GameObject scoredisplayPrefab;
	public GameObject boardPrefab;
	public GameObject netPrefab;

	public float mWidth;
	public float mHeight;

	public float sampleDensity;

	public static GoalZone leftGoal;
	public static GoalZone rightGoal;
	public static List<SupportSpot> samples;



	public void SamplePositions()
	{
		if(sampleDensity<=0)
		{
			Debug.LogError("ArenaGenerator: Invalid sampling density");
			return;
		}
		samples = new List<SupportSpot>();
		for(float x = (-mWidth/2 + 1); x < mWidth/2; x+=sampleDensity)
		{
			for(float y = (-mHeight/2 + 1); y < mHeight/2; y+=sampleDensity)
			{
				samples.Add(new SupportSpot(x,y,0));
			}
		}
	}

	void DrawSamples()
	{
		foreach(SupportSpot sample in samples)
		{
			Gizmos.DrawCube(sample.mPos, new Vector3(.1f,.1f,.1f));
		}
	}

	void OnDrawGizmosSelected()
	{
		DrawSamples();
	}

	public void PlaceBoards(float width, float height)
	{
		GameObject leftSide = (GameObject)Instantiate(boardPrefab, new Vector3(-width/2, 0, 0), Quaternion.identity);
		leftSide.transform.localScale = new Vector3(1,height,1);

		GameObject rightSide = (GameObject)Instantiate(boardPrefab, new Vector3(width/2, 0, 0), Quaternion.identity);
		rightSide.transform.localScale = new Vector3(1,height,1);

		GameObject top = (GameObject)Instantiate(boardPrefab, new Vector3(0, height/2, 0), Quaternion.identity);
		top.transform.localScale = new Vector3(width,1,1);

		GameObject bot = (GameObject)Instantiate(boardPrefab, new Vector3(0, -height/2, 0), Quaternion.identity);
		bot.transform.localScale = new Vector3(width,1,1);
	}

	void SetCameraSize()
	{
		Camera.main.orthographicSize = mHeight/2;
	}

	void PlaceNets()
	{
		GameObject leftNet = (GameObject)Instantiate(netPrefab, new Vector3(-mWidth/2 + (mWidth/20), 0, 0), Quaternion.Euler(0,0,180));
		leftGoal = leftNet.GetComponentInChildren<GoalZone>();
		GameObject rightNet = (GameObject)Instantiate(netPrefab, new Vector3(mWidth/2 - (mWidth/20), 0, 0), Quaternion.identity);
		rightGoal = rightNet.GetComponentInChildren<GoalZone>();
	}
	// Use this for initialization
	void Awake () {
		PlaceBoards(mWidth,mHeight);
		SetCameraSize();
		PlaceNets();
		SamplePositions();
		GameObject g = (GameObject)Instantiate(scoredisplayPrefab);
		GUIText scoreText = g.GetComponent<GUIText>();
		g.transform.position = new Vector3(0.6f, 0.9f, 0f);
		leftGoal.scoreText = scoreText;

		g = (GameObject)Instantiate(scoredisplayPrefab);
		scoreText = g.GetComponent<GUIText>();
		g.transform.position = new Vector3(0.4f, 0.9f, 0f);
		rightGoal.scoreText = scoreText;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
