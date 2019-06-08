using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdScripts : MonoBehaviour {

	public static BirdScripts instance;
	[SerializeField]
	private Rigidbody2D myRigidbody;
	[SerializeField]
	private Animator anim;
	private float forwardSpeed = 3f;
	private float bounceSpeed = 4f;
	private bool didFlap;
	public bool isAlive;
	private Button flapButton;
	[SerializeField]
	private AudioSource audioSource;
	[SerializeField]
	private AudioClip flapClip, pointClip, diedClip;

	private GameObject spawnerEnemy;

	public int score;
	// Use this for initialization
	void Awake () {
		if(instance == null) {
			instance = this;
		}
		isAlive = true;
		score = 0;
		flapButton = GameObject.FindGameObjectWithTag ("FlapButton").GetComponent<Button> ();
		flapButton.onClick.AddListener (() => FlapTheBird ());
		SetCameraX ();
		spawnerEnemy = GameObject.Find("Enemy Collector");

	}
	
	void FixedUpdate () {
		if(isAlive) {
			Vector3 temp = transform.position;
			temp.x += forwardSpeed * Time.deltaTime;
			transform.position = temp;

			// Hiệu ứng click cho bird bật lên
			if(didFlap) {
				didFlap = false;
				myRigidbody.velocity = new Vector2(0, bounceSpeed);
				audioSource.PlayOneShot (flapClip);
				anim.SetTrigger("Flapping");
			}
		}

		// Hiệu ứng xoay bird khi bay
		if(myRigidbody.velocity.y > 0) {
			float angel = 0;
			angel = Mathf.Lerp (0, 90, myRigidbody.velocity.y / 7);
			transform.rotation = Quaternion.Euler(0, 0, angel);
		} else if (myRigidbody.velocity.y == 0) {
			transform.rotation = Quaternion.Euler(0, 0, 0);
		} else if(myRigidbody.velocity.y < 0) {
			float angel = 0;
			angel = Mathf.Lerp (0, -90, -myRigidbody.velocity.y / 7);
			transform.rotation = Quaternion.Euler(0, 0, angel);
		}
	}

	void SetCameraX () {
		CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x) - 1f;
	}

	public float GetPositionX () {
		return transform.position.x;
	}

	public void FlapTheBird () {
		didFlap = true;
	}


	// Xử lý va chạm để bird chết
	void OnCollisionEnter2D (Collision2D target) {
		if (target.gameObject.tag == "Ground" || target.gameObject.tag == "Pipe" || target.gameObject.tag == "Fire" || target.gameObject.tag == "Enemy") {
			if(isAlive) {
				isAlive = false;
				anim.SetTrigger ("Died");
				audioSource.PlayOneShot (diedClip);
				GamePlayerController.instance.PlayerDiedShowScore(score);
				Destroy(spawnerEnemy);
			}
		}
	}

// Xử lý va chạm khi bird ghi điểm
	void OnTriggerEnter2D (Collider2D target) {
		if (target.tag == "PipeHolder") {
				score++;
				GamePlayerController.instance.BestScore(score);
				audioSource.PlayOneShot (pointClip);
		}
	}
}
