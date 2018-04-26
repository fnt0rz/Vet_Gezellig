using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {

	[SerializeField] float maxHealthPoints = 3f;
	[SerializeField] float playerLives = 1f;
	float currentHealthPoints = 3f;
	bool playerIsAlive = true;
	int characterIndex; 

	public delegate void PlayerDeath(float remainingLives);
	public event PlayerDeath playerDeath;

	public bool isAlive {
		get {
			return playerIsAlive;
		}
	}
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

	public float GetCurrentHealthPoints
	{
		get {
			return currentHealthPoints;
		 }

	}

		public int getCharIndex
	{
		get {
			return characterIndex;
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
			currentHealthPoints -= damage;
			KillPlayer();
		}
		else
		{
			currentHealthPoints -= damage;
		}
	}


	public void Respawn() {
		currentHealthPoints = maxHealthPoints;
		playerIsAlive = true;
	}

	public void ChangeCharacterIndex(int characterList) {
		characterIndex++;
		if (characterIndex == characterList)
			{
				characterIndex = 0;
			}
	}

    private void KillPlayer()
    {
		// play deathanimation --> Animator
		playerIsAlive = false;
		playerDeath(playerLives);
		playerLives--;	

    }

}
