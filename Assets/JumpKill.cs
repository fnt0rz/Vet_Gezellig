using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpKill : MonoBehaviour {

	Enemy enemy;

	private void Start() {
		enemy = GetComponentInParent<Enemy>();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player"))
		{
			//FIXME: Add effects
			Destroy(enemy.gameObject);
		}
	}

}
