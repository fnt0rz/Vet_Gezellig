using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerFire: MonoBehaviour {
	
	PlayerStats playerStats;
	FireBall fireBall;
	public bool fireEnabled = true;


	//TODO: Move animations to animatorscript
	public void Start () {
		playerStats = FindObjectOfType<PlayerStats>();
		fireBall = FindObjectOfType<FireBall>();
	}
	
	void Update () {
		FireController();
    }

    private void FireController()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire2") && GetComponent<PlayerMovement>().isGrounded && fireEnabled && playerStats.isAlive)
			switch (playerStats.getCharIndex)
			{
				case 0:
				fireBall.FireFireball();
				break;
				default:
				Debug.LogError("Suppose to fire but no character?");
				break;
			}
    }
}
