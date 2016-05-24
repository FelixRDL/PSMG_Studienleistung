/// <summary>
/// Player input manager.
/// 
/// Handles player controls, movement and positions.
/// </summary>

using UnityEngine;
using System.Collections;

public class PlayerInputManager : MonoBehaviour
{
	// axis names
	string axisMovement = "movementAxis";
	string axisTurn = "turnAxis";
	// components
	Rigidbody m_Rigidbody;
	PlayerManager m_Resources;

	//
	//
	//

	/// <summary>
	/// Initialize the references.
	/// </summary>
	public void Start ()
	{
		m_Rigidbody = gameObject.GetComponent<Rigidbody> ();
		m_Resources = gameObject.GetComponent<PlayerManager> ();
	}

	/// <summary>
	/// Update the player movement.
	/// </summary>
	public void FixedUpdate ()
	{
		Move ();
		Turn ();
	} 

	//
	//
	//

	/// <summary>
	/// Execute linear movement.
	/// </summary>
	public void Move ()
	{

		// calculate movement amount
		float movementValue = Input.GetAxis (axisMovement);
		float movementSpeed = m_Resources.GetMovementSpeed ();
		Vector3 movementDirection = m_Rigidbody.transform.forward;
		Vector3 movement = m_Rigidbody.position + movementDirection * movementValue * Time.deltaTime * movementSpeed;
		// execute movement
		m_Rigidbody.MovePosition (movement);
	}

	/// <summary>
	/// Execute angular movement.
	/// </summary>
	public void Turn ()
	{
		// calculate rotation amount
		float rotationValue = Input.GetAxis (axisTurn);
		float rotationSpeed = m_Resources.GetTurnSpeed ();
		float turn = rotationValue * rotationSpeed * Time.deltaTime;
		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);
		// execute rotation
		m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);

	}

	//
	//
	//

	/// <summary>
	/// Simple function to immediately move player to a position.
	/// </summary>
	/// <param name="newPosition">New position.</param>
	public void SetPlayerToPosition (Vector3 newPosition)
	{
		Debug.Log ("Set player to new position");
		gameObject.transform.position = newPosition;
	}
}
