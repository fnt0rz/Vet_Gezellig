using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {

	[SerializeField] GameObject explosion;

	public delegate void ObjectHit(GameObject hit, float damage);
	public event ObjectHit enemyHit;
	public bool isEnemy;
	public Vector3 firedFrom;
	public float maxRange;
	public float hitDamage;
	PlayerStats playerStats;

	private void Start() {
		playerStats = FindObjectOfType<PlayerStats>();
	}

	private void Update() {
		DistanceController();
	}

    private void DistanceController()
    {
        if (Vector3.Distance(firedFrom,transform.position)> maxRange)
		{
			Destroy(gameObject);
		}
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
			case "LevelTrigger":
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
			enemyHit(hit, hitDamage);
		}
    }
}
