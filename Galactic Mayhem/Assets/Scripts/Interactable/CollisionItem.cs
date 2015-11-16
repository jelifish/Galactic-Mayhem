using UnityEngine;
using System.Collections;

public class CollisionItem : CollisionObject {
	public override void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy") {

			
		} else if(other.gameObject.tag == "EnemyBullet"){
			
		}else if(other.gameObject.tag == "Player"){

			if (this.name == "HealthPickup(Clone)"&&other.GetComponent<PlayerController>().hull<other.GetComponent<PlayerController>().maxHull){
			other.GetComponent<PlayerController>().recoverHull();
			Destroy(gameObject);
			}
			if (this.name == "ShieldPickup(Clone)"&&other.GetComponent<PlayerController>().shield<other.GetComponent<PlayerController>().maxShield){
				other.GetComponent<PlayerController>().recoverShield();
				Destroy(gameObject);
			}
			if (this.name == "SpeedPickup(Clone)"){
				other.GetComponent<PlayerController>().boostSpeed();
				Destroy(gameObject);
			}









			if (this.name == "AcceleratorInteractable(Clone)"){
				other.GetComponent<PlayerController>().boostSpeed();
				Destroy(gameObject);
			}
			if (this.name == "SpeedPickup(Clone)"){
				other.GetComponent<PlayerController>().boostSpeed();
				Destroy(gameObject);
			}

		}else if(other.gameObject.tag == "Bullet"||other.gameObject.tag == "TriggeredBullet"){
		}
	}

//	IEnumerator firemass(){
//		yield return new WaitForSeconds (2f);
//		foreach(GameObject bolt in GameObject.FindGameObjectsWithTag("Bullet")){
//
//			bolt.GetComponent<Rigidbody>().AddForce(bolt.transform.right * 100);
//			
//		}
//	}


}
