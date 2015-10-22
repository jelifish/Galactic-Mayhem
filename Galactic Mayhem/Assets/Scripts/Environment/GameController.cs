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

	//HUD Variables
	public GUIText score_txt = null;
	public int score;
	public GUIText health_txt;
	public int health;
	public GUIText shield_txt;
	public int shield;
	public GUIText weapon_txt;
	public string[] weapons;

	private PlayerController pc;

	void Start () {
		score = 0;
		UpdateHUD ();
		StartCoroutine(Wave1());
	}

	// Use this for initialization
	public float getSectorSize(){
		return sectorSize;
	}

	void Awake(){
		generateSectorSize();
	
	
	}

	private void generateSectorSize(){
		sectorSize = Random.Range (30.0F, 40.0F); //randomize sector size
		sectorSize = Mathf.Round(sectorSize * 100f) / 100f; // truncate decimal precision

	}

	IEnumerator Wave1(){
		for(int i=0; i<hazardCount; i++){

			Vector3 spawnPosition = new Vector3(Random.Range(-sectorSize/2, sectorSize/2),spawnValues.y,Random.Range(-sectorSize/2, sectorSize/2));
			Instantiate(blueSquare1, spawnPosition, Quaternion.Euler(90,0,0));//GameCamera.transform.rotation);

			yield return new WaitForSeconds(spawnWait);
		}
	
	//bool respawns;
	//IEnumerator CheckForEnemies(){ // should be double check
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

	//HUD Functions
	void UpdateHUD(){
		if (score_txt != null) {
			score_txt.text = "Score: " + score;
		}
	}

	public void addScore(int new_score){
		score += new_score;
		UpdateHUD ();
	}

}
