using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	[HideInInspector]public GameObject heal, shield, resist, boost;

	public GameObject player;
	public GameObject core1;
	public Vector3 spawnValues;

	public float spawnWait;
	public long gold;

	public float sectorSize;

	public float difficulty;
	public void setDifficulty(){
		difficulty=Vector2.Distance (new Vector2 (0, 0), new Vector2 (xSector, ySector));
	}
	public int xSector = 5;
	public int ySector = 10;
	  
	//HUD references
	public Text hullText;
	public Text pixText;
	public Text sectorText;
	public Text shieldText;
	public Text waveText;
	public Text boostText;

	public GameObject sectorBounds;

	public int score;

	//public GUIText weapon_txt;
	public string[] weapons;

	public int waveLimit = 0;
	public int waveCurrent = 0;

	public void init(){

		player.GetComponent<SkillSystem> ().init ();
		player.GetComponent<PlayerController> ().init ();
		calcHazardCount ();
		spawnWait = Random.Range (2f, 5f);
		score = 0;



		setBounds();
		StartCoroutine(Wave1());





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
		float chance = 0.3f;
		hazardCount = (int)tempdiff; 
		while (Mathf.Sqrt(tempdiff) >= 1){
			//Debug.Log(hazardCount);
			if(Random.value > chance||hazardCount>10){
				enemyModifier++;
				chance = chance * 1.5f;
				tempdiff = tempdiff - Mathf.Sqrt(tempdiff);
				hazardCount = (int) tempdiff;
			}
			else{break;}

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
		sectorSize = Random.Range (sectorSizeMin+difficulty, sectorSizeMax+difficulty); //randomize sector size
		sectorSize = Mathf.Round(sectorSize * 100f) / 100f; // truncate decimal precision

	}

	IEnumerator Wave1(){

		if (xSector == 0 && ySector == 0) {
			sectorClear();
		} else {
			yield return new WaitForSeconds (2f);
			for (int i=0; i<hazardCount; i++) {
				float randX = Random.Range (-sectorSize / 2, sectorSize / 2);
				float randY = Random.Range (-sectorSize / 2, sectorSize / 2);

				while (randX < player.transform.position.x+5&&randX>player.transform.position.x-5 && randY < player.transform.position.y+5&& randY>player.transform.position.y-5&& player!=null) {
					randX = Random.Range (-sectorSize, sectorSize);
					randY = Random.Range (-sectorSize, sectorSize);
				}
				Vector3 spawnPosition = new Vector3 (randX, randY, spawnValues.z);
				GameObject coreSpawn = (GameObject)Instantiate (core1, spawnPosition, Quaternion.Euler (0, 0, 0));//GameCamera.transform.rotation);

				coreSpawn.GetComponent<CollisionObject>().sectorDifficulty = this.difficulty + this.enemyModifier;
				coreSpawn.GetComponent<CoreScript>().sectorDifficulty = this.difficulty + this.enemyModifier;

				yield return new WaitForSeconds (spawnWait);
				spawnWait = Random.Range (1f, 5f);
			}
		}
}
	public List<Coord> coords = new List<Coord>();
	public void sectorClear(){
		Coord thisCoord = new Coord (xSector,ySector);
			coords.Add (thisCoord);

		openBorders();
	}


	public void openBorders(){
		sectorBounds.GetComponent<BoxColSetSectorSize> ().liftBorder ();
		player.GetComponent<PlayerController> ().sectorClear = true;

	}
	private bool moving = false;
	public void moveNorth(){
		if (!moving) {
			ySector++;
			animColorFade.SetTrigger ("fade");
			Invoke ("newSector", 3f);
			moving = true;
		}
	}
	public void moveSouth(){
		if (!moving) {
			ySector--;
			animColorFade.SetTrigger ("fade");
			Invoke ("newSector", 3f);
			moving = true;
		}
	}
	public void moveEast(){
		if (!moving) {
			xSector++;
			animColorFade.SetTrigger ("fade");
			Invoke ("newSector", 3f);
			moving = true;
		}
	}
	public void moveWest(){
		if (!moving) {
			xSector--;
			animColorFade.SetTrigger ("fade");
			Invoke ("newSector", 3f);
			moving = true;
		}
	}

	public Animator animColorFade; 
	private bool visitedSector = false;
	public void newSector(){
		setDifficulty ();
		generateSectorSize();
		setBounds ();
		waveCurrent = 0;
		visitedSector = false;
//		Debug.Log (coords.Count);
		//bool fal = false;
		clear = false;

		foreach (Coord coord in coords) {
//			Debug.Log(coord.x+","+coord.y);
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
//		foreach (GameObject junk in GameObject.FindGameObjectsWithTag("Bullet")) {
//			Destroy(junk);
//		}
		ObjectPool.pool.FlushObjects();
		foreach (GameObject junk in GameObject.FindGameObjectsWithTag("EnemyBullet")) {
			Destroy(junk);
		}
		foreach (GameObject junk in GameObject.FindGameObjectsWithTag("Strike")) {
			Destroy(junk);
		}
//		foreach (GameObject junk in GameObject.FindGameObjectsWithTag("Interactible")) {
//			Destroy(junk);
//		}
		player.transform.position = new Vector3 (0, 0, 0);
		moving = false;
		UpdateHUD ();

	}
	//HUD Functions

	private bool clear = false; 
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

			if(!clear){
			sectorClear();
			clear = true;
			}
		}
	}
	public void setHealth (float curHP, float hpMax){
		hullText.text = "Hull: " + (int)curHP + "/" + (int)hpMax;
	}
	public void setShield (float curShield, float shieldMax){
		shieldText.text = "Shield: " + (int)curShield + "/" + (int)shieldMax;
	}
	public void setBoost (float curBoost){
		if (curBoost <= 0) {
			boostText.text = "";
		} else {
			//Debug.Log(curBoost);
			boostText.text = "Boost Time: " + (int)curBoost;
		}

	}

	public void addWave(){
		waveCurrent += 1;
		UpdateHUD ();
	}

	public void addScore(int new_score){
		score += new_score;
		UpdateHUD ();
	}

	public void restart (){
		//sectorText.text = "Now reinitializing";
		Invoke ("destroyAndCreate", 5);
	}
	public void destroyAndCreate(){
		animColorFade.SetTrigger ("fade");
		Destroy (GameObject.FindWithTag ("MainMenu"));
		Destroy (gameObject);
		//Application.LoadLevel(0);
		SceneManager.LoadScene(0);
	}


}


[System.Serializable]
public class Coord
{
	public float x, y;
	public Coord()
	{
	}
	public Coord(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}





