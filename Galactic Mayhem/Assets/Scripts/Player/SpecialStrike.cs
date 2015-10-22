using UnityEngine;
using System.Collections;

public class SpecialStrike : MonoBehaviour {

	public GameObject weaponSlot;
	public GameObject projectile;

	public float coolDown;// = Random.Range(4f,6f);


	private int slotPosition = 1;
	string fireButton = "Fire";
	void Start () {

		if (GetComponentInParent<WeaponScript>() != null) {
			weaponSlot = GetComponentInParent<WeaponScript> ().weaponSlot;
			slotPosition = weaponSlot.GetComponent<WeaponSystem> ().getSlotPosition ()+1;
			coolDown = Random.Range (1f, 1f);
		}

		fireButton = fireButton + slotPosition as string;
		//Debug.Log (fireButton); //"Fire1"
	}

	public float nextFire = 3.0f;
	void Update () {
	
		if (Input.GetButton (fireButton) && Time.time > nextFire && (weaponSlot != null)) {
			nextFire = Time.time + coolDown;
			weaponSlot.GetComponent<WeaponSystem> ().loader.Add (new Load (projectile, Quaternion.identity));
			weaponSlot.GetComponent<WeaponSystem> ().shoot ();

		}




	}
}
