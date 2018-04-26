using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerFire : MonoBehaviour {
	
	[SerializeField] GameObject weaponFire;
	[SerializeField] int forwardForce = 50;
	[SerializeField] Transform fireLocation;
	[SerializeField] float fireRate = 3f;
	[SerializeField] float nextFire = 0f;
	[SerializeField] float attackRange = 10f;
	[SerializeField] float fire2Damage = 3f;
	Animator animator;
	PlayerMovement playerMovement;
	PlayerStats playerStats;
	public bool fireEnabled = true;


	//TODO: Move animations to animatorscript
	public void InitialLoad () {
		animator = GetComponentInChildren<Animator>();
		playerStats = FindObjectOfType<PlayerStats>();
	}
	

	public void RefreshAnimator() {
		animator = GetComponentInChildren<Animator>();
	}

	void Update () {
		FireController();
    }

    private void FireController()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire2") && GetComponent<PlayerMovement>().isGrounded && fireEnabled && playerStats.isAlive)
            FireRateHandeler();
    }

    private void FireRateHandeler()
    {	
		if (Time.time > nextFire)
		{
			if (animator.gameObject.activeSelf)
            {
				animator.SetTrigger("isFiring");
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
