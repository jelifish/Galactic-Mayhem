﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponSystem : MonoBehaviour {
	

	public GameController gc;

	public void shoot() 
	{if (!channeling) {
			foreach (Load load in loader) {
				Instantiate (load.projectile, this.transform.position + load.offset, this.transform.rotation * load.rotation);
			
				load.sound.GetComponent<AudioSource> ().Play ();
			}
			loader.Clear ();
		}
	}
	public List<Load> loader = new List<Load>();
	public GameObject weapon;
	public float reloadSpeed =  .5f;
	public bool channeling; //if channeling do not shoot anything

	private int slotPosition;
	public void setSlotPosition(int x){
		slotPosition = x;
	}
	public int getSlotPosition(){
		return slotPosition;
	}

	void Start(){
		gc = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
	}


	public GameObject bolt;
	public void initWeapon(){
		if (transform.FindChild("Weapon") != null) {
			Destroy (transform.GetChild (0));//should only ever be one weapon
		} 
		GameObject child = (GameObject)Instantiate (weapon, this.transform.position, this.transform.rotation);

		child.GetComponent<WeaponScript> ().weaponSlot = this.gameObject;
		child.AddComponent<BlasterWeapon>();
		child.GetComponent<BlasterWeapon> ().bolt = bolt;
		BlasterAttributes startWep = new BlasterAttributes ();
		//startWep.rateOfFire = .5f;
		child.GetComponent<BlasterWeapon>().init(startWep);
		//child.GetComponent<BlasterWeapon> ().weaponSlot = this.gameObject;
		child.transform.parent = this.transform;

	}
	public void initWeapon(BlasterAttributes blaster){
		
	}

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






}


















[System.Serializable]
public class BlasterAttributes
{
	public float projectileSize; //between .5 and 2 or 3
	public float initialSpeed; //
	public float rateOfFire; // between 1 and .001 left tilt random
	public float projectileDrag;
	public float fireAngleVariance; //between 0 and 45 normalized random (22.5 for v shape)
	public Quaternion directionOfFire; // should be converted into quaternion
	public float projectileAge; //between 2 and 10 normalized random
	
	//public bool projectileTrajectory1; //0straight, 1sin wave, 2triangle wave, 3curved
	//public bool projectileTrajectory2l //0normal, 1alternating speed, random
	//public bool semiAuto;
	//public float semiAutoTime; //pause time between attacks
	
	public int specialAtkType; //0.fire fast and strong, 1.spray short ranged, 2.piercing snipe, 3.Strike
	
	
	public BlasterAttributes(){
		projectileSize = 2f;
		initialSpeed = 10f;
		rateOfFire = Random.Range(.2f, 1f);
		projectileDrag = 1f;
		fireAngleVariance = 5f;
		//directionOfFire = Quaternio * Random.insideUnitCircle.x;
		projectileAge = 15f;
		//generateSpecial ();
	}
	public GameObject generateSpecial(){
	
//		List<string> specialWeps = new List<string> ();
//
//		///list of special weps
//		specialWeps.Add ("SpecialBlasterMineField");
//		specialWeps.Add ("SpecialStrike");
//
//		return specialWeps[Random.Range(0,specialWeps.Count)];









//		Debug.Log(specialWeps.Count);
//		GameObject new = new GameObject("Special");
//		GameObject specialObj = (GameObject) //Instantiate(new GameObject("Special"),0,Quaternion.identity);
//
//		//specialObj.AddComponent(special.GetType());//gameObject.AddComponent ("special") as SpecialWeapon;
//		//return specialObj;
//
//		return null;



		int length = GameObject.FindGameObjectsWithTag ("Special").Length;
		//Debug.Log("there are "+ length+" num of specials" );
		GameObject spec = GameObject.FindGameObjectsWithTag("Special")[Random.Range (0, length)];  

		return spec;
	}



}