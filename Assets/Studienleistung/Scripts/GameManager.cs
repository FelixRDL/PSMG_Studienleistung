/// <summary>
/// Game manager.
/// 
/// Controls the game state, environment and UI functions.
/// </summary>


using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	// system state
	private bool isGameWon = false;
	private float timeSinceStart = 0.0f;
	private bool isCountingTime = true;

	// UI
	private bool isShowingOpenerButton = false;
	private GUIStyle styleLPs;
	private GUIStyle styleWon;

	// important game object references
	private Door[] doors;
	private Door selectedDoor;
	private Monster monster;
	PlayerManager player;


	void Start ()
	{
		// add callbacks
		PlayerManager.OnPlayerHealthChanged += OnPlayerHealthChanged;
		VictoryTrigger.OnGameWon += OnGameWon;

		// initialize references
		doors = gameObject.GetComponentsInChildren<Door> ();
		monster = gameObject.GetComponentInChildren<Monster> ();
		player = gameObject.GetComponentInChildren <PlayerManager> ();

		// initialize UI styles
		styleLPs = new GUIStyle ();
		styleLPs.normal.textColor = Color.gray;
		styleWon = new GUIStyle ();
		styleWon.normal.textColor = Color.red;
		styleWon.fontSize = 40;
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

	/// <summary>
	/// Draw UI Layer
	/// </summary>
	void OnGUI ()
	{
		// LP label
		string textLP = "LP: " + player.lifePoints + "/" + PlayerManager.MAX_LIFE_POINTS;
		GUI.Label (new Rect (10, 10, 100, 20), textLP, styleLPs);
		// time label
		string textTime = "Time: " + (int)timeSinceStart;
		GUI.Label (new Rect (10, 30, 100, 20), textTime, styleLPs);

		// occasionally show opener button
		if (isShowingOpenerButton) {
			if (GUI.Button (new Rect (Screen.width / 2 - 100 / 2, Screen.height / 2 + 100 / 2, 100, 100), "OPEN")) {
				selectedDoor.Open ();
				GameObject.Find ("Root").GetComponent<SoundManager> ().PlaySound (SoundManager.DOOR_SOUND);
			}
		}

		// if the game is won, show the winner label
		if (isGameWon) {
			GUI.Label (new Rect (Screen.width / 2 - 100 / 2, Screen.height / 2 + 100 / 2, 100, 100), "YOU WON!", styleWon);
			GUI.Label (new Rect (Screen.width / 2 - 170 / 2, Screen.height / 2 + 200 / 2, 100, 100), "Time: " + (int)timeSinceStart + " seconds", styleWon);
		}
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
		if (newVal < 0) {
			Debug.Log ("Respawn");
			GameObject.Find ("Player").GetComponent<PlayerManager> ().Respawn ();
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

	// wait and reset game then
	IEnumerator Wait ()
	{
		yield return new WaitForSeconds (5);
		ResetGame ();
	}
		
	//
	//
	// UI Functions

	/// <summary>
	/// If a door trigger has been entered, the game should show the door open button.
	/// </summary>
	/// <param name="connectedDoor">Connected door.</param>
	public void ShowDoorOpenButton (Door connectedDoor)
	{
		isShowingOpenerButton = true;
		selectedDoor = connectedDoor;
	}

	/// <summary>
	/// If a door trigger has been used or exited, hide the door button and deselect the door.
	/// </summary>
	public void HideDoorOpenButton ()
	{
		isShowingOpenerButton = false;
		selectedDoor = null;
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
}
