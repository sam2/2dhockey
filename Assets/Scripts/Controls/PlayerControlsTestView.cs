using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class PlayerControlsTestView : MonoBehaviour, IPlayerControlsView {

	LineRenderer mLineRenderer;
	public float mLineLength = 1;

	public GameObject mSelectCircle;

	Player mSelectedPlayer;
	 
	void Start()
	{
		mLineRenderer = GetComponent<LineRenderer>();
		mLineRenderer.SetVertexCount(2);
	}

	public void ShowChargeUpShot(float percent, Vector2 origin, Vector2 direction)
	{
		if(percent == 0)
		{
			mLineRenderer.enabled = false;
			return;
		}
		if(mLineRenderer.enabled == false)
			mLineRenderer.enabled = true;
		mLineRenderer.SetPosition(0, origin);

		Vector2 pos = origin + (direction.normalized * percent * mLineLength);

		mLineRenderer.SetPosition(1, pos);
	}

	void Update()
	{
		if(mSelectedPlayer != null)
		{
			mSelectCircle.transform.position = mSelectedPlayer.transform.position;
		}
	}

	public void ChangeSelected(Player player)
	{
		mSelectedPlayer = player;
		if(player != null)
		{			
			return;
		}
		mSelectCircle.SetActive(false);
	}
}
