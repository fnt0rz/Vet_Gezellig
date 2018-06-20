using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float enemyHealth = 3f;
	CollisionHandler[] collisionHandlers;
	bool subscribed;
    [SerializeField] List<String> projectiles = new List<String>();

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
		print("Destroyed by ");
        foreach (CollisionHandler collisionhandler in collisionHandlers)
        {
            collisionhandler.enemyHit -= HitHandler;
        }
		Destroy(gameObject);
    }

    private void GotHit(float damage)
    {
		//hit effect
		print("got hit for " + damage);
        enemyHealth -= damage;
    }


    // Update is called once per frame
    void Update()
    {
        collisionHandlers = FindObjectsOfType<CollisionHandler>();
        foreach (CollisionHandler collisionHandler in collisionHandlers)
        {
            if (!projectiles.Contains(collisionHandler.gameObject.name) && !collisionHandler.isEnemy)
            {
                projectiles.Add(collisionHandler.gameObject.name);
                collisionHandler.enemyHit += HitHandler;
            }
        }
    }
}
