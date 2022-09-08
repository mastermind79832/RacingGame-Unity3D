using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

	[SerializeField] private float m_Acceleration = 50; 
	[SerializeField] private float m_MaxSpeed  = 15;
	[SerializeField] private float m_Deceleration = 0.98f;
	[SerializeField] private float m_SteerAngle = 20;
	[SerializeField] private float m_Traction = 1;

	private Vector3 m_Speed;
	private Vector3 m_Steering;

	private Vector2 m_Input;

	private void Update()
	{
		GetInput();
		ShowDebugRay();

	}
	private void FixedUpdate()
	{
		ForwardMovement();
		Steering();
		Deceleration();
		LimitSpeed();
		Traction();
	}

	private void ShowDebugRay()
	{
		Debug.DrawRay(transform.position, m_Speed.normalized * 3, Color.red);
		Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
	}

	private void GetInput()
	{
	 	m_Input.y = Input.GetAxis("Vertical");
		m_Input.x = Input.GetAxis("Horizontal");
	}


	private void Traction() => m_Speed = Vector3.Lerp(m_Speed.normalized, transform.forward, m_Traction * Time.deltaTime) * m_Speed.magnitude;
	private void LimitSpeed() => m_Speed = Vector3.ClampMagnitude(m_Speed, m_MaxSpeed);
	private void Deceleration() => m_Speed *= m_Deceleration;
	private void Steering()
	{
		m_Steering = m_Input.x * m_SteerAngle * m_Speed.magnitude * Time.deltaTime * Vector3.up;
		transform.Rotate(m_Steering);
	}
	private void ForwardMovement()
	{
		m_Speed += m_Input.y * m_Acceleration * Time.deltaTime * transform.forward;
		transform.position += m_Speed * Time.deltaTime;
	}
}
