using UnityEngine;
using System.Collections;

public class BlasterWeapon : Weapon {

	public BlasterAttributes blaster;
	public GameObject bolt;

	public void Start(){ //this overwrites parent
		bolt.GetComponent<Rigidbody> ().drag = blaster.projectileDrag;
		weaponSlot = this.transform.parent.gameObject; 
	}
	
	public void init(BlasterAttributes blaster)
	{
		this.blaster = blaster;
		bolt.GetComponent<Mover> ().age = blaster.projectileAge;

		GameObject special = (GameObject) Instantiate (blaster.generateSpecial(), this.transform.position, this.transform.rotation);

		special.transform.parent = this.transform;

	}
	public float nextFire = 3.0f;
	void Update ()
	{
		if (Time.time > nextFire && weaponSlot.GetComponent<WeaponSystem>().channeling == false)
		{
			nextFire = Time.time + blaster.rateOfFire;

			weaponSlot.GetComponent<WeaponSystem>().loader.Add (new Load (bolt, Quaternion.identity * Quaternion.Euler(0f, 0.0f, Random.Range(0f, 360f)) ));
			weaponSlot.GetComponent<WeaponSystem>().loader.Add (new Load (bolt, Quaternion.identity * Quaternion.Euler(0f, 0.0f, Random.Range(0f, 360f)) ));
			//weaponSlot.GetComponent<WeaponSystem>().loader.Add (new Load (bolt, Quaternion.identity * Quaternion.Euler(0f, 0.0f, Random.Range(0f, 360f)) ));
			weaponSlot.GetComponent<WeaponSystem>().shoot();
		}

	}
}
