using UnityEngine;
using System.Collections;

public class SpawnedWeapon : LookAtObject {
	public GameObject touchEvent;
	void Start(){

		targetObject = (GameObject)Instantiate (touchEvent, this.transform.position, this.transform.rotation);
	}

}
