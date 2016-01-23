using UnityEngine;
using System.Collections;

public interface IGamePad  {
	
	Vector2 GetLeftStick();
	Vector2 GetRightStick();

	Vector2 GetDPad();

	float GetRTrigger();
	float GetLTrigger();

	bool IsButtonDown(EGamePadButton button);
	bool IsButtonPressed(EGamePadButton button);
	bool IsButtonReleased(EGamePadButton button);

	bool IsEnabled();
	void SetEnabled(bool enabled);
}

public enum EGamePadButton
{
	A,
	B,
	X,
	Y,
	Start,
	Select,
	LShoulder,
	RShoulder,
	LStick,
	RStick
}
