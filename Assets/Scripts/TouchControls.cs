using UnityEngine;
using System.Collections;

public class TouchControls : MonoBehaviour {

	Team mTeam;

	bool mShoot;
	Player mTarget;

	public float TOUCH_RANGE;
	public float SNAP_RANGE;

	bool snapped;
	// Use this for initialization
	void Start () {
		mTeam = GetComponent<Team>();
	}

	int mShotFingerID = -1;
	
	// Update is called once per frame
	void Update () 
	{
#if UNITY_EDITOR
		Vector2 mousePos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if(Input.GetMouseButtonDown(0))
		{
			mTarget = FindClosestPlayerToPoint(mousePos, TOUCH_RANGE);
			Debug.Log(mTarget);
		}
		mShoot = Input.GetKey(KeyCode.Space) && (mTarget == Puck.puck.controllingPlayer); //Touch.touchCount > 1;
		if(mTarget != null)
		{
			if(mShoot)
			{
				mTarget.mView.ChangeLineColor(Color.red);
				mTarget.mView.DrawToMouse(mTarget.transform.position);
				if(Input.GetMouseButtonUp(0))
				{
					Vector2 shotVector = mousePos - (Vector2)mTarget.transform.position;
					Puck.puck.Shoot(shotVector.normalized*mTarget.shotPower);
					mTarget.mView.ClearPlayerView();
				}
			}
			else
			{
				if(Input.GetMouseButtonUp(0))
				{
					ClearTarget();
				}
				if(Input.GetMouseButton(0))
				{
					mTarget.mView.ChangeLineColor(Color.blue);
					mTarget.mView.DrawToMouse(mTarget.transform.position);
					mTarget.ChangeState(mTarget.controlledState);
					mTarget.destinationPosition = mousePos;
					Debug.Log("drawing to "+mTarget);
				}
			}
		}
#else
	/*	if(Input.touchCount > 1)
		{
			if(Input.GetTouch(1).phase == TouchPhase.Began)
			{
				mShotFingerID = Input.GetTouch(1).fingerId;
				mShoot = true;
			}
			if(Input.GetTouch(1).phase == TouchPhase.Ended)
			{
				mShotFingerID = -1;
				mShoot = false;
			}
		}
		if(Input.touchCount > 0)
		{
			Touch t0 = Input.GetTouch(0);
			Vector2 touchPos = Camera.main.ScreenToWorldPoint(t0.position);
			if(t0.phase == TouchPhase.Began && t0.fingerId != mShotFingerID)
			{
				mTarget = FindClosestPlayerToPoint(touchPos, TOUCH_RANGE);
			}
			if(mTarget != null)
			{
				if(mShoot)
				{
					mTarget.mView.ChangeLineColor(Color.red);
					mTarget.mView.DrawToMouse(mTarget.transform.position);
					if(t0.phase == TouchPhase.Ended)
					{
						Vector2 shotVector = touchPos - (Vector2)mTarget.transform.position;
						Puck.puck.Shoot(shotVector.normalized*mTarget.shotPower);
						mTarget.mView.ClearPlayerView();
					}
				}
				else
				{
					if(t0.phase == TouchPhase.Ended)
					{
						ClearTarget();
					}
					if(t0.phase == TouchPhase.Moved)
					{
						mTarget.mView.ChangeLineColor(Color.blue);
						mTarget.mView.DrawLine(mTarget.transform.position, touchPos); 
						mTarget.ChangeState(mTarget.controlledState);
						mTarget.destinationPosition = touchPos;
						Debug.Log("drawing to "+mTarget);
					}
				}
			}
		} */
		if(Input.touchCount > 0)
		{
			//shoot
			if(Input.touchCount > 1)
			{
				switch(Input.GetTouch(1).phase)
				{

				case TouchPhase.Began:
					mShoot = true;
					if(mTarget != null)
					{
						mTarget.mView.ChangeLineColor(Color.red);
					}
					break;

				case TouchPhase.Ended:
					if(mShoot)
					{
						mShoot = false;
						Vector2 shotVector = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - mTarget.transform.position;
						Puck.puck.Shoot(shotVector.normalized*mTarget.shotPower);
						ClearTarget();
					}
					break;

				case TouchPhase.Moved:
				case TouchPhase.Stationary:
					if(mTarget != null)
					{
						mTarget.mView.DrawToMouse(mTarget.transform.position);
					}
					break;
				}
			
			}

			if(Input.touchCount == 1)
			{
				Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				
				switch(Input.GetTouch(0).phase)
				{
					
				case TouchPhase.Began:
					mTarget = FindClosestPlayerToPoint(touchPos, TOUCH_RANGE);
					if(mTarget != null)
					{
						mTarget.mView.ChangeLineColor(Color.blue);
						mTarget.ChangeState(mTarget.controlledState);
					}
					break;
					
				case TouchPhase.Ended:
					ClearTarget();
					break;
					
				case TouchPhase.Moved:
				case TouchPhase.Stationary:
					if(mTarget != null)
					{
						mTarget.destinationPosition = touchPos;
						mTarget.mView.DrawToMouse(mTarget.transform.position);
					}
					break;
				}

			}
		}
#endif

	}

	void ClearTarget()
	{
		if(mTarget!=null)
		{
			mTarget.mView.ClearPlayerView();
			mTarget = null;
		}
	}



	public Player FindClosestPlayerToPoint(Vector2 point, float minRange)
	{
		Player closest = null;

		foreach(Player p in mTeam.mPlayers)
		{
			float dist = Vector2.Distance(point, p.transform.position);
			Debug.Log("Dist: "+dist);
			if(dist < minRange)
			{
				minRange = dist;
				closest = p;
			}
		}

		return closest;
	}

	public void Toggle(bool on)
	{
		enabled = on;
	}
}
