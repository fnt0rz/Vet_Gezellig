using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    PlayerStats playerStats;
    CheckPointDisplay checkPointDisplay;
    bool triggered = false;
    

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        checkPointDisplay = FindObjectOfType<CheckPointDisplay>();
    }

	private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !triggered)
        {
            playerStats.setRespawnLocation(this.transform.position);
            var renderer = GetComponent<Renderer>();
            renderer.material.SetColor("_Color", Color.green);
            checkPointDisplay.DisplayCheckPointText();
            triggered = true;
        }
	}


}
