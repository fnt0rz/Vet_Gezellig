using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {
	PlayerStats	playerStats;

	private void Start() {
		playerStats = FindObjectOfType<PlayerStats>();
	}



	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player")
		{
			playerStats.PlayerHit(playerStats.GetCurrentHealthPoints);
		}
	}
}
