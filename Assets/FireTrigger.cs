using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrigger : MonoBehaviour {

	FireBall fireBall;
	KamikazePigeon  kamikazePigeon;
	PlayerStats playerStats;

	// Use this for initialization
	void Start () {
		fireBall = FindObjectOfType<FireBall>();
		playerStats = FindObjectOfType<PlayerStats>();
		kamikazePigeon = FindObjectOfType<KamikazePigeon>();
	}
	
	private void TriggerFire(){
		switch (playerStats.getCharIndex)
		{
			case 0: 
			fireBall.FireCurrentWeapon();
			break;
			case 1:
			kamikazePigeon.FireCurrentWeapon();
			break;
			default:
			Debug.LogError("No player?");
			break;
		}

	}

}
