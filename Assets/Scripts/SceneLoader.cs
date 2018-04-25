﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	PlayerStats playerStats;
	ScreenFade screenFade;
	PlayerSwitcher playerSwitcher;
	PlayerMovement playerMovement;
	PlayerFire playerFire;

	private void Start() {
		playerStats = FindObjectOfType<PlayerStats>();
		playerSwitcher = FindObjectOfType<PlayerSwitcher>();
		playerMovement = FindObjectOfType<PlayerMovement>();
		playerFire = FindObjectOfType<PlayerFire>();
		playerStats.Respawn();
		playerStats.playerDeath += RespawnHandler;
		LoadPlayer();

	}

    private void LoadPlayer()
    {
        GameObject characterToLoad = playerSwitcher.getCharacter;
		characterToLoad.SetActive(true);
		playerMovement.InitialLoad();
		playerFire.InitialLoad();
    }

    private void RespawnHandler(float lives) {
		
		if (lives > 0)
		{
			StartCoroutine(RespawnPlayer());
		}

	}

    private IEnumerator RespawnPlayer()
    {
        	screenFade = FindObjectOfType<ScreenFade>();
			while (screenFade.screenBlack == false)
			{
				yield return new WaitForSeconds(1f);
			}
			playerStats.playerDeath -= RespawnHandler;
			var currentScene = SceneManager.GetActiveScene().buildIndex;
			SceneManager.LoadScene(currentScene);

    }
}
		// game overtext
		// load main menu
		// fade screen

