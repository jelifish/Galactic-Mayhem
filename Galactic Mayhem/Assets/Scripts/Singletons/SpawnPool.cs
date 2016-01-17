using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPool : MonoBehaviour {
	public static SpawnPool pool = null;
	public List<GameObject> objs;
	private PlayerController pc;

	private GameObject spawnContainer;
	public BoxColSetSectorSize sectorClass;

    public void addSpawn(GameObject spawn)
    {//add a spawn into the list
     //		Debug.Log("reached");
        objs.Add(spawn);
        placeSpawn(spawn);
        spawnChecker(spawn);
        spawn.SetActive(true);
    }

    protected void placeSpawn(GameObject spawn)
    {
        //places the spawn in a location near to the player

        float x, y;
        float playerX = pc.transform.position.x;
        float playerY = pc.transform.position.y;

        while (true)
        {
            x = Random.Range(playerX - 10, playerX + 10);
            y = Random.Range(playerY - 10, playerY + 10);
            if (x < playerX + 3 && x > playerX - 3 && y < playerY + 3 && y > playerY - 3)
            {
            }
            else {
                break;
            }
        }
        spawn.transform.position = new Vector3(x, y, 0);
        spawn.GetComponent<SpawnedWeapon>().touchEvent.transform.position = new Vector3(x, y, spawn.GetComponent<SpawnedWeapon>().touchEvent.transform.position.z);
        spawnChecker(spawn);
    }

    public void executeSpawn(GameObject spawn)
    {
        //if (ReferenceEquals(spawn, null))
        //{
            spawn.GetComponent<SpawnedWeapon>().touchEvent.SetActive(false);

            spawn.SetActive(false);
            //Invoke ("reinitSpawn", spawn.GetComponent<Interactable> ().coolDown);
            StartCoroutine(reinitSpawn(spawn, spawn.GetComponent<Interactable>().coolDown));
        //}
	}
    IEnumerator reinitSpawn(GameObject spawn, float coolDown) {
        yield return new WaitForSeconds(coolDown);
        
        placeSpawn(spawn);
        spawn.SetActive(true);
        spawn.GetComponent<SpawnedWeapon>().touchEvent.SetActive(true);
    }
	//public void reinitSpawn(GameObject spawn){
	//	spawn.SetActive (true);
	//	spawn.GetComponent<SpawnedWeapon> ().touchEvent.SetActive (true);
	//}


	public void removeSpawn (SkillType type){//MaterialType, ControlType, GuardType, AssaultType, AuraType
	//remove all spawns with the typename from the list


	}
	public void removeSpawn (GameObject skillObj){//1,2,3...
		//remove one spawn with this name from the list

		foreach (GameObject obj in objs) {
            if (obj.GetComponent<Interactable> ().skillObject.Equals(skillObj)) {
				objs.Remove (obj);
                Destroy(obj.GetComponent<SpawnedWeapon>().touchEvent);
				Destroy (obj);
				break;
			}

		}
	}

	public void spawnChecker(GameObject obj){
		//checks if one thing was not placed right
		//Debug.Log("reached");
		foreach (GameObject obj1 in objs) {
			if(obj == obj1 || obj1.activeSelf == false){
			//	Debug.Log("these two are the same! or something is inactive");
			}
			else if(Vector2.Distance(obj.transform.position, obj1.transform.position) <3){
				placeSpawn(obj);
				spawnChecker(obj);
				//Debug.Log("overlapping spawns! :O");
			}
		}
	}
	public void spawnChecker(){
	//checks if any spawns were not placed right

		foreach (GameObject obj1 in objs) {
			foreach(GameObject obj2 in objs){
				if(obj1 == obj2|| obj1.activeSelf == false){
					Debug.Log("these two are the same! :) or somethign is false");
				}
				else if(Vector2.Distance(obj1.transform.position, obj2.transform.position) <3){
					placeSpawn(obj1);
					spawnChecker(obj1);
					Debug.Log("overlapping spawns! :O");
				}



			}
		}
	}
	public GameObject GetPooledObject(){//do i need this
		//GameObject temp = null;
		return null;
	}


	void Awake(){
		pc = this.gameObject.GetComponent<PlayerController> ();
		pool = this;
	}
	public void distanceChecker (){
		foreach (GameObject obj in objs) {
			if (Vector2.Distance (obj.transform.position, pc.gameObject.transform.position) > 18 && obj.activeSelf && !obj.GetComponent<Interactable>().inUse) {
				placeSpawn (obj);
			}
		}
	}
	void Start () {

		InvokeRepeating("distanceChecker", 0.5f, 0.5f); //checks spawn every 10th of a sec. might get changed to a half of a sec
		objs = new List<GameObject> ();

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
