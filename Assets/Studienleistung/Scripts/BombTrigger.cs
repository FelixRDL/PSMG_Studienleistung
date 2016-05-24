using UnityEngine;
using System.Collections;

public class BombTrigger : MonoBehaviour
{
	public int bombDamage;

	void OnTriggerEnter (Collider other)
	{
		PlayerManager player = other.gameObject.GetComponent<PlayerManager> ();
		if (player != null) {
			GameObject.Find ("Root").GetComponent<SoundManager> ().PlaySound (SoundManager.BOOM_SOUND);
			player.lifePoints -= bombDamage;
			SetActive (false);
		}
	}

	public void SetActive (bool isActive)
	{
		this.GetComponent<Renderer> ().enabled = isActive;
		this.GetComponent<Collider> ().enabled = isActive;
	}
}
