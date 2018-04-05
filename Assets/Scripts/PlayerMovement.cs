using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

[SerializeField] float controllSpeed = 1f;
[SerializeField] float jumpPower = 12f;
Rigidbody playerBody;
bool isGrounded;

    private void Awake() {
     	gameObject.tag = "Player";   
    }

	private void Start() {
	playerBody = GetComponent<Rigidbody>();
	
	}
	void OnCollisionStay(Collision playerCollider) {
		if (!isGrounded && playerBody.velocity.y <= Mathf.Epsilon)
		{
			isGrounded = true;
		}
	}
	private void Update()
    {
        PlayerJump();
    }
    private void PlayerJump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded)
        {
			isGrounded = false;
            playerBody.velocity = new Vector3(playerBody.velocity.x, jumpPower, playerBody.velocity.z);
        }
    }

    void FixedUpdate()
    {
        HorizontalMovement();
    }

    private void HorizontalMovement()
    {
        var xThrust = CrossPlatformInputManager.GetAxis("Horizontal");
		if (xThrust > 0)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.x,0f, transform.localRotation.z); //TODO: Make smooth turn
        }
        else if(xThrust < 0) {
			transform.localRotation = Quaternion.Euler(transform.localRotation.x,180f,transform.localRotation.z);
		}
        var xOffset = xThrust * controllSpeed;
        var rawXPos = transform.position.x + xOffset;
        transform.position = new Vector3(rawXPos, transform.position.y, transform.position.z);
    }
}
