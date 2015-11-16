using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnedWeapon : RotateTowards {
	public GameObject touchEvent;
	public float selfDestructTimer = 7f;
	public SpecialWeapon SpecialWeapon;
	void Start(){
		//Destroy (gameObject, 5);
		//Debug.Log ("hit");
		towardsObject = (GameObject)Instantiate (touchEvent, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 3), this.transform.rotation);
		touchEvent = towardsObject;
		towardsObject.GetComponent<DragTransform> ().spawn = this.transform.gameObject;

		StartCoroutine (selfDestruct ());
	}
	public bool destroyThis = true;
	IEnumerator selfDestruct(){
		yield return new WaitForSeconds (selfDestructTimer);

		if (destroyThis) {
			Destroy (gameObject);
			Destroy (towardsObject);
		}
	}









}
