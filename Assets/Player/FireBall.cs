﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

	[SerializeField] GameObject weaponFire;
	[SerializeField] Transform fireLocation;
	[SerializeField] float fireRate = 3f;
	[SerializeField] float nextFire = 0f;
	[SerializeField] float attackRange = 10f;
	[SerializeField] float fire2Damage = 3f;
	[SerializeField] int forwardForce = 400;
	Animator animator;
	PlayerStats playerStats;
	PlayerSwitcher playerSwitcher;

	public void Start() {
		animator = GetComponentInChildren<Animator>();
		playerStats = FindObjectOfType<PlayerStats>();
		playerSwitcher = FindObjectOfType<PlayerSwitcher>();
		playerSwitcher.playerSwitch += RefreshAnimator;
		fireLocation = transform.Find("WeaponLocation");
	}
	
	public void RefreshAnimator() {
		animator = GetComponentInChildren<Animator>();
	}

	public void FireFireball()
    {	
		if (Time.time > nextFire)
		{
			if (animator.gameObject.activeSelf)
            {
				animator.SetTrigger("fireFireball");
				nextFire = Time.time + fireRate;
			}
		}
    }

	public float fireCooldown{
		get {
			return nextFire;
		}
	}

    public void FireCurrentWeapon() 
    {
		var projectile = Instantiate(weaponFire, fireLocation.position, transform.rotation);
        var projectileBody = projectile.GetComponent<Rigidbody>();
		SetProjectileStats(projectile);
		projectileBody.AddForce(transform.forward * forwardForce);

    }

	    private void SetProjectileStats(GameObject projectile)
    {
        projectile.GetComponent<CollisionHandler>().isEnemy = false;
        projectile.GetComponent<CollisionHandler>().firedFrom = transform.position;
        projectile.GetComponent<CollisionHandler>().maxRange = attackRange;
		projectile.GetComponent<CollisionHandler>().hitDamage = fire2Damage;
    }

}