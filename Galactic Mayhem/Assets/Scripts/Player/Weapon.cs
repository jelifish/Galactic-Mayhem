using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public GameObject weaponSlot;
	public GameController gc;

	void Start(){ //will be overwritten if child has a start()

		weaponSlot = this.transform.parent.gameObject;
	}

}
