using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTextTrigger : MonoBehaviour {

	GameObject textBalloon;

	private void Start() {
		textBalloon = transform.Find("TextBalloon").gameObject;
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player")
		{
			textBalloon.SetActive(true);
		}
	}
}
