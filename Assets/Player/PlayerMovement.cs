using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

    public bool isGrounded;
    public float speed = 10.0F;
    public float jumpPower = 12f;
    Rigidbody playerBody;
    static Animator animator;
    public float translation;
    bool facingRight;
    public bool moveEnabled = true;
    PlayerStats playerStats;


    private void Awake() {
     	gameObject.tag = "Player";   
    }

	private void Start() {
    facingRight = true;
    animator = GetComponent<Animator>();
	playerBody = GetComponent<Rigidbody>();
    playerStats = FindObjectOfType<PlayerStats>();
	
	}
	void OnCollisionEnter(Collision playerCollider) { //FIXME: Needs to be fixed with head
		if (!isGrounded)
		{
			isGrounded = true;
            animator.ResetTrigger("isJumping");
            animator.SetBool("isLanding", false);
		}
	}
	private void Update()
    {
        PlayerJump();  
        RunningAnimation();  
        moveChecker();    
    }

    private void moveChecker()
    {
        if (playerStats.isAlive)
        {
            moveEnabled = true;
        }
        else
        {
            moveEnabled = false;
        }
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
        if (moveEnabled)
        {
            translation = Input.GetAxis("Horizontal") * speed;
            MovementHandeler();
        }

    }

    private void flipPlayer()
    {
        if (translation > 0 && !facingRight || translation < 0 && facingRight)
        {
            switch (facingRight)
            {
                case true:
                transform.rotation = Quaternion.Euler(transform.rotation.x,270f,transform.rotation.z);
                facingRight = !facingRight;
                break;
                case false:
                transform.rotation = Quaternion.Euler(transform.rotation.x,90f,transform.rotation.z);
                facingRight = !facingRight;
                break;
                default:
                break;
            }

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
            isGrounded = false;
            animator.SetBool("isLanding", true);
        }

        playerBody.velocity = new Vector3(translation,playerBody.velocity.y,playerBody.velocity.z);

    }

    private void PlayerJump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded && moveEnabled)
        {   
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
