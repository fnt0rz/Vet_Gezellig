using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerSwitcher : MonoBehaviour {

	[SerializeField] GameObject currentCharacter;
	[SerializeField] GameObject newCharacter;
	GameObject oldChar;
	CameraFollow cameraFollow;
	PlayerMovement playerMovement;
	PlayerFire playerFire;

	private void Awake() {
		int StatsCount = FindObjectsOfType<PlayerSwitcher>().Length;
		if (StatsCount > 1)
		{
			Destroy(gameObject);
		}
		else {
			DontDestroyOnLoad(gameObject);
		}

     	currentCharacter.tag = "Player";   
	}

	private void Update() {
		Switchplayer();
	}

	private void Start() {
		cameraFollow = FindObjectOfType<CameraFollow>();
		playerMovement = FindObjectOfType<PlayerMovement>();
		playerFire = FindObjectOfType<PlayerFire>();
	}

    private void Switchplayer()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire3") && playerMovement.isGrounded && playerMovement.moveEnabled)
		{
			disableCurrent();
			enableNew();
			cameraFollow.RefreshTarget();
			playerMovement.RefreshAnimator();
			playerFire.RefreshAnimator();
		}
    }

    private void enableNew()
    {
        newCharacter.transform.position = oldChar.transform.position;
		newCharacter.SetActive(true);
		currentCharacter = newCharacter;
		newCharacter = oldChar;
		currentCharacter.tag = "Player";
    }

    private void disableCurrent()
    {
        oldChar = currentCharacter;
		currentCharacter.SetActive(false);	
		currentCharacter.tag ="Untagged";
    }
}
