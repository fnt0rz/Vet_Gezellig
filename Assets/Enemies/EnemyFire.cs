using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour {

	[SerializeField] float attackRadius = 10f;
	[SerializeField] GameObject projectileToFire;
	[SerializeField] float forwardForce = 50f;
	[SerializeField] float fireRate = 0.5f;
	[SerializeField] float nextFire = 0f;
	[SerializeField] GameObject aimFor;
    [SerializeField] float destroyTime = 1.5f;
	GameObject player;
	PlayerStats playerStats;

    private void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		playerStats = FindObjectOfType<PlayerStats>();
	
	}

	private void Update() {
		FireChecker();
	}

    private void FireChecker()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= attackRadius && playerStats.isAlive)
		{
			fireRateHandler();
			
		}
	}

    private void fireRateHandler()
    {
		if (Time.time > nextFire) {
        fireProjectile();
        nextFire = Time.time + fireRate;
		}
    }

    private void fireProjectile()
    {

        transform.LookAt(aimFor.transform);
        var projectile = Instantiate(projectileToFire, transform.position, transform.rotation);
        var projectileBody = projectile.GetComponent<Rigidbody>();
        projectile.GetComponent<CollisionHandler>().isEnemy = true;
        projectileBody.AddForce(transform.forward * forwardForce);
        Destroy(projectile, destroyTime);
    }

    private void OnDrawGizmos() {
		Gizmos.DrawWireSphere(transform.position, attackRadius);
	}
}
