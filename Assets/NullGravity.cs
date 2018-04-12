using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullGravity : MonoBehaviour {

	void OnCollisionEnter(Collision other) {
		if(other.transform.tag == "Player")	
		{
			var playerBody = FindObjectOfType<PlayerMovement>();
			playerBody.GetComponent<Rigidbody>().useGravity = false;
			playerBody.transform.localRotation = Quaternion.Euler(180f,transform.localRotation.y,transform.localRotation.z);
			playerBody.transform.parent = gameObject.transform;

		}
	}

/* 	private void OnCollisionExit(Collision other) {
		if (other.transform.tag == "Player")
		{
			other.transform.parent = null;

		}
	} */
		
}

