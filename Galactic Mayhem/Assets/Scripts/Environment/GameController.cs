using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public GameObject player;
	public GameObject core1;
	public Vector3 spawnValues;

	public float spawnWait;
	public long gold;

	public float sectorSize;
	//public Camera GameCamera;


	public float difficulty;
	public void setDifficulty(){
		difficulty=Vector2.Distance (new Vector2 (0, 0), new Vector2 (xSector, ySector));
	}
	public int xSector = 5;
	public int ySector = 10;
	  
	//HUD references
	public Text hullText;
	//public Text scoreText;
	public Text sectorText;
	public Text shieldText;
	public Text waveText;

	public GameObject sectorBounds;

	public int score;

	//public GUIText weapon_txt;
	public string[] weapons;

	public int waveLimit = 0;
	public int waveCurrent = 0;

	public void init(){

		player.GetComponent<PlayerController> ().init ();
		calcHazardCount ();
		spawnWait = Random.Range (5, 10);
		score = 0;

		StartCoroutine(Wave1());

		setBounds();
		Coord thisCoord = new Coord ();
		thisCoord.x = xSector;
		thisCoord.y = ySector;
		coords.Add (thisCoord);






		UpdateHUD (); //always do this at the end
	}

	public Boundary boundary;
	public void setBounds(){
		boundary.xMin = - (sectorSize / 2);
		boundary.xMax = (sectorSize / 2);
		boundary.yMin = - (sectorSize / 2);
		boundary.yMax = (sectorSize / 2);
		sectorBounds.GetComponent<BoxColSetSectorSize> ().setBounds (sectorSize);
	}


	public int hazardCount = 1;
	public float enemyModifier = 0;
	public void calcHazardCount(){

		float tempdiff = Random.Range(difficulty*0.75f,difficulty*1.25f) ;
		float chance = 0.2f;
		while (Mathf.Sqrt(tempdiff) >= 1){
			if(Random.value > chance){
				enemyModifier++;
				chance = chance * 1.5f;
				tempdiff = tempdiff - Mathf.Sqrt(tempdiff);
				hazardCount = (int) tempdiff;
			}else {break;}

		}

		hazardCount = (int)tempdiff + 1;
		waveLimit = hazardCount;
	}

	// Use this when initializing
	public float getSectorSize(){
		return sectorSize;
	}

	void Awake(){
		generateSectorSize();
		setDifficulty ();
	
	}
	public float sectorSizeMin;
	public float sectorSizeMax;

	private void generateSectorSize(){
		sectorSize = Random.Range (sectorSizeMin, sectorSizeMax); //randomize sector size
		sectorSize = Mathf.Round(sectorSize * 100f) / 100f; // truncate decimal precision

	}

	IEnumerator Wave1(){
		if (xSector == 0 && ySector == 0) {
			sectorClear();
		} else {
			for (int i=0; i<hazardCount; i++) {
				float randX = Random.Range (-sectorSize / 2, sectorSize / 2);
				float randY = Random.Range (-sectorSize / 2, sectorSize / 2);
				while (randX < player.transform.position.x+5&&randX>player.transform.position.x-5 && randY < player.transform.position.y+5&& randY>player.transform.position.y-5) {
					randX = Random.Range (-sectorSize, sectorSize);
					randY = Random.Range (-sectorSize, sectorSize);
				}
				Vector3 spawnPosition = new Vector3 (randX, randY, spawnValues.z);
				Instantiate (core1, spawnPosition, Quaternion.Euler (0, 0, 0));//GameCamera.transform.rotation);

				yield return new WaitForSeconds (spawnWait);
			}
		}
}

	public List<Coord> coords = new List<Coord>();
	public void sectorClear(){
		Coord thisCoord = new Coord ();
		thisCoord.x = xSector;
		thisCoord.y = ySector;
//		bool add = true;
//		foreach (Coord coord in coords) {
//			if (coord.x == xSector && coord.y == ySector) {
//				add = false;
//				Debug.Log("already visited");
//			}
//		}
//		if(!add)
			coords.Add (thisCoord);

		openBorders();
	}


	public void openBorders(){
		sectorBounds.GetComponent<BoxColSetSectorSize> ().liftBorder ();
		player.GetComponent<PlayerController> ().sectorClear = true;

	}
	public void moveNorth(){
		ySector++;
		newSector ();
	}
	public void moveSouth(){
		ySector--;
		newSector ();
	}
	public void moveEast(){
		xSector++;
		newSector ();
	}
	public void moveWest(){
		xSector--;
		newSector ();
	}
	private bool visitedSector = false;
	public void newSector(){
		setDifficulty ();
		generateSectorSize();
		setBounds ();
		waveCurrent = 0;
		visitedSector = false;
//		Debug.Log (xSector + "," + ySector);
		foreach (Coord coord in coords) {
			if (coord.x == xSector && coord.y == ySector) {

				visitedSector = true;
				sectorBounds.GetComponent<BoxColSetSectorSize> ().liftBorder ();
				player.GetComponent<PlayerController>().sectorClear = true;
				waveLimit = 0;

			}
		}


		if (!visitedSector) {

			player.GetComponent<PlayerController> ().sectorClear = false;
			sectorBounds.GetComponent<BoxColSetSectorSize> ().setBorder ();
			calcHazardCount ();
			spawnWait = Random.Range (5, 10);
			visitedSector = false;
			StartCoroutine (Wave1 ());

		}
		foreach (GameObject junk in GameObject.FindGameObjectsWithTag("Bullet")) {
			Destroy(junk);
		}
		foreach (GameObject junk in GameObject.FindGameObjectsWithTag("EnemyBullet")) {
			Destroy(junk);
		}
		foreach (GameObject junk in GameObject.FindGameObjectsWithTag("Strike")) {
			Destroy(junk);
		}
		foreach (GameObject junk in GameObject.FindGameObjectsWithTag("Interactible")) {
			Destroy(junk);
		}
//		foreach (GameObject junk in sectorJunk) {
//			Destroy(junk);
//		}
		//sectorJunk.Clear ();
		player.transform.position = new Vector3 (0, 0, 0);

		//sectorBounds.GetComponent<BoxColSetSectorSize> ().setBorder ();
		UpdateHUD ();

	}
	//HUD Functions
	void UpdateHUD(){
//		if (score_txt != null) {
//			score_txt.text = "Score: " + score;
//		}
//
			sectorText.text = "Sector: ("+xSector+","+ySector+")";
			
			waveText.text = "Enemies: "+waveCurrent+"/"+waveLimit+"";
//		} 
//		Debug.Log (waveCurrent);
		if (waveCurrent == waveLimit) {
		
			sectorClear();
		}
	}
	public void setHealth (float curHP, float hpMax){
		hullText.text = "Hull: " + (int)curHP + "/" + (int)hpMax;
	}
	public void setShield (float curShield, float shieldMax){
		shieldText.text = "Shield: " + (int)curShield + "/" + (int)shieldMax;
	}

	public void addWave(){
		waveCurrent += 1;
		UpdateHUD ();
	}

	public void addScore(int new_score){
		score += new_score;
		UpdateHUD ();
	}
}

[System.Serializable]
public class Coord
{
	public float x, y;
}





