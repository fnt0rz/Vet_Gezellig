using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerFire : MonoBehaviour {
	
	[SerializeField] GameObject weaponFire;
	[SerializeField] int forwardForce = 50;
	[SerializeField] Transform fireLocation;
	Animator animator;
	PlayerMovement playerMovement;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (CrossPlatformInputManager.GetButtonDown("Fire1") && GetComponent<PlayerMovement>().isGrounded)
        {
            animator.SetTrigger("isFiring");
		}

    }

    private void FireCurrentWeapon()
    {
		var projectile = Instantiate(weaponFire, fireLocation.position, transform.rotation);
        var projectileBody = projectile.GetComponent<Rigidbody>();
		projectileBody.AddForce(transform.forward * forwardForce);
        Destroy(projectile, 2f);
    }
}
