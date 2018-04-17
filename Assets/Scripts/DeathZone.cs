using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {
	PlayerStats	player;

	private void Start() {
		player = FindObjectOfType<PlayerStats>();
	}



	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player")
		{
			player.PlayerHit(3f);
		}
	}
}
