using UnityEngine;
using System.Collections;

public class PlayerMovementManager
{
	private float turnSpeed = 40.0f;
	private float moveSpeed = 4.0f;

	private float movementVal = 0;
	private float turningVal = 0;

	private Rigidbody m_rigidbody;
	private PlayerManager resources;



	public PlayerMovementManager (GameObject player, PlayerManager resources)
	{
		m_rigidbody = player.GetComponent<Rigidbody> ();
		this.resources = resources;
	}

	public void Update ()
	{
		Move ();
		Turn ();
	}

	private void Move ()
	{
		Vector3 movement = m_rigidbody.transform.forward * movementVal * Time.deltaTime;
		m_rigidbody.MovePosition (m_rigidbody.position + movement * resources.GetMovementSpeed ());
	}

	private void Turn ()
	{
		float turn = turningVal * Time.deltaTime * resources.GetTurnSpeed ();
		Quaternion turnRotation = Quaternion.Euler (0f, turningVal, 0f);
		m_rigidbody.MoveRotation (m_rigidbody.rotation * turnRotation);
	}

	public void setMovement (float value)
	{
		movementVal = value;
	}

	public void setTurning (float value)
	{
		turningVal = value;
	}
}
