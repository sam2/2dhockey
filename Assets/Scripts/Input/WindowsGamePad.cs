using UnityEngine;
using System.Collections;

public class WindowsGamePad : IGamePad {

	private int mIndex = -1;
	private float mStickDeadZone;
	private bool mIsEnabled = true;

	public WindowsGamePad(int index, float stickDeadZone)
	{
		mIndex = index;
		mStickDeadZone = stickDeadZone;
	}

	public Vector2 GetLeftStick()
	{
		Vector2 axis = new Vector2(Input.GetAxis("L_XAxis_"+mIndex), -Input.GetAxis("L_YAxis_"+mIndex));
		if(axis.magnitude >= mStickDeadZone && IsEnabled())
		{
			return axis;
		}
		return Vector2.zero;
	}

	public Vector2 GetRightStick()
	{
		Vector2 axis =  new Vector2(Input.GetAxis("R_XAxis_"+mIndex), -Input.GetAxis("R_YAxis_"+mIndex));
		if(axis.magnitude >= mStickDeadZone && IsEnabled())
		{
			return axis;
		}
		return Vector2.zero;
	}
	
	public Vector2 GetDPad()
	{
		//TODO:
		return Vector2.zero;
	}
	
	public float GetRTrigger()
	{
		//TODO:
		return 0f;
	}

	public float GetLTrigger()
	{
		//TODO:
		return 0f;
	}
	
	public bool IsButtonPressed(EGamePadButton button)
	{
		bool down = false;
		switch(button)
		{
		case EGamePadButton.A:
			down = Input.GetButtonDown("A_"+mIndex);
			break;
		case EGamePadButton.B:
			down = Input.GetButtonDown("B_"+mIndex);
			break;
		case EGamePadButton.X:
			down = Input.GetButtonDown("X_"+mIndex);
			break;
		case EGamePadButton.Y:
			down = Input.GetButtonDown("Y_"+mIndex);
			break;
		case EGamePadButton.Start:
			down = Input.GetButtonDown("Start_"+mIndex);
			break;
		case EGamePadButton.Select:
			down = Input.GetButtonDown("Back_"+mIndex);
			break;
		case EGamePadButton.RStick:
			down = Input.GetButtonDown("LS_"+mIndex);
			break;
		case EGamePadButton.LStick:
			down = Input.GetButtonDown("RS_"+mIndex);
			break;
		case EGamePadButton.RShoulder:
			down = Input.GetButtonDown("RB_"+mIndex);
			break;
		case EGamePadButton.LShoulder:
			down = Input.GetButtonDown("LB_"+mIndex);
			break;
		}

		return down && IsEnabled();
	}

	public bool IsButtonDown(EGamePadButton button)
	{
		bool down = false;
		switch(button)
		{
		case EGamePadButton.A:
			down = Input.GetButton("A_"+mIndex);
			break;
		case EGamePadButton.B:
			down = Input.GetButton("B_"+mIndex);
			break;
		case EGamePadButton.X:
			down = Input.GetButton("X_"+mIndex);
			break;
		case EGamePadButton.Y:
			down = Input.GetButton("Y_"+mIndex);
			break;
		case EGamePadButton.Start:
			down = Input.GetButton("Start_"+mIndex);
			break;
		case EGamePadButton.Select:
			down = Input.GetButton("Back_"+mIndex);
			break;
		case EGamePadButton.RStick:
			down = Input.GetButton("LS_"+mIndex);
			break;
		case EGamePadButton.LStick:
			down = Input.GetButton("RS_"+mIndex);
			break;
		case EGamePadButton.RShoulder:
			down = Input.GetButton("RB_"+mIndex);
			break;
		case EGamePadButton.LShoulder:
			down = Input.GetButton("LB_"+mIndex);
			break;
		}
		
		return down && IsEnabled();
	}

	public bool IsButtonReleased(EGamePadButton button)
	{
		bool down = false;
		switch(button)
		{
		case EGamePadButton.A:
			down = Input.GetButtonUp("A_"+mIndex);
			break;
		case EGamePadButton.B:
			down = Input.GetButtonUp("B_"+mIndex);
			break;
		case EGamePadButton.X:
			down = Input.GetButtonUp("X_"+mIndex);
			break;
		case EGamePadButton.Y:
			down = Input.GetButtonUp("Y_"+mIndex);
			break;
		case EGamePadButton.Start:
			down = Input.GetButtonUp("Start_"+mIndex);
			break;
		case EGamePadButton.Select:
			down = Input.GetButtonUp("Back_"+mIndex);
			break;
		case EGamePadButton.RStick:
			down = Input.GetButtonUp("LS_"+mIndex);
			break;
		case EGamePadButton.LStick:
			down = Input.GetButtonUp("RS_"+mIndex);
			break;
		case EGamePadButton.RShoulder:
			down = Input.GetButtonUp("RB_"+mIndex);
			break;
		case EGamePadButton.LShoulder:
			down = Input.GetButtonUp("LB_"+mIndex);
			break;
		}
		
		return down && IsEnabled();
	}

	public bool IsEnabled()
	{
		return mIsEnabled;
	}

	public void SetEnabled(bool enabled)
	{
		mIsEnabled = enabled;
	}
}
