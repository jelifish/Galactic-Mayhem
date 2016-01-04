using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {
	public static ObjectPool pool = null;
	public GameObject bolt;
	List<GameObject> objs;
	Queue<GameObject> activeObj;

	public int poolSize = 1000;

	protected GameObject containerObject;

	void Awake(){
		pool = this;
	}
	public int count;
	void Start(){
		count = 0;
		containerObject = new GameObject("ObjectPool");
		objs = new List<GameObject> ();
		activeObj = new Queue<GameObject> ();
		for (int i = 0; i< poolSize; i++) {
			GameObject obj = (GameObject)Instantiate(bolt);
			obj.SetActive(false);
			objs.Add(obj);
		}

	}

	public void FlushObjects(){//this method returns all queue objects back to the gameobject list and deactivates.
		GameObject temp = null;
		while (activeObj.Count >0) {
			temp =activeObj.Dequeue();
			if (temp == null)
			{break;}
			else{
			temp.SetActive(false);
			objs.Add(temp);
			}
		}
	}


	public GameObject GetPooledObject(){
		GameObject temp = null;
		for(int i=0; i<objs.Count; i++)
		{
			temp = objs[i];
			if(!temp.activeInHierarchy)
			{
				activeObj.Enqueue(objs[i]);
				count++;
				temp.SetActive(true);
				temp.transform.rotation = Quaternion.identity;
				temp.GetComponent<Rigidbody>().velocity = Vector3.zero;
				return temp;
			}
		}
		while (true) {
			temp = activeObj.Dequeue();
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
					activeObj.Enqueue(temp);
					temp.SetActive(true);
					temp.transform.rotation = Quaternion.identity;
					temp.GetComponent<Rigidbody>().velocity = Vector3.zero;
					count++;
					return temp;

				}else{
					activeObj.Enqueue(temp);
					temp.SetActive(true);
					temp.transform.rotation = Quaternion.identity;
					temp.GetComponent<Rigidbody>().velocity = Vector3.zero;
					count++;
					return temp;
				}

			}
		}
	}


}
