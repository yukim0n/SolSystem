﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCam : MonoBehaviour {

	// public Variables
	[Header("Movement")]
	public float SlowSpeed = 8f;
	public float FastSpeed = 15f;
	[Range(0,1)]
	public float MoveSmoothFactor = 0.75f;

	[Header("Rotation")]
	public float PitchSensitivity = 10f;
	public float MinimumPitch = -90f;
	public float MaximumPitch = 90f;
	public float YawSensitiviy = 10f;
	public bool InvertPitch = true;

	[Range(0,1)]
	public float RotationSmoothFactor = 0.15f;

	// private Variables

	Vector3 currentVelocity;
	Vector3 targetVelocity;

	Vector3 currentRotation;
	Vector3 targetRotation;

	float currentSpeed;

	void HandleInput(){
		targetVelocity = new Vector3 (
			Input.GetAxisRaw("Horizontal"),
			Input.GetAxisRaw("Jump"),
			Input.GetAxisRaw("Vertical")

		).normalized;

		currentSpeed = Input.GetButton ("Run") ? FastSpeed : SlowSpeed;
	
		targetRotation = new Vector3 (
			Mathf.Clamp (targetRotation.x + Input.GetAxis ("Mouse Y") * (InvertPitch?-1:1) * PitchSensitivity, MinimumPitch, MaximumPitch),
			targetRotation.y + Input.GetAxis("Mouse X") * YawSensitiviy
		);
	}

	void Move(){
		currentVelocity = Vector3.Lerp (currentVelocity, targetVelocity * currentSpeed * Time.deltaTime, MoveSmoothFactor);
		transform.Translate (currentVelocity);
	}

	void Rotate(){
		currentRotation = Vector3.Lerp (currentRotation, targetRotation, RotationSmoothFactor);
		transform.rotation = Quaternion.Euler (currentRotation);
	}

	private void Start() {
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void Update () {
		HandleInput ();
		Move ();
		Rotate ();
	}
}
