using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerFire : MonoBehaviour {
	
	[SerializeField] GameObject weaponFire;
	[SerializeField] int forwardForce = 50;
	[SerializeField] Transform fireLocation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (CrossPlatformInputManager.GetButtonDown("Fire1"))
		{
			var fireball = Instantiate(weaponFire,fireLocation.position,transform.localRotation);
			var rigidbody = fireball.GetComponent<Rigidbody>();
			print(rigidbody);
			rigidbody.AddForce(transform.forward * forwardForce);
		}	

	}
}
