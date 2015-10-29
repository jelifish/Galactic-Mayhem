using UnityEngine;
using System.Collections;

public class SpecialWeapon : Weapon{
	//public GameObject weaponSlot; 

	public GameObject projectile;
	public float coolDown;
	public float minCD = 5f, maxCD = 7f;
	public int slotPosition = 1;
	public string fireButton = "Fire";
	
	void Start () {
		
		if (GetComponentInParent<WeaponScript>() != null) {
			weaponSlot = GetComponentInParent<WeaponScript> ().weaponSlot;
			slotPosition = weaponSlot.GetComponent<WeaponSystem> ().getSlotPosition ()+1;
			coolDown = Random.Range (minCD, maxCD);
		}
		
		fireButton = "Fire" + slotPosition as string;
		//Debug.Log (fireButton); //"Fire1"
	}


}
