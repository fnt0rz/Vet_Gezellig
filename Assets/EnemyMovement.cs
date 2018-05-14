using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	Enemy enemy;
	[SerializeField] float jumpHeight = 4f;

	void Start () {
		enemy = GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
