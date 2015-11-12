using UnityEngine;
using System.Collections;

public class CoreBlockCollision : BlockCollision{
	
//	public Material onState, offState;
//	private float maxShield;
////	public float shield;
////	public float hull;
//	public float maxHull;
//	
//	//hud vars
//	public int score_value;
//	private GameController gc;
//	
//	void Start(){
//		maxShield = shield;
//		maxHull = hull;
//		if (GameObject.FindWithTag ("GameController") != null) {
//			gc = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
//		}else{ Debug.LogWarning("Cannot Find GameController");}
//	}
//	
//	
//	public float getShield(){
//		return shield;
//	}
//	public float getHull(){
//		return hull;
//	}
//	public void takeDamage(float damage){
//		shield -= damage;
//		if (shield < 0) {
//			
//			hull += shield;
//			shield = 0f;
//		}
//		if (hull <= 0.0f) {
//			onDeath ();
//			Destroy (gameObject);
//		}
//	}
//	public void addShield(float heal){
//		shield += heal;
//		if (shield > maxShield) {
//			shield = maxShield;}
//		
//	}
//	private int selfInflicted;
//
//
//	void OnTriggerEnter(Collider other)
//	{
//		if (other.gameObject.tag == "Enemy") {
//			return;
//			
//		} else if(other.gameObject.tag == "EnemyBullet"){
//			
//		}else if(other.gameObject.tag == "Player"){
//			other.GetComponent<PlayerController>().takeDamage(getShield()+getHull());
//			takeDamage (other.GetComponent<PlayerController>().getHull() + other.GetComponent<PlayerController>().getShield());
//		}else if(other.gameObject.tag == "Bullet"||other.gameObject.tag == "TriggeredBullet"){
//			takeDamage (other.gameObject.GetComponent<Rigidbody> ().velocity.magnitude);
//			Destroy(other.gameObject);
//		}
//	}
//	public GameObject deathParticles;
//	

	public override void onDeath(){

		Debug.Log("enemydeath");
		gc.addScore(scoreValue);
		gc.addWave ();
		foreach (Transform child in this.transform)
		{
			if(child.GetComponent<OuterBlockCollision>()!= null)
			{child.GetComponent<OuterBlockCollision>().onDeath();
			}
		}

		Instantiate( deathParticles, this.transform.position, this.transform.rotation);
	}
//	public void destroyObject()
//	{
//		Destroy(this.gameObject);
//	}
}
