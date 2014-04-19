using UnityEngine;

public class SupportSpot
{
	public Vector2 mPos;
	public float mScore;
	public SupportSpot(float x, float y, float score)
	{
		mPos = new Vector2(x,y);
		mScore = score;
	}
}