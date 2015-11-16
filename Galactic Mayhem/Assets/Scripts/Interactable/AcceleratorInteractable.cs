using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AcceleratorInteractable : Interactable {

	public List<GameObject> bullets = new List<GameObject>();

	public override void mouseUpFire(){
		Time.timeScale += 0.5F;
		Time.fixedDeltaTime = 0.02F * Time.timeScale;
		StartCoroutine (Blast ());

	}
	public override void mouseDownFire(){
		Time.timeScale -= 0.5F;
		Time.fixedDeltaTime = 0.02F * Time.timeScale;
		stopDestroy ();
		foreach(GameObject bolt in GameObject.FindGameObjectsWithTag("Bullet")){
			//Debug.Log("found");]
			bullets.Add(bolt);
			bolt.GetComponent<RotateTowards>().towardsObject = GetComponent<SpawnedWeapon>().touchEvent.gameObject;
			
			
			//bolt.GetComponent<Rigidbody>().AddForce(bolt.transform.right * 50);
			
		}
	}

	public void stopDestroy(){
		GetComponent<SpawnedWeapon> ().destroyThis = false;
	}

	void Start(){
		gameObject.GetComponent<Renderer> ().material.SetColor ("_TintColor", new Color(247/255.0F,216/255.0F,66/255.0F,255f));
	}

	IEnumerator Blast()
	{
		yield return new WaitForSeconds (0.1f);
		foreach(GameObject bolt in bullets){
			if(bolt !=null&&Vector3.Distance(bolt.transform.position, this.transform.position) < 10){
			bolt.GetComponent<Rigidbody>().AddForce(bolt.transform.right * 25);
			}
		}
		yield return new WaitForSeconds (0.1f);
		foreach(GameObject bolt in bullets){
			if(bolt !=null&&Vector3.Distance(bolt.transform.position, this.transform.position) < 10){
				bolt.GetComponent<Rigidbody>().AddForce(bolt.transform.right * 50);
			}
		}
		yield return new WaitForSeconds (0.1f);
		foreach(GameObject bolt in bullets){
			if(bolt !=null&&Vector3.Distance(bolt.transform.position, this.transform.position) < 10){
				bolt.GetComponent<Rigidbody>().AddForce(bolt.transform.right * 75);
			}
		}
		yield return new WaitForSeconds (0.1f);
		foreach(GameObject bolt in bullets){
			if(bolt !=null&&Vector3.Distance(bolt.transform.position, this.transform.position) < 10){
				bolt.GetComponent<Rigidbody>().AddForce(bolt.transform.right * 100);
			}
		}
		Destroy (this.gameObject);
		yield return new WaitForSeconds (0.1f);
		
	}



}
