using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class PlayerHealthBar : MonoBehaviour {

    RawImage healthBarRawImage;
    PlayerStats player;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        healthBarRawImage = GetComponent<RawImage>();
        SetLives();
    }

    private void SetLives()
    {
        var livesUI = GetComponentInChildren<Text>();
        livesUI.text = "LIVES: " + player.currentLives.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        float xValue = -(player.healthAsPercentage / 2f) - 0.5f;
        healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
    }
}
