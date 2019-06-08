using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScripts : MonoBehaviour {

    [SerializeField]
    private float speed;
	// Update is called once per frame
	void Update () {
		if(BirdScripts.instance != null) {
			MoveEnemy();
        }
	}

	// Tạo di chuyển của enemy
    void MoveEnemy() {
		Vector3 temp = transform.position;
        temp.x -= speed * Time.deltaTime;
		transform.position = temp;
	}

	// Xử lý va chạm để hủy các enemy đi qua màn hình
    void OnTriggerEnter2D (Collider2D target) {
        if(target.tag == "DestroyEnemy") {
            Destroy(gameObject);
        }
	
	}
}
