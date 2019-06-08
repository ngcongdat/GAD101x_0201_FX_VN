using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public static float offsetX;

	// Update is called once per frame
	void Update () {
		if(BirdScripts.instance != null) {
			if(BirdScripts.instance.isAlive) {
				MoveTheCamera ();
			}
		}
	}

	// Tạo di chuyển của camera theo bird
	void MoveTheCamera () {
		Vector3 temp = transform.position;
		temp.x = BirdScripts.instance.GetPositionX() + offsetX;
		transform.position = temp;
	}
}
