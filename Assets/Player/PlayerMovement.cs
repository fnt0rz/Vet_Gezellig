using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

    public float speed = 10.0F;
    [SerializeField] float controllSpeed = 1f;
    [SerializeField] float jumpPower = 12f;
    Rigidbody playerBody;
    static Animator animator;
    float translation;
    [SerializeField] bool isGrounded;
    [SerializeField] float yVelocity;
    bool facingRight;
    int jumpCount;


    private void Awake() {
     	gameObject.tag = "Player";   
    }

	private void Start() {
    facingRight = true;
    animator = GetComponent<Animator>();
	playerBody = GetComponent<Rigidbody>();
	
	}
	void OnCollisionEnter(Collision playerCollider) {
		if (!isGrounded)
		{
			isGrounded = true;
            jumpCount = 1;
            animator.ResetTrigger("isJumping");
            animator.SetBool("isLanding", false);
		}
	}
	private void Update()
    {
        PlayerJump();  
        RunningAnimation();      
        yVelocity = playerBody.velocity.y;
        
    }

    void FixedUpdate() 
    {
        InputTranslator();
        flipPlayer();
        RunningAnimation();
        LayerHandler();
        
    }

    private void InputTranslator()
    {
        translation = Input.GetAxis("Horizontal") * speed;
        MovementHandeler();
    }

    private void flipPlayer()
    {
        if (translation > 0 && !facingRight || translation < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.z *= -1;
            transform.localScale = (localScale);
        }
    }

    private void RunningAnimation()
    {
        if (translation != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void MovementHandeler()
    {
        if (playerBody.velocity.y < -1)
        {   
            animator.SetBool("isLanding", true);
        }

        translation *= Time.deltaTime;
        transform.Translate(0, 0, translation);

    }

    private void PlayerJump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded && jumpCount > 0)
        {   
            jumpCount -= 1;
            animator.SetTrigger("isJumping");
            playerBody.velocity = new Vector3(playerBody.velocity.x,jumpPower,playerBody.velocity.z);
			isGrounded = false;
        }
    }

    private void LayerHandler() 
    {
        if (!isGrounded)
        {
            animator.SetLayerWeight(1,1);
        }
        else
        {
            animator.SetLayerWeight(1,0);
        }
    }
}
