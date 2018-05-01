using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerSwitcher : MonoBehaviour {

	[SerializeField] GameObject currentCharacter;
	CameraFollow cameraFollow;
	PlayerMovement playerMovement;
	PlayerStats playerStats;
	public List<GameObject> characterList;

	public delegate void PlayerSwitch();
	public event PlayerSwitch playerSwitch;

	private void Awake() {
		playerStats = FindObjectOfType<PlayerStats>();
		FillList();
        CharacterSelection();
	}

	private void Update() {
		Switchplayer();
	}

	private void Start()
    {
        cameraFollow = FindObjectOfType<CameraFollow>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void FillList()
    {
		characterList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Character"));
		characterList.Sort ((x, y) => x.name.CompareTo (y.name));
    }

    public GameObject getCharacter {
		get {
			return currentCharacter;
		}
	}

	public void FirstLoad () 
	{
		playerSwitch();
	}

    private void CharacterSelection()
    {
		foreach (GameObject gameObject in characterList)
		{
			gameObject.SetActive(false);
		}

		if (currentCharacter == null)
		{
			currentCharacter = characterList[playerStats.getCharIndex];
		}
		else
		{
			currentCharacter.SetActive(true);
		}
	}

    private void Switchplayer()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire3") && playerMovement.isGrounded && playerMovement.moveEnabled)
		{
			//animation
			ChangeCharacter();
			playerSwitch();
			cameraFollow.RefreshTarget();
		}
    }

    private void ChangeCharacter()
    {
		currentCharacter.SetActive(false);
		playerStats.ChangeCharacterIndex(characterList.Count);
		currentCharacter = characterList[playerStats.getCharIndex];
		currentCharacter.SetActive(true);
    }

	
}
