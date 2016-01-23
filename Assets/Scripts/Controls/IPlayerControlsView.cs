using UnityEngine;
using System.Collections;

public interface IPlayerControlsView  {

	void ShowChargeUpShot(float percent, Vector2 origin, Vector2 direction);
	void ChangeSelected(Player player);
	
}
