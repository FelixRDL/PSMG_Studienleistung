using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	// system state
	private bool isGameWon = false;
	private float timeSinceStart = 0.0f;

	// UI
	private bool isShowingOpenerButton = false;
	private GUIStyle styleLPs;
	private GUIStyle styleWon;
	// important game objects
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
		string text = "LP: " + player.lifePoints + "/" + PlayerManager.MAX_LIFE_POINTS;
		GUI.Label (new Rect (10, 10, 100, 100), text, styleLPs);

		// occasionally show opener button
		if (isShowingOpenerButton) {
			if (GUI.Button (new Rect (Screen.width / 2 - 100 / 2, Screen.height / 2 + 100 / 2, 100, 100), "OPEN")) {
				selectedDoor.Open ();
				GameObject.Find ("Root").GetComponent<SoundManager> ().PlaySound (SoundManager.DOOR_SOUND);
			}
		}

		if (isGameWon) {
			GUI.Label (new Rect (Screen.width / 2 - 100 / 2, Screen.height / 2 + 100 / 2, 100, 100), "YOU WON!", styleWon);
		}
	}

	//
	//
	// Player Callbacks

	private void OnPlayerHealthChanged (int newVal)
	{
		Debug.Log ("Player Health Changed to " + newVal);
		if (newVal < 0) {
			Debug.Log ("Respawn");
			GameObject.Find ("Player").GetComponent<PlayerManager> ().Respawn ();
		}
	}

	private void OnGameWon ()
	{
		Debug.Log ("Won");
		isGameWon = true;
		GameObject.Find ("Root").GetComponent<SoundManager> ().PlaySound (SoundManager.WIN_SOUND);

		StartCoroutine (Wait ());
	}

	IEnumerator Wait ()
	{
		yield return new WaitForSeconds (5);
		ResetGame ();
	}
		
	//
	//
	// UI
	public void ShowDoorOpenButton (Door connectedDoor)
	{
		isShowingOpenerButton = true;
		selectedDoor = connectedDoor;
	}

	public void HideDoorOpenButton ()
	{
		isShowingOpenerButton = false;
		selectedDoor = null;
	}

	public void ShowGameWon ()
	{
		Debug.Log ("Won");
	}

	public void ResetGame ()
	{
		Debug.Log ("Reset");
		isGameWon = false;
		GameObject.Find ("Player").GetComponent <PlayerManager> ().Respawn ();

		foreach (BombTrigger bomb in gameObject.GetComponentsInChildren<BombTrigger>()) {
			bomb.SetActive (true);
		}

		foreach (Door door in gameObject.GetComponentsInChildren<Door>()) {
			door.Close ();
		}
	}

}
