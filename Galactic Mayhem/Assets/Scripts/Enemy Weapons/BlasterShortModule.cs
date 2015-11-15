using UnityEngine;
using System.Collections;

public class BlasterShortModule : BlasterModule {
	void Update()
	{
		
		if (Time.time > nextFire+Random.Range(-fireRate*.1f, fireRate*1.1f)) {
			if (loaded == false) {
				
				loader.Add (new Load (enemyBolt, Quaternion.identity * Quaternion.Euler(0.0f, Random.Range(- 50.0f, 50.0f), 0.0f) ));
				
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
