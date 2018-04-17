using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour {

	[SerializeField] float attackRadius = 10f;
	[SerializeField] GameObject projectileToFire;
	[SerializeField] float forwardForce = 50f;
	GameObject player;
	[SerializeField] float fireRate = 0.5f;
	[SerializeField] float nextFire = 0f;
	[SerializeField] GameObject aimFor;
    [SerializeField] float destroyTime = 1.5f;

    private void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		

	}

	private void Update() {
		DistanceChecker();
	}

    private void DistanceChecker()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= attackRadius && Time.time > nextFire)
		{

			print("Starting to fire!" + gameObject.name);
			FireProjectile();
			nextFire = Time.time + fireRate;
		}
	}

    private void FireProjectile()
    {
		transform.LookAt(aimFor.transform);
       	var projectile = Instantiate(projectileToFire,transform.position,transform.rotation);
        var projectileBody = projectile.GetComponent<Rigidbody>();
		projectile.tag = "Projectile";
		projectileBody.AddForce(transform.forward * forwardForce);
        Destroy(projectile, destroyTime);
    }

    private void OnDrawGizmos() {
		Gizmos.DrawWireSphere(transform.position, attackRadius);
	}
}
