using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireControler : MonoBehaviour {

	[SerializeField]
	private float speed;
	private bool running = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(BirdScripts.instance != null) {
			if(BirdScripts.instance.isAlive) {
				if(gameObject.name == "Fire Top") {
					if(running) {
						MoveFireDown();
					}
					else {
						MoveFireUp();
					}
				}
				if(gameObject.name == "Fire Bottom") {
					if(running) {
						MoveFireUp();
					}
					else {
						MoveFireDown();
					}
				}
			}
			else {
				Destroy (gameObject);
			}
		}
	}

	// Tạo di chuyển của chướng ngại vật
	void MoveFireDown() {
		Vector3 temp = transform.position;
		temp.y -= speed * Time.deltaTime;
		transform.position = temp;
	}
	void MoveFireUp() {
		Vector3 temp = transform.position;
		temp.y += speed * Time.deltaTime;
		transform.position = temp;
	}
	
	// Xử lý va chạm để các chướng ngại vật di chuyển lên xuống
	void OnTriggerEnter2D (Collider2D target) {

		if(gameObject.name == "Fire Top") {
			if(target.tag == "MoveFireDown") {
				running = true;
			}
			if(target.tag == "MoveFireUp") {
				running = false;
			}
		}
		if(gameObject.name == "Fire Bottom") {
			if(target.tag == "MoveFireDown") {
				running = false;
			}
			if(target.tag == "MoveFireUp") {
				running = true;
			}
		}
		
	}
}
