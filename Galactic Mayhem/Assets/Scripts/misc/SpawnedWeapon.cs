using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnedWeapon : RotateTowards {//this will rotate to mouse
	public GameObject touchEvent;
	void Awake(){
		towardsObject = (GameObject)Instantiate (touchEvent, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 3), this.transform.rotation);
		touchEvent = towardsObject;
		towardsObject.GetComponent<DragTransform> ().spawn = this.transform.gameObject;
		var offset = new Vector2(towardsObject.transform.position.x - transform.position.x, towardsObject.transform.position.y - transform.position.y);
		var angleofFire = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
		targetVector = towardsObject.transform.position - transform.position;
		
		transform.rotation = Quaternion.Euler(0, 0, angleofFire);
	}
	void Start(){
	}
	public bool destroyThis = false;









}
