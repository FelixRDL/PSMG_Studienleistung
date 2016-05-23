using UnityEngine;
using System.Collections;

public class PlayerInputManager : MonoBehaviour
{
	// axis
	string axisMovement = "movementAxis";
	string axisTurn = "turnAxis";

	// components
	Rigidbody m_Rigidbody;
	PlayerManager m_Resources;

	//
	//
	//

	public void Start ()
	{
		m_Rigidbody = gameObject.GetComponent<Rigidbody> ();
		m_Resources = gameObject.GetComponent<PlayerManager> ();
	}

	public void FixedUpdate ()
	{
		Move ();
		Turn ();
	} 

	//
	//
	//

	public void Move ()
	{
		float healthFactor = 1.0f;

		if (m_Resources.lifePoints <= PlayerManager.CRIT_LIFE_POINTS)
			healthFactor = .1f;
		else if (m_Resources.lifePoints <= PlayerManager.MEDIUM_LIFE_POINTS)
			healthFactor = .5f;


		float movementValue = Input.GetAxis (axisMovement);
		float movementSpeed = m_Resources.GetMovementSpeed ();
		Vector3 movementDirection = m_Rigidbody.transform.forward;


		Vector3 movement = m_Rigidbody.position + movementDirection * movementValue * Time.deltaTime * movementSpeed * healthFactor;
		m_Rigidbody.MovePosition (movement);
	}

	public void Turn ()
	{
		float rotationValue = Input.GetAxis (axisTurn);
		float rotationSpeed = m_Resources.GetTurnSpeed ();
		float turn = rotationValue * rotationSpeed * Time.deltaTime;

		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

		m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);

	}

	//
	//
	//

	public void SetPlayerToPosition (Vector3 newPosition)
	{
		Debug.Log ("Set player to new position");
		gameObject.transform.position = newPosition;
	}
}
