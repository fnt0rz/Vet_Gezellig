using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArena : MonoBehaviour {

	[SerializeField] GameObject WallLeft;
	[SerializeField] GameObject WallRight;
	[SerializeField] float cameraMovementSpeed = 3f;
	[SerializeField] float waitTime = 3f;
	PlayerMovement playerMovement;
	PlayerFire playerFire;
	bool triggerEnabled = true;
	bool moveCamera = false;
	Transform cameraPos;
	Vector3 newCameraPos;
	Boss boss;
	Camera m_camera;
	// Use this for initialization

	private void Start() {
		playerMovement = FindObjectOfType<PlayerMovement>();
		playerFire = FindObjectOfType<PlayerFire>();
		m_camera = Camera.main;
		cameraPos = m_camera.transform;
		boss = FindObjectOfType<Boss>();

	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player") && triggerEnabled)
		{
			PlayerControl();
			ToggleWalls();
			MoveCamera();

			triggerEnabled = false;
		}
	}

    private void MoveCamera()
    {
		moveCamera = true;
		newCameraPos = new Vector3(-11f, cameraPos.localPosition.y, cameraPos.localPosition.z);
		StartCoroutine(BossIntro(waitTime));
    }

    private IEnumerator BossIntro(float time)
    {
		print("Start waiting for " + time);
        yield return new WaitForSeconds(time);
		print("Done");
		newCameraPos = new Vector3(0f, cameraPos.localPosition.y, cameraPos.localPosition.z);
		StartCoroutine(StartFight(2f));

	}

    private IEnumerator StartFight(float time)
    {
		yield return new WaitForSeconds(time);
		PlayerControl();
		boss.fireEnabled = true;
		moveCamera = false;
    }

    private void LateUpdate() {
		if (moveCamera)
		{
			m_camera.transform.localPosition = Vector3.Lerp(cameraPos.localPosition, newCameraPos, Time.deltaTime * cameraMovementSpeed);
		}
	}


    private void ToggleWalls()
    {
        WallLeft.SetActive(!WallLeft.activeSelf);
		WallRight.SetActive(!WallRight.activeSelf);
    }

    public void PlayerControl()
    {
        Movement();
        playerFire.fireEnabled = !playerFire.fireEnabled;

    }

    private void Movement()
    {
        playerMovement.moveEnabled = !playerMovement.moveEnabled;
        var rb = playerMovement.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
		playerMovement.translation = 0;
    }
}
