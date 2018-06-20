using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour {

	PlayerStats playerStats;

	private void Start() {
		playerStats = FindObjectOfType<PlayerStats>();
        this.tag = "LevelTrigger";
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Player"))
		{
			playerStats.HealUp();
			Destroy(this.gameObject);
		}
	}
}
