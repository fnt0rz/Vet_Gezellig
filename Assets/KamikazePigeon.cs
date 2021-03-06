﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazePigeon : MonoBehaviour {

	[SerializeField] GameObject weaponFire;
	[SerializeField] Transform fireLocation;
	[SerializeField] float fireRate = 3f;
	[SerializeField] float nextFire = 0f;
	[SerializeField] float attackRange = 10f;
	[SerializeField] float fire2Damage = 3f;
	[SerializeField] int forwardForce = 400;
    int pigeonCounter = 0;
    Animator animator;
	PlayerSwitcher playerSwitcher;

	private void OnEnable() {
		playerSwitcher = FindObjectOfType<PlayerSwitcher>();
		playerSwitcher.playerSwitch += RefreshAnimator;		
	}
	
	public void Start() {
		animator = GetComponentInChildren<Animator>();
		fireLocation = transform.Find("WeaponLocation");
	}
	
	public void RefreshAnimator() {
		animator = GetComponentInChildren<Animator>();
	}

		public void FirePigeon()
    {	
		if (Time.time > nextFire)
		{
			if (animator.gameObject.activeSelf)
            {
				animator.SetTrigger("firePigeon");
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
        projectile.name = "AirProjectile " + pigeonCounter;
        ++pigeonCounter;
        SetProjectileStats(projectile);
		projectileBody.AddForce(transform.up * forwardForce);

    }

	    private void SetProjectileStats(GameObject projectile)
    {
        projectile.GetComponent<CollisionHandler>().isEnemy = false;
        projectile.GetComponent<CollisionHandler>().firedFrom = transform.position;
        projectile.GetComponent<CollisionHandler>().maxRange = attackRange;
		projectile.GetComponent<CollisionHandler>().hitDamage = fire2Damage;
    }

	private void OnDisable() {
		playerSwitcher.playerSwitch -= RefreshAnimator;	
	}
}
