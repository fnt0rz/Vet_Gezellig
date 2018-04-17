using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullGravity : MonoBehaviour {

	bool attached = false;

	private void Start() {
		
	}

	void OnCollisionEnter(Collision other) {
		if(other.transform.tag == "Player" && !attached)	
		{
			var nullZone = new GameObject();
			nullZone.transform.parent = gameObject.transform;
			var playerBody = FindObjectOfType<PlayerMovement>();
			playerBody.GetComponent<Rigidbody>().useGravity = false;
			playerBody.transform.parent = nullZone.transform;
			playerBody.transform.localRotation = Quaternion.Euler(180f,playerBody.transform.localRotation.y,playerBody.transform.localRotation.z);

			attached = true;	

		}
	}

/* 	private void OnCollisionExit(Collision other) {
		if (other.transform.tag == "Player")
		{
			other.transform.parent = null;

		}
	} 
		 */
}

