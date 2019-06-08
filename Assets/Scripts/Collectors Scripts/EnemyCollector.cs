using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollector : MonoBehaviour {

	public static EnemyCollector instance;

	[SerializeField]
	private GameObject enemyBirds;
	private float enemyMin = -0.8f;
	private float enemyMax = 1.2f;
	
	bool spawner;

	float time;
	// Use this for initialization
	void Awake () {
		if(instance == null) {
			instance = this;
		}
		spawner = false;
	}

	void Update() {
		// Tạo eneny sau 2 giây
		if(spawner) {
			time += Time.deltaTime;
			if(time >= 2) {
				SpawnerEnemy();
				time = 0;
			}
		}
		
	}

	public void Spawner() {
		spawner = true;
	}

	public void SetPause() {
		spawner = false;
	}
	
	// Tạo enemy
	void SpawnerEnemy() {
		if(spawner){
		Vector2 temp = new Vector2 (transform.position.x, Random.Range(enemyMin, enemyMax));
		Instantiate(enemyBirds, temp, Quaternion.identity);
		}
	}
}
