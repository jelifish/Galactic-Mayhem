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

	void Start(){
		containerObject = new GameObject("ObjectPool");
		Debug.Log("hitthe p[ool");
		objs = new List<GameObject> ();
		activeObj = new Queue<GameObject> ();
		for (int i = 0; i< poolSize; i++) {
			GameObject obj = (GameObject)Instantiate(bolt);
			obj.SetActive(false);
			objs.Add(obj);
		}

	}

//	public void FlushObjects(){//this method returns all queue objects back to the gameobject list and deactivates.
//		GameObject temp = null;
//		while (activeObj.Peek != null) {
//			temp =activeObj.Dequeue;
//			temp.SetActive(false);
//			objs.Add(temp);
//		}
//	}


	public GameObject GetPooledObject(){
		for(int i=0; i<objs.Count; i++)
		{
			if(!objs[i].activeInHierarchy)
			{
				activeObj.Enqueue(objs[i]);
				return objs[i];
			}
		}
		while (true) {
			GameObject temp = activeObj.Dequeue();
			if(temp == null){ //if queue is empty then we recursive

				Debug.Log("out of objects");
				//return GetPooledObject();//caution: will infinite loop if there is nothing in both lists
			}
//			else if(temp.activeInHierarchy){ //if temp is active then we return this object because it is the oldest object on the stack
//				activeObj.Enqueue(temp);
//				return temp;
//			}
			else {
				activeObj.Enqueue(temp);
				return temp;
			}
		}
	}


}
