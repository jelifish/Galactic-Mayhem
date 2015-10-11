using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject blueSquare1;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public long gold;


	public float sectorSize;
	public Camera GameCamera;
	// Use this for initialization
	public float getSectorSize(){
		return sectorSize;
	}
	void Awake(){
		generateSectorSize();
	
	
	}
	private void generateSectorSize(){
		sectorSize = Random.Range (20.0F, 50.0F); //randomize sector size
		sectorSize = Mathf.Round(sectorSize * 100f) / 100f; // truncate decimal precision

	}
	void Start () {
		StartCoroutine(Wave1());

	}
	IEnumerator Wave1(){
		for(int i=0; i<hazardCount; i++){

			Vector3 spawnPosition = new Vector3(Random.Range(-sectorSize/2, sectorSize/2),spawnValues.y,Random.Range(-sectorSize/2, sectorSize/2));
			Instantiate(blueSquare1, spawnPosition, Quaternion.Euler(90,0,0));//GameCamera.transform.rotation);

			yield return new WaitForSeconds(spawnWait);
		}
	
	//bool respawns;
	//IEnumerator CheckForEnemies(){
		//if (respawns == null)
		//	respawns = GameObject.FindGameObjectsWithTag ("Enemy");
	//	}


//
//		for(int i=0; i<hazardCount; i++){
//			
//			Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x),spawnValues.y,spawnValues.z);
//			Instantiate(blueSquare1, spawnPosition, GameCamera.transform.rotation);
//			
//			yield return new WaitForSeconds(spawnWait);
//		}
}

}
