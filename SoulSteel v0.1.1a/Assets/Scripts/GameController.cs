﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject enemy1;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public long gold;

	// Use this for initialization
	void Start () {
		StartCoroutine(Wave1());

	}
	IEnumerator Wave1(){
		for(int i=0; i<hazardCount; i++){

			Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x),spawnValues.y,spawnValues.z);
			Instantiate(enemy1, spawnPosition, Quaternion.identity);

			yield return new WaitForSeconds(spawnWait);
		}
	
	//bool respawns;
	//IEnumerator CheckForEnemies(){
		//if (respawns == null)
		//	respawns = GameObject.FindGameObjectsWithTag ("Enemy");
	//	}



		for(int i=0; i<hazardCount; i++){
			
			Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x),spawnValues.y,spawnValues.z);
			Instantiate(enemy1, spawnPosition, Quaternion.identity);
			
			yield return new WaitForSeconds(spawnWait);
		}
}

}
