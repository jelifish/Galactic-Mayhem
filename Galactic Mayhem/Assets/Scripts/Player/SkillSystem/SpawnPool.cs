using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPool : MonoBehaviour {
	public static SpawnPool pool = null;
	private List<GameObject> objs;
	private PlayerController pc;

	private GameObject spawnContainer;
	public BoxColSetSectorSize sectorClass;

	public void checkSpawn(GameObject spawn){
	
	}
	public void placeSpawn(GameObject spawn){
	//places the spawn in a location near to the player

	}
	public void addSpawn(GameObject spawn){//add a spawn into the list
	//replace old spawn with new one

	}
	public void removeSpawn (SkillType type){//MaterialType, ControlType, GuardType, AssaultType, AuraType
	//remove all spawns with the typename from the list
	}
	public void neutralizeSpawn(GameObject spawn){
	
	}
	public void spawnChecker(){
	//checks if any spawns becomes too far away from the player
		bool spawnMoved = false;

		foreach (GameObject obj1 in objs) {
			foreach(GameObject obj2 in objs){
				if(obj1 == obj2){
					Debug.Log("these two are the same! :)");
				}
				else if(Vector2.Distance(obj1.transform.position, obj2.transform.position) <3){
					placeSpawn(obj1);
					spawnMoved = true;
					Debug.Log("overlapping spawns! :O");
				}



			}
		}
		if (spawnMoved) {
			spawnChecker();
		}
	}
	public GameObject GetPooledObject(){//do i need this
		GameObject temp = null;
		return null;
	}


	void Awake(){
		pc = this.gameObject.GetComponent<PlayerController> ();
	}
	void Start () {

		InvokeRepeating("spawnChecker", 0.1f, 0.1f); //checks spawn every 10th of a sec. might get changed to a half of a sec


//		spawnContainer = new GameObject("spawnContainer");
//		containerObject = new GameObject("ObjectPool");
//		objs = new List<GameObject> ();
//		activeObj = new Queue<GameObject> ();
//		for (int i = 0; i<= poolSize; i++) {
//			GameObject obj = (GameObject)Instantiate(bolt);
//			obj.transform.parent = container.transform;
//			obj.SetActive(false);
//			objs.Add(obj);
//			activeObj.Enqueue (obj);
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
