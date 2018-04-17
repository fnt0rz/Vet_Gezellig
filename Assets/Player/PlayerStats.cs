using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {

	[SerializeField] float maxHealthPoints = 3f;
	[SerializeField] float playerLives = 1f;
	float currentHealthPoints = 3f;
	public bool playerIsAlive = true;

	public delegate void PlayerDeath(float remainingLives);
	public event PlayerDeath playerDeath;

	public float currentLives 
	{
		get {
			return playerLives;
			}
	}

	public float healthAsPercentage
	{
		get {
			return currentHealthPoints / (float)maxHealthPoints;
		}
	}

	private void Awake() {
		int StatsCount = FindObjectsOfType<PlayerStats>().Length;
		if (StatsCount > 1)
		{
			Destroy(gameObject);
		}
		else {
			DontDestroyOnLoad(gameObject);
		}
	}
	

	public void PlayerHit(float damage){
		if (currentHealthPoints <= damage)
		{
			KillPlayer();
		}
		else
		{
			currentHealthPoints -= damage;
		}
	}


    private void KillPlayer()
    {
		// play deathanimation --> Animator
		playerIsAlive = false;
		playerDeath(playerLives);
		playerLives--;	
		currentHealthPoints = maxHealthPoints;
    }

}
