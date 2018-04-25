using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrigger : MonoBehaviour {

	PlayerFire playerFire;

	// Use this for initialization
	void Start () {
		playerFire = FindObjectOfType<PlayerFire>();
	}
	
	private void TriggerFire(){
		playerFire.FireCurrentWeapon();
	}

}
