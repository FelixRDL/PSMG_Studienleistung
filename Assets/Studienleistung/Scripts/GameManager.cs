/// <summary>
/// Game manager.
/// 
/// Controls the game state and environment.
/// 
/// NOTE: Unfortunately I recognised, that this class has not been conceptionalized static per se. Since it is too late to correct the issue in all associated scripts, 
/// I have implemented the system singleton-like, as component of a individual "root" game object.
/// </summary>


using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	// system state
	private bool isGameWon = false;
	private bool isGameLost = false;
	private float timeSinceStart = 0.0f;
	private bool isCountingTime = true;
	// important game object references
	private Door[] doors;
	private Door selectedDoor;
	private Monster monster;
	PlayerManager player;
	UIManager ui;


	void Start ()
	{
		// add callbacks
		PlayerManager.OnPlayerHealthChanged += OnPlayerHealthChanged;
		VictoryTrigger.OnGameWon += OnGameWon;
		// initialize references
		doors = gameObject.GetComponentsInChildren<Door> ();
		monster = gameObject.GetComponentInChildren<Monster> ();
		player = gameObject.GetComponentInChildren <PlayerManager> ();
		ui = gameObject.GetComponent<UIManager> ();
	}

	/// <summary>
	/// Central Updating functions for environment and monsters
	/// </summary>
	void FixedUpdate ()
	{
		if (isCountingTime)
			timeSinceStart += Time.deltaTime;

		foreach (Door door in doors) {
			door.Refresh ();
		}

		monster.Refresh ();
	}

	//
	//
	// Player Callbacks

	/// <summary>
	/// Check, if the player has died and react to this. Callback coming from PlayerManager
	/// </summary>
	/// <param name="newVal">New value.</param>
	private void OnPlayerHealthChanged (int newVal)
	{
		Debug.Log ("Player Health Changed to " + newVal);
		if (newVal <= 0) {
			OnGameLost ();
		}
	}

	/// <summary>
	/// Check, if the goal has been Reached. Coming from VictoryTrigger
	/// </summary>
	private void OnGameWon ()
	{
		// update system status
		isGameWon = true;
		// disable timer
		isCountingTime = false;
		// play victory sound
		GameObject.Find ("Root").GetComponent<SoundManager> ().PlaySound (SoundManager.WIN_SOUND);

		// wait
		StartCoroutine (Wait ());
	}

	/// <summary>
	/// Called, when the player died.Similar to OnGameWon();
	/// </summary>
	private void OnGameLost ()
	{
		isGameLost = true;
		isCountingTime = false;
		GameObject.Find ("Root").GetComponent<SoundManager> ().PlaySound (SoundManager.TALK_SOUND);

		StartCoroutine (Wait ());
	}

	// wait and reset game then
	IEnumerator Wait ()
	{
		yield return new WaitForSeconds (5);
		ResetGame ();
	}


	//
	//
	// Game Control function

	public void ResetGame ()
	{
		// reset counter
		isCountingTime = true;
		timeSinceStart = 0;
		// reset game state
		isGameWon = false;
		isGameLost = false;
		// respawn player
		GameObject.Find ("Player").GetComponent <PlayerManager> ().Respawn ();
		// reactivate bombs
		foreach (BombTrigger bomb in gameObject.GetComponentsInChildren<BombTrigger>()) {
			bomb.SetActive (true);
		}
		// close all doors
		foreach (Door door in gameObject.GetComponentsInChildren<Door>()) {
			door.Close ();
		}
	}

	//
	//
	// Getters
	public float GetTimeSinceStart ()
	{
		return timeSinceStart;
	}

	public bool GetIsGameWon ()
	{
		return isGameWon;
	}

	public bool GetIsGameLost ()
	{
		return isGameLost;
	}

	//
	//
	// environment interaction

	public void SelectDoor (Door connectedDoor)
	{
		ui.ShowDoorOpenButton ();
		selectedDoor = connectedDoor;
	}

	public void OpenSelectedDoor ()
	{
		selectedDoor.Open ();
	}
	
	/// <summary>
	/// If a door trigger has been used or exited, hide the door button and deselect the door.
	/// </summary>
	public void DeselectDoor ()
	{
		ui.HideDoorOpenButton ();
		selectedDoor = null;
	}
}
