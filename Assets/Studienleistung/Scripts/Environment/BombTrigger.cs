/// <summary>
/// BombTrigger.
/// 
/// A trigger affecting the attached bomb to explode.
/// </summary>
using UnityEngine;
using System.Collections;

public class BombTrigger : MonoBehaviour
{
	// the amount of lifepoints, that the player will lose around an exploding bomb
	public int bombDamage;

	/// <summary>
	/// Trigger the bomb and deal explosive damage.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter (Collider other)
	{
		// try to get the playermanager component from colliding object
		// affirm, that other collider really was the player
		PlayerManager player = other.gameObject.GetComponent<PlayerManager> ();

		if (player != null) {
			// play boom sound
			GameObject.Find ("Root").GetComponent<SoundManager> ().PlaySound (SoundManager.BOOM_SOUND);
			// deal damage
			player.lifePoints -= bombDamage;
			// deactivate bomb
			SetActive (false);
		}
	}

	/// <summary>
	/// Sets the bomb armed or disabled.
	/// </summary>
	/// <param name="isActive">If set to <c>true</c> is active.</param>
	public void SetActive (bool isActive)
	{
		this.GetComponent<Renderer> ().enabled = isActive;
		this.GetComponent<Collider> ().enabled = isActive;
	}
}
