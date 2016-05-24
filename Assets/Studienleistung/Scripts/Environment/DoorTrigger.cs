/// <summary>
/// Door trigger.
/// 
/// The door triggers calls back to the game manager to manipulate UI, when a player has entered the trigger zone.
/// </summary>

using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
	// the door, that is associated to the trigger
	private Door m_door;

	/// <summary>
	/// Get the own door through hierarchy.
	/// </summary>
	void Start ()
	{
		m_door = gameObject.transform.parent.parent.GetComponentInChildren<Door> ();
	}

	/// <summary>
	/// Notifies Game Manager, when zone is entered. Also plays a nice sound.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter (Collider other)
	{
		Debug.Log ("enter " + other);

		// if colliding object is player
		if (other.gameObject.GetComponent<PlayerManager> () != null && !m_door.isOpen) {
			GameObject root = GameObject.Find ("Root");
			// show the button to open the associated door
			root.GetComponent<GameManager> ().SelectDoor (m_door);
			// play 'bing'
			root.GetComponent<SoundManager> ().PlaySound (SoundManager.TRIGGER_SOUND);
		}
	}

	/// <summary>
	/// If player exits the zone, hide UI and unselect door.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerExit (Collider other)
	{
		// if colliding g o is player
		if (other.gameObject.GetComponent<PlayerManager> ()) {
			Debug.Log ("leave " + other);
			GameObject.Find ("Root").GetComponent<GameManager> ().DeselectDoor ();
		}
	}
}
