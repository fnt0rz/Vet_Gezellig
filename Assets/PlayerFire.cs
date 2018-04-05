using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerFire : MonoBehaviour {
	
	[SerializeField] ParticleSystem weaponFire;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (CrossPlatformInputManager.GetButtonDown("Fire1"))
		{
			weaponFire.Play();
		}	

	}
}
