using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour {

	GameObject textBalloon;

	private void Start() {
		textBalloon = transform.Find("TextBalloon").gameObject;

	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player")
        {
            StartCoroutine(StartFadeIn());
        }
    }

    private IEnumerator StartFadeIn()
    {
        var shader = textBalloon.GetComponent<MeshRenderer>();
        var material = shader.material;
        Color color = material.GetColor("_Color");
		while (color.a < 1)
		{
			color.a += Time.deltaTime / 3f;
        	material.SetColor("_Color", color);
			yield return null;
		}

    }
}
