using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlasterModule : MonoBehaviour {
	public Transform aim;
	public Load load;
	public List<Load> loader = new List<Load>();


	// bolt variables
	public GameObject enemyBolt;
	public Vector3 attack1DefaultSize = new Vector3(1F,1F,1F);
	public float attack1DefaultDrag = .1f;
	
	public float fireRate = 0.5F;
	private float nextFire = 0.5F;//time until fire again
	private bool loaded = false;


	public void shoot(Load load) 
	{
		Instantiate (load.projectile, this.transform.position + load.offset, this.transform.rotation * load.rotation);
		//anim.SetBool (0, true);
		//return load;
	}
	void Update()
	{

		if (Time.time > nextFire) {
			if (loaded == false) {

				loader.Add (new Load (enemyBolt, Quaternion.identity * Quaternion.Euler(0.0f, Random.Range(-10.0f, 10.0f), 0.0f) ));
				
			}
			
			foreach (Load load in loader) {
				shoot (load); //shoots and removes the loaded object
				
			}
			loader.Clear ();
			loaded = false;//empty magazine
			
			nextFire = Time.time + fireRate;
		} 
			

	}

}
