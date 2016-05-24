/// <summary>
/// Victory trigger.
/// 
/// Simply trigger, when player has won the game.
/// </summary>
using UnityEngine;
using System.Collections;

public class VictoryTrigger : MonoBehaviour
{
	public delegate void VictoryEvent ();
	public static event VictoryEvent OnGameWon;

	void OnTriggerEnter ()
	{
		OnGameWon ();
	}
}
