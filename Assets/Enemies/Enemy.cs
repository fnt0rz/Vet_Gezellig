using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField] float enemyHealth = 3f;
	CollisionHandler collisionHandler;
	bool subscribed;

	private void Start() {
		gameObject.tag = "Enemy";
	}

    private void HitHandler (GameObject hit, float damage)
    {
        if (hit == this.gameObject && enemyHealth > damage)
		{
			GotHit(damage);
		}
		else if(hit == this.gameObject && enemyHealth <= damage)
		{
			GotKilled();
		}
    }

    private void GotKilled()
    {
		// kill effect
		collisionHandler.enemyHit -= HitHandler;
		Destroy(gameObject);
    }

    private void GotHit(float damage)
    {
		//hit effect
        enemyHealth -= damage;
    }

    // Update is called once per frame
    void Update () {
		collisionHandler = FindObjectOfType<CollisionHandler>();
		if (collisionHandler != null && !subscribed && !collisionHandler.isEnemy)
		{	
			subscribed = true;
			collisionHandler.enemyHit += HitHandler;
		}
		else if(collisionHandler == null && subscribed)
		{
			subscribed = false;
		}
	}
}
