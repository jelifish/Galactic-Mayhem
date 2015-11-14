using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject player;
	public GameObject blueSquare1;
	public GameObject core1;
	public Vector3 spawnValues;

	public float spawnWait;
	public long gold;

	public float sectorSize;
	public Camera GameCamera;


	public float difficulty;
	public float getDifficulty(){
		return Vector2.Distance (new Vector2 (0, 0), new Vector2(xSector, ySector));
	}
	public int xSector = 5;
	public int ySector = 10;
	  
	//HUD references
	public Text hullText;
	//public Text scoreText;
	public Text sectorText;
	public Text shieldText;
	public Text waveText;


	public int score;

	//public GUIText weapon_txt;
	public string[] weapons;

	public int waveLimit = 0;
	public int waveCurrent = 0;


	void Start () {

	}

	public void init(){
		player.GetComponent<PlayerController> ().init ();
		calcHazardCount ();
		spawnWait = Random.Range (5, 10);
		score = 0;

		waveLimit = hazardCount;
		StartCoroutine(Wave1());

		UpdateHUD (); //always do this at the end
	}


	public int hazardCount = 1;
	public float enemyModifier = 0;
	public void calcHazardCount(){

		float tempdiff = difficulty;
		float chance = 0.2f;
		while (Mathf.Sqrt(tempdiff) >= 1){
			if(Random.value > chance){
				enemyModifier++;
				chance = chance * 1.5f;
				tempdiff = tempdiff - Mathf.Sqrt(tempdiff);
				hazardCount = (int) tempdiff;
				Debug.Log(tempdiff);
			}else {break;}

		}

		hazardCount = (int)tempdiff + 1;
	}

	// Use this for initialization
	public float getSectorSize(){
		return sectorSize;
	}

	void Awake(){
		generateSectorSize();
		difficulty = getDifficulty ();
		//Debug.Log (getDifficulty());
	
	}
	public float sectorSizeMin;
	public float sectorSizeMax;

	private void generateSectorSize(){
		sectorSize = Random.Range (sectorSizeMin, sectorSizeMax); //randomize sector size
		sectorSize = Mathf.Round(sectorSize * 100f) / 100f; // truncate decimal precision

	}

	IEnumerator Wave1(){
		for(int i=0; i<hazardCount; i++){

			Vector3 spawnPosition = new Vector3(Random.Range(-sectorSize/2, sectorSize/2),Random.Range(-sectorSize/2, sectorSize/2),spawnValues.z);
			Instantiate(core1, spawnPosition, Quaternion.Euler(0,0,0));//GameCamera.transform.rotation);

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
//		if (score_txt != null) {
//			score_txt.text = "Score: " + score;
//		}
//
			sectorText.text = "Sector: ("+xSector+","+ySector+")";

			waveText.text = "Wave: "+waveCurrent+"/"+waveLimit+"";
//		} 
//		Debug.Log (waveCurrent);
	}
	public void setHealth (float curHP, float hpMax){
		hullText.text = "Hull: " + (int)curHP + "/" + (int)hpMax;
	}
	public void setShield (float curShield, float shieldMax){
		shieldText.text = "Shield: " + (int)curShield + "/" + (int)shieldMax;
	}

	public void addWave(){
		Debug.Log("enemykilled");
		waveCurrent += 1;
		UpdateHUD ();
	}

	public void addScore(int new_score){
		score += new_score;
		UpdateHUD ();
	}


}
