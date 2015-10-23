using UnityEngine;
using System.Collections;

public class BlasterWeapon : MonoBehaviour {

	public BlasterAttributes blaster;
	public GameObject weaponSlot;
	public GameObject bolt;
	public void Start(){
		bolt.GetComponent<Rigidbody> ().drag = blaster.projectileDrag;
	}
	
	public void init(BlasterAttributes blaster)
	{
		this.blaster = blaster;
		bolt.GetComponent<Mover> ().age = blaster.projectileAge;

		GameObject special = (GameObject) Instantiate (blaster.generateSpecial(), this.transform.position, this.transform.rotation);
		special.tag = "ActiveSpecial";
		special.transform.parent = this.transform;

	}
	public float nextFire = 3.0f;
	void Update ()
	{
		if (Time.time > nextFire && weaponSlot.GetComponent<WeaponSystem>().channeling == false)
		{
			nextFire = Time.time + blaster.rateOfFire;

			weaponSlot.GetComponent<WeaponSystem>().loader.Add (new Load (bolt, Quaternion.identity * Quaternion.Euler(0f, 0.0f, Random.Range(-20.0f, 20.0f)) ));
			weaponSlot.GetComponent<WeaponSystem>().shoot();
		}

	}
}
