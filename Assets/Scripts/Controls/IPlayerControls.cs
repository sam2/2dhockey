using UnityEngine;
using System.Collections;

public interface IPlayerControls 
{
	Skater GetSelectedPlayer();
	void SelectPlayer(Skater hockeyPlayer);
}
