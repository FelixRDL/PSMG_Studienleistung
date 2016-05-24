/// <summary>
/// Door.
/// 
/// Script for opening and closing the doors and playing the associated animation.
/// </summary>

using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
	// movement speed of the doors whilst opening
	private static float doorSpeed = 1;
	// stored y coordinates for open and closed doors
	private const float yOpen = 1.5f, yClosed = 3.0f;
	// logical representation of the state of the door, internal var and public property
	private bool m_isOpen = false;
	public bool isOpen {
		get {
			return m_isOpen;
		}
		set {
			m_isOpen = value;
		}
	}


	/// <summary>
	/// Initialize opening animation
	/// </summary>
	public void Open ()
	{
		m_isOpen = true;
		GameObject.Find ("Root").GetComponent<GameManager> ().DeselectDoor ();
	}

	/// <summary>
	/// Reset the door's state to a 'closed' y coordinate 
	/// </summary>
	public void Close ()
	{
		m_isOpen = false;
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, yClosed, gameObject.transform.position.z);
	}


	/// <summary>
	/// My Update function, called by the GameManager each frame. Executes the animation.
	/// </summary>
	public void Refresh ()
	{
		// if the internal state of the door is "open", but the door has not moved far enough into the floor, it will continue its animation
		if (m_isOpen && gameObject.transform.position.y > yOpen) {
			Debug.Log (gameObject.GetInstanceID () + " open door");
			gameObject.transform.Translate (0f, - doorSpeed * Time.deltaTime, 0f);
		}

		// script for closing door again (animated), obsolete
		/*else if (!isOpen && gameObject.transform.position.y < yClosed) {
			gameObject.transform.Translate (0f, doorSpeed * Time.deltaTime, 0f);
			Debug.Log (gameObject.GetInstanceID () + " close door");
		}*/
	}
}
