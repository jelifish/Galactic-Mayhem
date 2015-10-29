using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public GameObject weaponSlot;
	void Start(){ //will be overwritten if child has a start()
		weaponSlot = this.transform.parent.gameObject;
	}


//	void Start(){
//		weaponSlot = GetComponentInParent<Transform>().gameObject;
//		onStart ();
//	}
//	public void onStart(){
//
//	}

}
