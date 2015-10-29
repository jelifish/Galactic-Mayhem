using UnityEngine;
using System.Collections;

public class CollisionObject : MonoBehaviour {

	//abstrsct class block collision supports these things:
	//1. shield/hull + damage calculations on collision
	//2. score and score incrementals
	//3. self destruction -- no child destruction
	//4. particle instantiation
	
	public float maxShield;
	public float shield;
	public float hull;
	public float maxHull;
	
	public GameObject deathParticles;

	public GameController gc;
	
	void Start(){
		maxShield = shield;
		maxHull = hull;
		
		//get gamecontroller
		if (GameObject.FindWithTag ("GameController") != null) {
			gc = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
		}else{ Debug.LogWarning("Cannot Find GameController");}
		
	}

	public float getShield(){
		return shield;
	}
	public float getHull(){
		return hull;
	}
	public void takeDamage(float damage){
		shield -= damage;
		if (shield < 0) {
			
			hull += shield;
			shield = 0f;
		}
		if (hull <= 0.0f) {
			onDeath ();
			destroyObject();
		}
	}
	public void addShield(float heal){
		shield += heal;
		if (shield > maxShield) {
			shield = maxShield;}
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy") {
			return;
			
		} else if(other.gameObject.tag == "EnemyBullet"){
			
		}else if(other.gameObject.tag == "Player"){
			other.GetComponent<PlayerController>().takeDamage(getShield()+getHull());
			takeDamage (other.GetComponent<PlayerController>().getHull() + other.GetComponent<PlayerController>().getShield());
		}else if(other.gameObject.tag == "Bullet"||other.gameObject.tag == "TriggeredBullet"){
			takeDamage (other.gameObject.GetComponent<Rigidbody> ().velocity.magnitude);
			Destroy(other.gameObject);
		}
	}
	
	public void onDeath(){	
		Instantiate( deathParticles, this.transform.position, this.transform.rotation);
	}
	public void destroyObject()
	{
		Destroy(this.gameObject);
	}




}
