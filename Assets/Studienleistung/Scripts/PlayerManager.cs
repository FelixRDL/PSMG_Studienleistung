using UnityEngine;
using System.Collections;

// Manager class for Player Figure
public class PlayerManager : MonoBehaviour
{
	// public vars
	public float movementSpeed;
	public float turnSpeed;

	public const int MAX_LIFE_POINTS = 100;
	public const int MEDIUM_LIFE_POINTS = 50;
	public const int CRIT_LIFE_POINTS = 20;

	private int mLifePoints;
	public int lifePoints {
		get {
			return mLifePoints;
		}
		set {
			mLifePoints = value;
			OnPlayerHealthChanged (value);
		}
	}

	private Vector3 startPosition;

	// modules
	private PlayerInputManager m_playerInputManager;

	// delegates
	public delegate void PlayerEvent (int val);
	public static event PlayerEvent OnPlayerHealthChanged;



	// initialize
	void Start ()
	{
		startPosition = gameObject.transform.position;
		m_playerInputManager = gameObject.AddComponent<PlayerInputManager> ();
		lifePoints = MAX_LIFE_POINTS;
	}

	// respawn player
	public void Respawn ()
	{
		m_playerInputManager.SetPlayerToPosition (startPosition);
		lifePoints = MAX_LIFE_POINTS;
	}

	// Getters
	public float GetMovementSpeed ()
	{
		return movementSpeed;
	}

	public float GetTurnSpeed ()
	{
		return turnSpeed;
	}
}
