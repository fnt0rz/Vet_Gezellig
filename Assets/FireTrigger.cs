using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrigger : MonoBehaviour {

	FireBall fireBall;

	// Use this for initialization
	void Start () {
		fireBall = FindObjectOfType<FireBall>();
	}
	
	private void TriggerFire(){
		fireBall.FireCurrentWeapon();
	}

}
