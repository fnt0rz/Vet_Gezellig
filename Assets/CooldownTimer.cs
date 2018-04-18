using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownTimer : MonoBehaviour {
	PlayerFire playerFire;
	Text coolDownUi;

	private void Start() {
		playerFire = FindObjectOfType<PlayerFire>();
		coolDownUi = GetComponent<Text>();
	}
	void Update () {
		var rawCooldown = playerFire.fireCooldown - Time.time;
		double coolDown = System.Math.Round(rawCooldown,1);
		if (coolDown > Mathf.Epsilon)
		{
			coolDownUi = GetComponent<Text>();
			coolDownUi.text = coolDown.ToString();
		}
		else {
			coolDownUi.text = "";
		}
	}
}
