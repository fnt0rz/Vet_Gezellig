using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {

	[SerializeField] GameObject explosion;

	public delegate void EnemyHit(GameObject hit, float damage);
	public event EnemyHit enemyHit;


	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Enemy")
		{
			var hit = other.gameObject;
			var vfx = Instantiate(explosion,transform.position,Quaternion.identity);
			Destroy(gameObject);
			Destroy(vfx, 0.5f);
			enemyHit(hit,3f);
		}
		else if(other.gameObject.tag != "Player" && other.gameObject.tag != "Projectile")
		{
			var vfx = Instantiate(explosion,transform.position,Quaternion.identity);
			Destroy(gameObject);
			Destroy(vfx, 0.5f);
		}
	}


}
