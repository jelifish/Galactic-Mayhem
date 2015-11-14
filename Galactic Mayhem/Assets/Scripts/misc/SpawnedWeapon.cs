using UnityEngine;
using System.Collections;

public class SpawnedWeapon : LookAtObject {
	public GameObject touchEvent;

	public SpecialWeapon SpecialWeapon;

	void Start(){

		targetObject = (GameObject)Instantiate (touchEvent, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 3), this.transform.rotation);
	}

}
