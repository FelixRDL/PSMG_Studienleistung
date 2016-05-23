using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
	Door m_door;

	void Start ()
	{
		m_door = gameObject.transform.parent.parent.GetComponentInChildren<Door> ();
	}

	// Use this for initialization
	void OnTriggerEnter (Collider other)
	{
		Debug.Log ("enter " + other);

		// if colliding object is player
		if (other.gameObject.GetComponent<PlayerManager> () != null && !m_door.isOpen) {
			GameObject root = GameObject.Find ("Root");

			root.GetComponent<GameManager> ().ShowDoorOpenButton (m_door);
			// play 'bing'
			root.GetComponent<AudioSource> ().Play ();
		}
	}

	void OnTriggerExit (Collider other)
	{
		Debug.Log ("leave " + other);
		GameObject.Find ("Root").GetComponent<GameManager> ().HideDoorOpenButton ();
	}
}
