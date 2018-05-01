using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	PlayerStats playerStats;
	ScreenFade screenFade;
	PlayerSwitcher playerSwitcher;
	PlayerMovement playerMovement;
	FireBall playerFire;

	private void OnEnable() {
		playerStats = FindObjectOfType<PlayerStats>();
		playerStats.playerDeath += RespawnHandler;		
	}

	private void Start() {
		playerSwitcher = FindObjectOfType<PlayerSwitcher>();
		playerStats.Respawn();
		LoadPlayer();
	}

    private void LoadPlayer()
    {
        GameObject characterToLoad = playerSwitcher.getCharacter;
		characterToLoad.SetActive(true);
		playerSwitcher.FirstLoad();
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
		var currentScene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentScene);

    }
	private void OnDisable() {
		playerStats.playerDeath -= RespawnHandler;
	}
}
		// game overtext
		// load main menu
		// fade screen

