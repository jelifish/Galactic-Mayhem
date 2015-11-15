using UnityEngine;
using System.Collections;

public class BlasterMineFieldInteractable : Interactable{
	public override void fireAtLocation(){
		
	}
	public override void fireAtTarget(){
		stopDestroy ();
		StartCoroutine (mineField ());
	}

	public void stopDestroy(){
		GetComponent<SpawnedWeapon> ().destroyThis = false;
	}
	IEnumerator mineField()
	{
		initialSpeed = 5;
		while (true) {
			for(int i=0;i<50;i++){
				GameObject temp = (GameObject)Instantiate (projectile, this.transform.position, this.transform.rotation * Quaternion.Euler(0f, 0.0f, Random.Range(-10.0f, 10.0f)));
				temp.GetComponent<Mover>().speed = Random.Range(initialSpeed*.9f, initialSpeed*1.1f);
				yield return new WaitForSeconds (.01f);
			}
			break;
		}
		Destroy (this.gameObject);
		
		
	}
}
