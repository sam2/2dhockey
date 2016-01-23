using UnityEngine;
using System.Collections;

public interface IPlayerControls 
{
	Player GetSelectedPlayer();
	void SelectPlayer(Player hockeyPlayer);
}
