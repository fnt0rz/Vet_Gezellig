using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : MonoBehaviour {

	[SerializeField] float superJumpPower = 10f;
	float currentJumpPower;
	PlayerMovement playerMovement;

	private void Start() {
		playerMovement = FindObjectOfType<PlayerMovement>();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player")
		{
			currentJumpPower = playerMovement.jumpPower;
			playerMovement.jumpPower = superJumpPower;
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player")
		{
			playerMovement.jumpPower = currentJumpPower;
		}
	}	
	
}
