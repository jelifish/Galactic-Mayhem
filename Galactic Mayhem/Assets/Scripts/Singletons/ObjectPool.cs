using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {
	public static ObjectPool pool = null;
	public GameObject bolt;
	private List<GameObject> objs;
	private Queue<GameObject> activeObj;

	public BoxColSetSectorSize sectorClass;
	public int poolSize = 1000;

	protected GameObject containerObject;

	void Awake(){
		pool = this;
	}
	public int count;
	public int size;
	void Start(){
		count = 0;
		containerObject = new GameObject("ProjectilePool");
		objs = new List<GameObject> ();
		activeObj = new Queue<GameObject> ();
		for (int i = 0; i<= poolSize; i++) {
			GameObject obj = (GameObject)Instantiate(bolt);
			obj.transform.parent = containerObject.transform;
			obj.SetActive(false);
			objs.Add(obj);
			activeObj.Enqueue (obj);
		}

	}
	public void AddObject (){//this code is also used in start but start does not increase poolsize
		poolSize++;
		GameObject obj = (GameObject)Instantiate(bolt);
		obj.transform.parent = containerObject.transform;
		obj.SetActive(false);
		objs.Add(obj);
		activeObj.Enqueue (obj);
	}
	public void AddObject(int amount){
		for (int i = 0; i<=amount; i++) {
			AddObject();
		}
	}

	public void DestroyObject(GameObject obj){
		//deathparticles here
		obj.SetActive (false);
	}
	public void FlushObjects(){//this method returns all queue objects back to the gameobject list and deactivates.
		GameObject temp = null;
		int counter = 0;
		while (counter <= poolSize) { //do for all objects in pool

			temp =activeObj.Dequeue();
			if (temp == null)
			{
				Debug.Log("wtf you nulled this object? find out where right now!");
				break;
			}
			else{
			temp.SetActive(false);
			temp.GetComponent<ProjectileCollision> ().sizeSet = false;
			activeObj.Enqueue(temp);
			}
			counter++;
		}
	}
	//public void
	public int bulletHull = 1;
	public void Neutralize(GameObject temp){
		activeObj.Enqueue (temp);

		temp.transform.rotation = Quaternion.identity;
		temp.GetComponent<Rigidbody>().velocity = Vector3.zero;
		temp.GetComponent<ProjectileCollision> ().hull = bulletHull;
		temp.GetComponent<ProjectileCollision> ().killed = false;
		temp.GetComponent<ProjectileCollision> ().SetSectorSize (sectorClass.getTotalSectorSize());
		temp.transform.localScale = new Vector3(.4f, .4f,.4f);
        //temp.GetComponent<SphereCollider>().radius = .6f;
        //.1f = normal 
        temp.SetActive(true);
	}
	public GameObject GetPooledObject(){
		GameObject temp = null;
//		for(int i=0; i<objs.Count; i++)
//		{
//			temp = objs[i];
//			if(count < poolSize && !temp.activeInHierarchy)
//			{
//				//activeObj.Enqueue(objs[i]);
//				count++;
//
//				Neutralize(temp);
//				return temp;
//			}
//		}
		while (true) {
			temp = activeObj.Dequeue();
			size = activeObj.Count;
			if(temp == null){ //if queue is empty then we recursive

				Debug.Log("out of objects");
				//return GetPooledObject();//caution: will infinite loop if there is nothing in both lists
			}
//			else if(temp.activeInHierarchy){ //if temp is active then we return this object because it is the oldest object on the stack
//				activeObj.Enqueue(temp);
//				return temp;
//			}
			else {
				if(temp.activeInHierarchy){
					//activeObj.Enqueue(temp);
					Neutralize(temp);
					count++;
					return temp;

				}else{
					//activeObj.Enqueue(temp);
					Neutralize(temp);
					count++;
					return temp;
				}

			}
		}
	}


}
