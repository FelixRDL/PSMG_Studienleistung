using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private Door[] doors;

	// UI
	private bool isShowingOpenerButton = false;
	private Door selectedDoor;

	private GUIStyle styleLPs;
	private GUIStyle styleWon;

	private bool isGameWon = false;

	void Start ()
	{
		PlayerManager.OnPlayerHealthChanged += OnPlayerHealthChanged;
		VictoryTrigger.OnGameWon += OnGameWon;
		doors = gameObject.GetComponentsInChildren<Door> ();

		styleLPs = new GUIStyle ();
		styleLPs.normal.textColor = Color.gray;

		styleWon = new GUIStyle ();
		styleWon.normal.textColor = Color.red;
		styleWon.fontSize = 40;
	}

	void FixedUpdate ()
	{
		Debug.Log ("main camera " + Camera.current);

		foreach (Door door in doors) {
			door.Refresh ();
		}
	}

	void OnGUI ()
	{
		PlayerManager player = GameObject.Find ("Player").GetComponent <PlayerManager> ();

		string text = "LP: " + player.lifePoints + "/" + PlayerManager.MAX_LIFE_POINTS;
		GUI.Label (new Rect (10, 10, 100, 100), text, styleLPs);

		// occasionally show opener button
		if (isShowingOpenerButton) {
			if (GUI.Button (new Rect (Screen.width / 2 - 100 / 2, Screen.height / 2 + 100 / 2, 100, 100), "OPEN")) {
				selectedDoor.Open ();
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
