using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponSystem : MonoBehaviour {

	[System.Serializable]
	public class BlasterAttributes
	{
		public float projectileSize; //between .5 and 2 or 3
		public float initialSpeed; //
		public float rateOfFire; // between 1 and .001 left tilt random
		public float fireAngleVariance; //between 0 and 45 normalized random (22.5 for v shape)
		public float directionOfFire; // should be converted into quaternion
		public float projectileAge; //between 2 and 10 normalized random

		//public bool projectileTrajectory1; //0straight, 1sin wave, 2triangle wave, 3curved
		//public bool projectileTrajectory2l //0normal, 1alternating speed, random
		//public bool semiAuto;
		//public float semiAutoTime; //pause time between attacks
		
		public int specialAtkType; //0.fire fast and strong, 1.spray short ranged, 2.piercing snipe, 3.Strike
	}

	public BlasterAttributes generateBlaster(){
		return null;
	}
	public GameObject fireballSound;
	public void shoot(Load load) 
	{
		Instantiate (load.projectile, this.transform.position + load.offset, this.transform.rotation * load.rotation);
		//Instantiate (load.projectile, this.transform.position, this.transform.rotation);
		//Debug.Log (shotSpawn.rotation);
		//Instantiate(load.projectile, new Vector3(3,4,0), shotSpawn.rotation);
//		fireballSound.GetComponent<AudioSource> ().Play ();
		//anim.SetBool (0, true);
		//return load;
	}
	public bool loaded = false;
	public List<Load> loader = new List<Load>();
	public GameObject bolt1;
	public float reloadSpeed = Time.time + .5f;
//	void Update(){
//		if (!Input.GetButton ("Fire1") && Time.time > reloadSpeed) {
//			if (loaded == false) {
//				
//				bolt1.GetComponent<Rigidbody>().drag = .5f;
//				
//				loader.Add (new Load (bolt1, Quaternion.identity * Quaternion.Euler(0f, 0.0f, Random.Range(-20.0f, 20.0f)) ));
//				
//			}
//			
//			foreach (Load load in loader) {
//				shoot (load); //shoots and removes the loaded object
//				
//			}
//			loader.Clear ();
//			loaded = false;//empty magazine
//			
//			reloadSpeed = Time.time + .5f;
//		}
//	}
	public float fireRate = .5f;
	
	private float nextFire = .5f;
	void Update ()
	{
		if (Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(bolt1, this.transform.position, this.transform.rotation);
		}
	}





}
