using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

 //TODO: Move animator different script! 

    [SerializeField] float floatTime = 10f;
    public bool isGrounded;
    public float speed = 10.0F;
    public float jumpPower = 14f;
    float doubleJumpPower = 10f;
    Rigidbody playerBody;
    static Animator animator;
    float translation;
    bool facingRight;
    public bool moveEnabled = true;
    PlayerStats playerStats;
    public int maxJumpes = 1;
    public int remainingJumps;
    PlayerSwitcher playerSwitcher;
    


    private void Awake() {
     	gameObject.tag = "Player";   
    }

    private void Start() {
        playerSwitcher = FindObjectOfType<PlayerSwitcher>();
        playerSwitcher.playerSwitch += ChangeStats;
        playerSwitcher.playerSwitch += RefreshAnimator;
        facingRight = true;
        animator = GetComponentInChildren<Animator>();
        playerBody = GetComponent<Rigidbody>();
        playerStats = FindObjectOfType<PlayerStats>();
    }

	void OnCollisionEnter(Collision playerCollider) 
    { //FIXME: Needs to be fixed with head
		if (!isGrounded)
		{
			isGrounded = true;
            remainingJumps = maxJumpes;
            if (animator.gameObject.activeSelf)
            {
            animator.ResetTrigger("isJumping");
            animator.SetBool("isLanding", false);
		}

		}
	}
	private void Update()
    {
        PlayerJump();  
        RunningAnimation();  
        moveChecker();    
    }

    public void RefreshAnimator()
    {
        animator = GetComponentInChildren<Animator>();
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
        PlayerDrag();
        ResetDrag();
    }

    void ResetDrag() {
        if (isGrounded && playerBody.drag > 0)
        {
            playerBody.drag = 0;
        }
    }

    private void InputTranslator()
    {
        if (moveEnabled)
        {
            translation = CrossPlatformInputManager.GetAxis("Horizontal") * speed;
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
      if (animator.gameObject.activeSelf) 
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
    }

    private void MovementHandeler()
    {
        playerBody.velocity = new Vector3(translation, playerBody.velocity.y, playerBody.velocity.z);
    }

    //This makes Saske Float
    private void PlayerDrag()
    {
        if (playerBody.velocity.y < -.7f && CrossPlatformInputManager.GetButton("Jump") && playerStats.getCharIndex == 1) // if player is controlling saske and holding jump
        {
            //TODO: Float animation
            playerBody.drag = floatTime; // increasing drag to make player float
            isGrounded = false;
            if (animator.gameObject.activeSelf)
            {
                animator.SetBool("isLanding", true);
            }
        }
        else if (playerBody.velocity.y < -.7f) // if player is not controlling saske OR is not holding space set the drag back to 0
        {
            playerBody.drag = 0;
            isGrounded = false;
            if (animator.gameObject.activeSelf)
            {
                animator.SetBool("isLanding", true);
            }
        }
    }

    private void PlayerJump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded && moveEnabled)
        {   
            remainingJumps --;
            if (animator.gameObject.activeSelf) {
            animator.SetTrigger("isJumping");
            }
            playerBody.velocity = Vector3.up * jumpPower;
			isGrounded = false;
        }

        else if (CrossPlatformInputManager.GetButtonDown("Jump") && remainingJumps >= 1 && moveEnabled && !isGrounded) // double jump
        {
            //TODO: Fart animation
            remainingJumps --;
            if (animator.gameObject.activeSelf) {
            animator.SetTrigger("isJumping");
            }
            playerBody.velocity = Vector3.up * doubleJumpPower;
			isGrounded = false;
        }
    }

    private void LayerHandler() 
    {
        if (animator.gameObject.activeSelf) {
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
    private void ChangeStats()
    {
		switch (playerStats.getCharIndex)
		{
			case 0:
				maxJumpes = 2;
				remainingJumps =2;
				break;
			case 1:
				maxJumpes = 1;
				remainingJumps =1;
				break;
			default:
			break;
		}
    }
}
