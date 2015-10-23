﻿using UnityEngine;
using System.Collections;

public class SpecialBlasterMineField : MonoBehaviour {

	public GameObject weaponSlot;
	public GameObject projectile;

	public float coolDown;// = Random.Range(4f,6f);


	private int slotPosition = 1;
	string fireButton = "Fire";
	void Start () {

		if (GetComponentInParent<WeaponScript>() != null) {
			weaponSlot = GetComponentInParent<WeaponScript> ().weaponSlot;
			slotPosition = weaponSlot.GetComponent<WeaponSystem> ().getSlotPosition ()+1;
			coolDown = Random.Range (6f, 10f);
		}

		fireButton = fireButton + slotPosition as string;
		//Debug.Log (fireButton); //"Fire1"
	}

	public float nextFire = 1.0f;
	void Update () {
	
		if (Input.GetButton (fireButton) && Time.time > nextFire && (weaponSlot != null)) {
			nextFire = Time.time + coolDown;
			StartCoroutine(fireStorm());
	//weaponSlot.GetComponent<WeaponSystem> ().loader.Add (new Load (projectile, Quaternion.identity));
			//weaponSlot.GetComponent<WeaponSystem> ().shoot ();

		}




	}
//	void addDelayedForce(GameObject obj){
//			
//		obj.GetComponent<Rigidbody>().AddForce(transform.right* 1000f);
//	
//	}

		IEnumerator fireStorm()
		{
		//projectile.transform.localScale = new Vector3(0.5F,0.5F,0.5F);
			//channeling = true;
			//ready = false;
			while (true) {
				for(int i=0;i<30;i++){
				float tempDrag = projectile.GetComponent<Rigidbody>().drag;
				projectile.GetComponent<Rigidbody>().drag = Random.Range(3f,5f);
					
				weaponSlot.GetComponent<WeaponSystem> ().loader.Add (new Load (projectile, Quaternion.identity * Quaternion.Euler(0f, 0.0f, Random.Range(-20.0f, 20.0f)) ));
				weaponSlot.GetComponent<WeaponSystem> ().shoot ();	
				//Invoke("addDelayedForce(projectile)", 3f);
				projectile.GetComponent<Rigidbody>().drag = tempDrag;
				//foreach (Load load in loader) {
						//shoot (load); //shoots and removes the loaded object
						
					//}
					yield return new WaitForSeconds (.01f);
				}
				break;
			}
		//projectile.transform.localScale = .25f;
		//projectile.GetComponent<Rigidbody>().drag = 10f;
			//channeling = false;
		//projectile.transform.localScale = new Vector3(1F,1F,1F);
			yield return new WaitForSeconds (5f);//cooldown before cast again
			//ready = true;
	
	
		}


















}
