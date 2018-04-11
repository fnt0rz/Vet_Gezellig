using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour {
	[SerializeField] Vector3 movementVector = new Vector3(0f,20f,0f);
	[Range(0,1)] [SerializeField] float movementFactor;
	[SerializeField] float movementPeriod = 10f;
	Vector3 startingPos;

	// Use this for initialization
	void Start () {
		startingPos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Movement();
	}

	void OnCollisionEnter(Collision other) {
		if(other.transform.tag == "Player")	
		{
			other.transform.parent = gameObject.transform;
		}
	}

	private void OnCollisionExit(Collision other) {
		if (other.transform.tag == "Player")
		{
			other.transform.parent = null;
		}
	}

    private void Movement()
    {
        if (movementPeriod <= Mathf.Epsilon) {return;}

		float cycles = Time.time / movementPeriod;
		const float tau = Mathf.PI * 2f;
		float rawSinWave = Mathf.Sin(cycles * tau);

		movementFactor = rawSinWave / 2f + 0.5f;

		Vector3 offset = movementVector * movementFactor;
		transform.position = startingPos + offset;
    }
}
