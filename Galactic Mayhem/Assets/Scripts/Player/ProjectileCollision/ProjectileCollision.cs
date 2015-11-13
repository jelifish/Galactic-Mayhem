using UnityEngine;
using System.Collections;

public class ProjectileCollision : CollisionObject {

	bool multiHit = false; //piercing attribute
	float baseDamage = 0f;
	float damageMultiplier = 1.2f;
	//int integrity = 2;

	//public void calculateDamage(){}

	public override void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy") {
			
			
		} else if(other.gameObject.tag == "EnemyBullet"){
			//takeDamage (other.gameObject.GetComponent<Rigidbody> ().velocity.magnitude);
			//Destroy(other.gameObject);
		}else if(other.gameObject.tag == "Player"){
			//other.GetComponent<PlayerController>().takeDamage(getShield()+getHull());
			//takeDamage (other.GetComponent<PlayerController>().getHull() + other.GetComponent<PlayerController>().getShield());
		}else if(other.gameObject.tag == "Bullet"||other.gameObject.tag == "TriggeredBullet"){
			
		}
	}



}
