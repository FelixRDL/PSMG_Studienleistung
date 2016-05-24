/// <summary>
/// Player manager.
/// 
/// Central Resource Object for the Player.
/// </summary>

using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
	// inspector-ready tuning params
	public float movementSpeed;
	public float turnSpeed;
	// constant values for movement speed, when lifepoints are low
	public const int MAX_LIFE_POINTS = 100;
	public const int MEDIUM_LIFE_POINTS = 50;
	public const int CRIT_LIFE_POINTS = 20;
	// internal representation of lifepoints and public porperty
	private int mLifePoints;
	public int lifePoints {
		get {
			return mLifePoints;
		}
		set {
			mLifePoints = value;
			// update health factor for movement speed when lp change
			if (value <= 0) {
				healthFactor = 0;
			} else if (value <= PlayerManager.CRIT_LIFE_POINTS)
				healthFactor = .1f;
			else if (value <= PlayerManager.MEDIUM_LIFE_POINTS)
				healthFactor = .5f;
			OnPlayerHealthChanged (value);
		}
	}
	private float healthFactor = 1.0f;
	// startposition of the player for respawns. Uses the first known position of the player
	private Vector3 startPosition;
	// associated modules
	private PlayerInputManager m_playerInputManager;
	// delegates
	public delegate void PlayerEvent (int val);
	public static event PlayerEvent OnPlayerHealthChanged;

	/// <summary>
	/// Initialize Player figure.
	/// </summary>
	void Start ()
	{
		// initialize starting position for respawning
		startPosition = gameObject.transform.position;
		// initially add Input Manager
		m_playerInputManager = gameObject.AddComponent<PlayerInputManager> ();
		// set life points to maximum
		lifePoints = MAX_LIFE_POINTS;
	}

	/// <summary>
	/// Respawn the player back to his starting point and fill up its lifepoints.
	/// </summary>
	public void Respawn ()
	{
		m_playerInputManager.SetPlayerToPosition (startPosition);
		lifePoints = MAX_LIFE_POINTS;
	}

	// Getters
	public float GetMovementSpeed ()
	{		
		return movementSpeed * healthFactor;
	}

	public float GetTurnSpeed ()
	{
		return turnSpeed;
	}
}
