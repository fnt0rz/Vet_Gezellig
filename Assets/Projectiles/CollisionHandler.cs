using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {

	[SerializeField] GameObject explosion;

	public delegate void ObjectHit(GameObject hit, float damage);
	public event ObjectHit enemyHit;
	public bool isEnemy;

	PlayerStats playerStats;

	private void Start() {
		playerStats = FindObjectOfType<PlayerStats>();
	}


	private void OnTriggerEnter(Collider other) {
		switch (other.gameObject.tag)
        {
			case "Enemy":
            	EnemyHit(other);
				break;
        	case "Player":
            	PlayerHit(other);
				break;
			default:
				EnvoirementHit(other);
				break;
        }
    }

    private void EnvoirementHit(Collider other)
    {
		var collisionChecker = other.GetComponent<CollisionHandler>();
		if (collisionChecker == null)
		{
			var vfx = Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
			Destroy(vfx, 0.5f);
		}

	
    }

    private void PlayerHit(Collider other)
    {
		if(isEnemy) {
        var vfx = Instantiate(explosion, transform.position, Quaternion.identity);
		playerStats.PlayerHit(1f);
        Destroy(gameObject);
        Destroy(vfx, 0.5f);

		}
    }

    private void EnemyHit(Collider other)
    {
		if (!isEnemy)
		{
			var hit = other.gameObject;
			var vfx = Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
			Destroy(vfx, 0.5f);
			enemyHit(hit, 3f);
		}
    }
}
