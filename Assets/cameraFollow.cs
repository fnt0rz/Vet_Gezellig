using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {

	GameObject player;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		print(player);

	}
	private void LateUpdate() {
	transform.position = player.transform.position;		
	}
}
