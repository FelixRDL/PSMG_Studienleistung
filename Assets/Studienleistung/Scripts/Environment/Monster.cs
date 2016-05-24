/// <summary>
/// Monster.
/// 
/// A funny class to make the Enemy Object turn and make strange noises.
/// </summary>

using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
	private bool isCollidingWithPlayer = false;

	/// <summary>
	/// Play funny noise, when player enters range.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.GetComponent<PlayerManager> () != null) {
			GameObject.Find ("Root").GetComponent<SoundManager> ().PlaySound (SoundManager.TALK_SOUND);
			isCollidingWithPlayer = true;
		}
	}

	/// <summary>
	/// Stop following, when player leaves range.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerExit (Collider other)
	{		
		if (other.gameObject.GetComponent<PlayerManager> () != null) {
			isCollidingWithPlayer = false;
		}

	}

	/// <summary>
	/// Refresh the monster - rotate towards player, when close enough.
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
