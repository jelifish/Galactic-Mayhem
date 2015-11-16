using UnityEngine;
using System.Collections;

public class BlasterMineFieldInteractable : Interactable{
	public override void mouseUpFire(){
	if (timeSlow) {
			Time.timeScale += 0.5F;
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
			timeSlow = false;
		}

	}
	private bool timeSlow = false;
	public override void mouseDownFire(){
		if (!timeSlow) {	
			Time.timeScale -= 0.5F;
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
			timeSlow = true;
		}
		stopDestroy ();
		StartCoroutine (mineField ());
	}

	public void stopDestroy(){
		GetComponent<SpawnedWeapon> ().destroyThis = false;
	}

	void Start(){
		gameObject.GetComponent<Renderer> ().material.SetColor ("_TintColor", new Color(152/255.0F,203/255.0F,74/255.0F,255f));
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
		if (timeSlow) {
			Time.timeScale += 0.5F;
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
			timeSlow = false;
		}
		Destroy (this.gameObject);


		
		
	}
}
