using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameController : MonoBehaviour {

	public GameObject homeBase;
	public GameObject player;
	public int xSector, ySector;
	public float xpos, ypos;
	public GameObject enemy;
	public GameObject pixeroid;

	//list of coords that have been generated
	public List<Coord> coords = new List<Coord>();

	public float gameDifficulty;
	// Use this for initialization
	void Awake () {
		calcAllSectors ();
		gameDifficulty = 0;
	}

	void Start(){
		StartCoroutine (slowUpdate ());
		StartCoroutine (wave ());

	}
	
	IEnumerator slowUpdate(){
		while (true) {
			yield return new WaitForSeconds (.1f);
			xSector = (int)((player.transform.position.x - (player.transform.position.x % gridSize)) / gridSize);
			ySector = (int)((player.transform.position.y - (player.transform.position.y % gridSize)) / gridSize);
		
			if (player.transform.position.x < 0)
				xSector -= 1;
			if (player.transform.position.y < 0)
				ySector -= 1;
		
			xpos = player.transform.position.x;
			ypos = player.transform.position.y;
		}
	}
	public float gridSize = 1;
	void Update(){

	}

	//true if sector coord is in list, false is not in list
	public bool isOccupied(int x, int y){
		foreach (Coord coord in coords) {
			//Debug.Log(coord.x+","+coord.y);
			if (coord.x == x && coord.y == y) {
				return true;
			}

		}
		return false;

	}


	public Coord playerCoord(){
		int x = (int)((player.transform.position.x - (player.transform.position.x % gridSize)) / gridSize);
		int y = (int)((player.transform.position.y - (player.transform.position.y % gridSize)) / gridSize);

		if (x < 0) x+=1;
		if (y < 0) y+=1;

		return new Coord (x, y);
	}
	//calculate what sectors need to be generated
	//finds the closest untouched sectors and creates them
	public void calcAllSectors(){
		//get current sector
		for (int i=-10; i<10; i++) {
			for(int j=-10; j<10; j++)
			{
				if(!isOccupied(xSector+i, ySector+j))
				{
					calcSector(xSector+i,ySector+j);
				}
				
			}
		}

	}

	//figures out what goes into each sector depending on the difficulty given a sector location x,y
	public void calcSector(int x, int y){
		if (x > -3 && y > -3 && x < 3 && y < 3) {
			//Debug.Log ("hit");//return;
		} else {
			if (Random.value > .9f) {
				createThing(enemy, new Vector3 ((x * gridSize) + Random.Range (0, gridSize), (y * gridSize) + Random.Range (0, gridSize)));
			}

			if(Random.value > .98f){
				//GameObject thing = (GameObject)Instantiate(pixeroid, new Vector3 ((x * gridSize) + Random.Range (0, gridSize), (y * gridSize) + Random.Range (0, gridSize)), Quaternion.identity);
				//GameObject thing = createThing(pixeroid,new Vector3 ((x * gridSize) + Random.Range (0, gridSize), (y * gridSize) + Random.Range (0, gridSize)));
				GameObject thing = Instantiate(pixeroid, new Vector3 ((x * gridSize) + Random.Range (0, gridSize), (y * gridSize) + Random.Range (0, gridSize)), Quaternion.identity) as GameObject;
				pixeroids.Add(thing);
			}

		}
		coords.Add (new Coord (x, y));
	}

	public List<GameObject> pixeroids = new List<GameObject>();

	public GameObject findNearestPixeroid(float x, float y){
	
		return pixeroids [1];
	}

	//create the gameobject at the specific location
	public GameObject createThing(GameObject obj, Vector3 loc){

		GameObject thing = Instantiate(obj, loc, Quaternion.identity) as GameObject;
		return thing;
	}


	IEnumerator wave(){
		yield return new WaitForSeconds (2f);
		while (true) {
			calcAllSectors ();
			yield return new WaitForSeconds (2f);
		}


	}

	 
}

[System.Serializable]
public class Coord
{
	public int x, y;
	//public int lbx, lby, rbx, rby, ltx, lty, rtx, rty;
	public Coord(int x, int y)
	{
		this.x = x;
		this.y = y;
		//this.lbx = x * 10
		//this.lby = (x - 1) * 10f

	}
}
