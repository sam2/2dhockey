using UnityEngine;
using System.Collections;

public class TouchControls : MonoBehaviour {

	Team mTeam;

	bool mShoot;
	Skater mTarget;

	public float TOUCH_RANGE;

	public float SNAP_RANGE;
	bool mSnapped;
	Skater mSnappedTo;
	// Use this for initialization
	void Start () {
		mTeam = GetComponent<Team>();
	}

	
	// Update is called once per frame
	void Update () 
	{
#if UNITY_EDITOR
		UpdateMouse();
#else
		UpdateTouch();



#endif

	}

	void ClearTarget()
	{
		if(mTarget!=null)
		{
			mTarget.View.ClearPlayerView();
			mShoot = false;
			mTarget = null;
		}
	}

	void UpdateMouse()
	{
		Vector2 mousePos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		if(Input.GetMouseButtonDown(0))
		{
			mTarget = FindClosestPlayerToPoint(mousePos, TOUCH_RANGE);
			if(mTarget != null && !mTarget.controlled)
			{
				mTarget.ChangeState(mTarget.controlledState);
			}
			Debug.Log(mTarget);
		}
		if(Input.GetKeyDown(KeyCode.Space) && (mTarget == Puck.Instance.controllingPlayer))
		{
			mShoot = true;
		}
		if(mTarget != null)
		{
			if(mShoot)
			{
				Time.timeScale = 0.25f;
				Time.fixedDeltaTime = 0.02f * Time.timeScale;
				mSnappedTo = FindClosestPlayerToPoint(mousePos, SNAP_RANGE);
				mSnapped = (mSnappedTo != null);
				
				if(mSnapped)
				{
					mTarget.View.ChangeLineColor(Color.green);
					mTarget.View.DrawLine(mTarget.transform.position, mSnappedTo.transform.position);
				}
				else
				{
					mTarget.View.ChangeLineColor(Color.red);
					mTarget.View.DrawToMouse(mTarget.transform.position);
				}
				if(Input.GetKeyUp(KeyCode.Space))
				{
					mShoot = false;
					Time.timeScale = 1f;
					Time.fixedDeltaTime = 0.02f * Time.timeScale;
					if(!mSnapped)
					{
						Vector2 shotVector = mousePos - (Vector2)mTarget.transform.position;
						Puck.Instance.Shoot(shotVector.normalized*mTarget.shotPower);
					}
					else
					{
						Puck.Instance.Shoot(mTarget.GetPassVector(mSnappedTo)*mTarget.shotPower);
					}
					mTarget.View.ClearPlayerView();
				}
			}
			else
			{

				if(Input.GetMouseButtonUp(0))
				{
					ClearTarget();
				}
				else if(Input.GetMouseButton(0))
				{
					mTarget.View.ChangeLineColor(Color.blue);
					mTarget.View.DrawToMouse(mTarget.transform.position);
					mTarget.MoveTo(mousePos);

				}
			}
		}
	}

	void UpdateTouch()
	{
		if(Input.touchCount > 0)
		{
			//shoot
			if(Input.touchCount > 1)
			{
				switch(Input.GetTouch(1).phase)
				{
					
				case TouchPhase.Began:
					if(mTarget != null)
					{
						mShoot = true;
						mTarget.View.ChangeLineColor(Color.red);
						Time.timeScale = 0.25f;
						Time.fixedDeltaTime = 0.02f * Time.timeScale;
					}
					break;
					
				case TouchPhase.Ended:
					if(mShoot)
					{
						mShoot = false;
						Time.timeScale = 1f;
						Time.fixedDeltaTime = 0.02f * Time.timeScale;
						if(!mSnapped)
						{
							Vector2 shotVector = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - mTarget.transform.position;
							Puck.Instance.Shoot(shotVector.normalized*mTarget.shotPower);
						}
						else
						{
							Puck.Instance.Shoot(mTarget.GetPassVector(mSnappedTo)*mTarget.shotPower);
						}
						ClearTarget();
					}
					break;
					
				case TouchPhase.Moved:
				case TouchPhase.Stationary:
					if(mTarget != null)
					{
						
						Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
						
						//snap passing
						mSnappedTo = FindClosestPlayerToPoint(touchPos, SNAP_RANGE);
						mSnapped = (mSnappedTo != null);
						
						if(mSnapped)
						{
							mTarget.View.ChangeLineColor(Color.green);
							mTarget.View.DrawLine(mTarget.transform.position, mSnappedTo.transform.position);
						}
						else
						{
							mTarget.View.ChangeLineColor(Color.red);
							mTarget.View.DrawToMouse(mTarget.transform.position);
						}
						
						
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
						mTarget.View.ChangeLineColor(Color.blue);
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
						mTarget.MoveTo(touchPos);
						mTarget.View.DrawToMouse(mTarget.transform.position);
					}
					break;
				}
				
			}
		}
	}


	public Skater FindClosestPlayerToPoint(Vector2 point, float minRange)
	{
		Skater closest = null;

		foreach(Skater p in mTeam.mPlayers)
		{
			float dist = Vector2.Distance(point, p.transform.position);

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
