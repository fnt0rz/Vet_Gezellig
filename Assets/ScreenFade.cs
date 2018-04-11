using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFade : MonoBehaviour {
	PlayerStats playerStats;
	public bool screenBlack = false;
	CanvasGroup canvasGroup;

	// Use this for initialization
	void Start () {
		playerStats = FindObjectOfType<PlayerStats>();
		canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 1;
		FadeIn();
		playerStats.playerDeath += FadeScreen;
	}

    private void FadeIn()
    {
        StartCoroutine (StartFadeIn());
    }

    private IEnumerator StartFadeIn()
    {
        while (canvasGroup.alpha > 0)
		{
			canvasGroup.alpha -= Time.deltaTime/2f; 
			yield return null;
		}
		canvasGroup.interactable = false;
		yield break;

    }

    private void FadeScreen(float Lives){
		StartCoroutine (StartFadeOut());
	}

    public IEnumerator StartFadeOut()
    {
 
		while (canvasGroup.alpha < 1)
		{
			canvasGroup.alpha += Time.deltaTime/1.5f; 
			yield return null;
		}
		screenBlack = true;		
		playerStats.playerDeath -= FadeScreen;
		yield break;
    }	
}
