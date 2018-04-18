using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour {

	[SerializeField] float attackRange = 10f;
	[SerializeField] GameObject projectileToFire;
	[SerializeField] float forwardForce = 50f;
	[SerializeField] float fireRate = 0.5f;
	[SerializeField] float nextFire = 0f;
	[SerializeField] GameObject aimFor;
	GameObject player;
	PlayerStats playerStats;

    private void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		playerStats = FindObjectOfType<PlayerStats>();
	
	}

	private void Update() {
		FireController();
	}


    private void FireController()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= attackRange && playerStats.isAlive)
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
        SetProjectileStats(projectile);
        projectileBody.AddForce(transform.forward * forwardForce);


    }

    private void SetProjectileStats(GameObject projectile)
    {
        projectile.GetComponent<CollisionHandler>().isEnemy = true;
        projectile.GetComponent<CollisionHandler>().firedFrom = transform.position;
        projectile.GetComponent<CollisionHandler>().maxRange = attackRange;
    }

    private void OnDrawGizmos() {
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}
}
