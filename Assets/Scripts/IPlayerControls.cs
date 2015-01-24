using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IPlayerControls {

	GameObject GetGameObject();
	void UpdatePath();
	PlayerControlsView GetView();
	Queue<Vector2>  GetPath();
}
