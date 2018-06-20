using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	[SerializeField] GameObject projectileToFire;
	[SerializeField] float forwardForce = 500f;
	[SerializeField] float fireRate = 3;
	[SerializeField] float nextFire = 0f;
    [SerializeField] float maxRange = 20f;
	List<GameObject> gunsToFire = new List<GameObject>();
	[SerializeField] int stage = 1;
    [SerializeField] Transform bossPos0;
    [SerializeField] Transform bossPos1;
	[SerializeField] float movementSpeed = 3f;
    [SerializeField] float destinationSpeed = 2f;
    Transform destination;
	Vector3 fireDirection;
    public bool fireEnabled = false;
	Enemy enemy;
	Animation stageAnim;
	bool stage2Played = false;
    bool stage3Played = false;
    BossArena bossArena;
    [SerializeField] float maxBossHp = 20f;

	// Use this for initialization
	void Start () {
		enemy = GetComponent<Enemy>();
        bossArena = FindObjectOfType<BossArena>();
	}
	
	private void Update() {
		if (enemy.enemyHealth <= (maxBossHp * 0.6) && !stage2Played)
		{
			StartStage2();
		}
        else if (enemy.enemyHealth <= (maxBossHp * 0.2) && !stage3Played)
        {
            TriggerStage3();
        }
		fireRateHandler();
	}

    private void StartStage2()
    {
        stage2Played = true;
        fireEnabled = false;
        bossArena.PlayerControl();
        stageAnim = GetComponent<Animation>();
		stageAnim.Play("Stage2");
        
    }

	private void SetStage2() 
	{
		stage = 2;
        fireEnabled = true;
		bossArena.PlayerControl();
        StartCoroutine(ChangeDestination(destinationSpeed));
	}

    IEnumerator ChangeDestination(float destinationSpeed)
    {
        while (true)
        {
            if (destination == bossPos1)
            {
                destination = bossPos0;
            }
            else
            {
                destination = bossPos1;
            }
            yield return new WaitForSeconds(destinationSpeed);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(bossPos0.position,transform.localScale);
        Gizmos.DrawWireCube(bossPos1.position,transform.localScale);
    }

    void FixedUpdate(){
        if (stage == 2) 
        {
            MoveSideWays(); 
        }
    }

    private void TriggerStage3()
    {
        StopCoroutine(ChangeDestination(0f));
        stage3Played = true;
        fireEnabled = false;
        bossArena.PlayerControl();
        destination = bossPos0;
        StartCoroutine(WaitForPos(3f));
    }

    IEnumerator WaitForPos(float time)
    {
        yield return new WaitForSeconds(time);
        stageAnim.Play("Stage3");
    }

    private void StartStage3() {
        stage = 3;
        fireEnabled = true;
        fireRate = fireRate / 2;
        bossArena.PlayerControl();
    }

    private void MoveSideWays()
    {
        transform.position = Vector3.Lerp(transform.position, destination.position, Time.fixedDeltaTime * movementSpeed);
    }

    private void fireRateHandler()
    {
		if (Time.time > nextFire && fireEnabled)
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
			case 3:
				Stage1();
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
        projectile.GetComponent<CollisionHandler>().maxRange = maxRange;
    }


}
