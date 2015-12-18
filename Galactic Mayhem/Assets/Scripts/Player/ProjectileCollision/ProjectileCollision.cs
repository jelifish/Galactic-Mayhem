using UnityEngine;
using System.Collections;

public class ProjectileCollision : MonoBehaviour {
	public float shield;
	[HideInInspector] public float maxShield;
	public float hull;
	[HideInInspector] public float maxHull;
	
	public float armor = 0;
	public float maxArmor;
	public bool killed;
	public GameObject deathParticles;

	public void takeDamage(float damage){
		//Debug.Log (damage);
		shield -= damage;
		if (shield < 0) {
			if(armor > (shield*-1))
			{
			}else{
				hull += shield+armor;}
			shield = 0f;
		}
		if (hull <= 0.0f) {
			if(!killed){
				onDeath ();
				destroyObject();
				killed = true;
			}
		}
	}
	public virtual void onDeath(){
		
		Instantiate( deathParticles, this.transform.position, this.transform.rotation);
	}
	public virtual void destroyObject()
	{
		Destroy(this.gameObject);
	}
	//bool multiHit = false; //piercing attribute
	//float baseDamage = 0f;
	//float damageMultiplier = 1.2f;
	//int integrity = 2;

	//public void calculateDamage(){}

//	public override void OnTriggerEnter(Collider other)
//	{
//		if (other.gameObject.tag == "Enemy") {
//			
//			
//		} else if(other.gameObject.tag == "EnemyBullet"){
//			//takeDamage (other.gameObject.GetComponent<Rigidbody> ().velocity.magnitude);
//			//Destroy(other.gameObject);
//		}else if(other.gameObject.tag == "Player"){
//			//other.GetComponent<PlayerController>().takeDamage(getShield()+getHull());
//			//takeDamage (other.GetComponent<PlayerController>().getHull() + other.GetComponent<PlayerController>().getShield());
//		}else if(other.gameObject.tag == "Bullet"||other.gameObject.tag == "TriggeredBullet"){
//			
//		}
//	}



}
