using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
	private bool isCollidingWithPlayer = false;


	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.GetComponent<PlayerManager> () != null) {
			GameObject.Find ("Root").GetComponent<SoundManager> ().PlaySound (SoundManager.TALK_SOUND);
			isCollidingWithPlayer = true;
		}
	}

	void OnTriggerExit (Collider other)
	{		
		if (other.gameObject.GetComponent<PlayerManager> () != null) {
			isCollidingWithPlayer = false;
		}

	}

	/// <summary>
	/// Refresh the monster - rotate towards player, when close enough
	/// </summary>
	public void Refresh ()
	{
		if (isCollidingWithPlayer) {
			gameObject.transform.LookAt (GameObject.Find ("Player").transform);
			// compensate offset in model
			gameObject.transform.Rotate (new Vector3 (0, 90, 0));
		}
	}
}
