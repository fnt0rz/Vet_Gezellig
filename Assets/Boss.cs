using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	[SerializeField] GameObject projectileToFire;
	[SerializeField] float forwardForce = 500f;
	[SerializeField] float fireRate = 3;
	[SerializeField] float nextFire = 0f;
	List<GameObject> gunsToFire = new List<GameObject>();
	[SerializeField] int stage = 1;
	[SerializeField] Vector3 movementVector = new Vector3(0f,20f,0f);
	[Range(0,1)] [SerializeField] float movementFactor;
	[SerializeField] float movementPeriod = 10f;
	Vector3 startingPos;
	Vector3 fireDirection;

	// Use this for initialization
	void Start () {
		startingPos = transform.position;
	}
	
	private void Update() {
		fireRateHandler();
	}

	void FixedUpdate() {
        if (stage == 2)
            MoveSideWays();
    }

    private void MoveSideWays()
    {
        if (movementPeriod <= Mathf.Epsilon) { return; }
        float cycles = Time.time / movementPeriod;
        const float tau = Mathf.PI * 2f;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSinWave / 2f + 0.5f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }

    private void fireRateHandler()
    {
		if (Time.time > nextFire)
        {
            SelectFireSequence();
            fireProjectile();
            nextFire = Time.time + fireRate;
        }
    }

    private void SelectFireSequence()
    {
		switch (stage)
		{
			case 1:
				Stage1();
				break;
			case 2:
				Stage2();
				break;
			default:
				Debug.LogError("Stage does not exist");
				break;
		}
    }

    private void Stage2()
    {
		fireDirection = -transform.up;
		fireRate = 2f;
        gunsToFire.Clear();
		gunsToFire.Add(GameObject.Find("LeftGun"));
        gunsToFire.Add(GameObject.Find("RightGun"));
		gunsToFire.Add(GameObject.Find("LowMidGun"));

    }

    private void Stage1()
    {
		fireDirection = transform.right;
        var fireSequence = Random.Range(1, 3);
        gunsToFire.Clear();
        switch (fireSequence)
        {
            case 1:

                gunsToFire.Add(GameObject.Find("MidGun"));
                gunsToFire.Add(GameObject.Find("BotGun"));
                break;
            case 2:

                gunsToFire.Add(GameObject.Find("MidGun"));
                gunsToFire.Add(GameObject.Find("TopGun"));
                break;
            default:
                Debug.LogError("Stage: " + stage + "wrong guns, check the range");
                break;
        }
    }

    private void fireProjectile()

    {
		foreach (GameObject gun in gunsToFire)
		{
			var projectile = Instantiate(projectileToFire, gun.transform.position, Quaternion.Euler(0f,90f,0f));
			var projectileBody = projectile.GetComponent<Rigidbody>();
			SetProjectileStats(projectile);
			projectileBody.AddForce(fireDirection * forwardForce);
		}

    }

    private void SetProjectileStats(GameObject projectile)
    {
        projectile.GetComponent<CollisionHandler>().isEnemy = true;
        projectile.GetComponent<CollisionHandler>().firedFrom = transform.position;
        projectile.GetComponent<CollisionHandler>().maxRange = 15f;
    }


}
