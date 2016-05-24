/// <summary>
/// User interface manager.
/// 
/// A class for controlling the UI features of the game.
/// </summary>

using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
	// UI
	private bool isShowingOpenerButton = false;
	private GUIStyle styleLPs;
	private GUIStyle styleWon;
	// associated objects
	private PlayerManager player;
	private GameManager game;

	void Start ()
	{
		player = GameObject.Find ("Player").GetComponent<PlayerManager> ();
		game = gameObject.GetComponent<GameManager> ();
		// initialize UI styles
		styleLPs = new GUIStyle ();
		styleLPs.normal.textColor = Color.gray;
		styleWon = new GUIStyle ();
		styleWon.normal.textColor = Color.red;
		styleWon.fontSize = 40;
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
		string textTime = "Time: " + (int)gameObject.GetComponent<GameManager> ().GetTimeSinceStart ();
		GUI.Label (new Rect (10, 30, 100, 20), textTime, styleLPs);
		
		// occasionally show opener button
		if (isShowingOpenerButton) {
			if (GUI.Button (new Rect (Screen.width / 2 - 100 / 2, Screen.height / 2 + 100 / 2, 100, 100), "OPEN")) {
				game.OpenSelectedDoor ();
				GameObject.Find ("Root").GetComponent<SoundManager> ().PlaySound (SoundManager.DOOR_SOUND);
			}
		}
		
		// if the game is won, show the winner label
		if (game.GetIsGameWon ()) {
			GUI.Label (new Rect (Screen.width / 2 - 100 / 2, Screen.height / 2 + 100 / 2, 100, 100), "YOU WON!", styleWon);
			GUI.Label (new Rect (Screen.width / 2 - 170 / 2, Screen.height / 2 + 200 / 2, 100, 100), "Time: " + (int)game.GetTimeSinceStart () + " seconds", styleWon);
		}
		// if the game is lost, show the loser label
		if (game.GetIsGameLost ()) {
			GUI.Label (new Rect (Screen.width / 2 - 100 / 2, Screen.height / 2 + 100 / 2, 100, 100), "YOU DIED!", styleWon);
		}
	}

	
	//
	//
	// UI Interaction
	
	/// <summary>
	/// Shows the button to open the door
	/// </summary>
	/// <param name="connectedDoor">Connected door.</param>
	public void ShowDoorOpenButton ()
	{
		isShowingOpenerButton = true;
	}
	
	/// <summary>
	/// Hides the button to show the door
	/// </summary>
	public void HideDoorOpenButton ()
	{
		isShowingOpenerButton = false;
	}
}
