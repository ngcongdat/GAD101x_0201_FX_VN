using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCollector : MonoBehaviour {

	private GameObject[] backgrounds;
	private GameObject[] grounds;
	private float speed = 2.5f;
	private float lastBGX;
	private float lastGrounds;

	// Use this for initialization
	void Awake() {

		// Khởi tạo tập hợp các GameObjects của grounds và backgrounds
		backgrounds = GameObject.FindGameObjectsWithTag ("Background");
		grounds = GameObject.FindGameObjectsWithTag ("Ground");

		// Gắn vị trị x của phần tử đầu tiên cho biến lastBGX và lastGrounds
		lastBGX = backgrounds [0].transform.position.x;
		lastGrounds = grounds [0].transform.position.x;

		// Tìm và gắn vị trí x phần tử cuối cùng của backgrounds cho lastBGX
		for(int i = 1; i < backgrounds.Length; i++) {
			if(lastBGX < backgrounds[i].transform.position.x) {
				lastBGX = backgrounds [i].transform.position.x;
			}
		}

		// Tìm và gắn vị trí x phần tử cuối cùng của backgrounds cho lastGrounds
		for(int i = 1; i < grounds.Length; i++) {
			if(lastGrounds < grounds[i].transform.position.x) {
				lastGrounds = grounds [i].transform.position.x;
			}
		}
	}
	
	// Xử lý va chạm của Backgrounds và Grounds sau đó quay về lastBGX và lastGrounds
	void OnTriggerEnter2D (Collider2D target) {
		if(target.tag == "Background") {
			Vector3 temp = target.transform.position;
			float width = ((BoxCollider2D)target).size.x;
			temp.x = lastBGX + width;
			target.transform.position = temp;
			lastBGX = temp.x;
		} else if(target.tag == "Ground") {
			Vector3 temp = target.transform.position;
			float width = ((BoxCollider2D)target).size.x;
			temp.x = lastGrounds + width;
			target.transform.position = temp;
			lastGrounds = temp.x;
		}
	}
}























































