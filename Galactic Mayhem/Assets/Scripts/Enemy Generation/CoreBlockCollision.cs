using UnityEngine;
using System.Collections;

public class CoreBlockCollision : MonoBehaviour {
	
	public Material onState, offState;
	float maxShield;
	public float shield;
	public float hull;
	public float maxHull;
	
	void Start(){
		maxShield = shield;
		maxHull = hull;
		
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
			Destroy (gameObject);
		}
	}
	public void addShield(float heal){
		shield += heal;
		if (shield > maxShield) {
			shield = maxShield;}
		
	}
	private int selfInflicted;
	//	void OnCollisionEnter(Collision other)
	//	{
	//		if (other.gameObject.tag == "Enemy") {
	//			selfInflicted++;
	//				if(selfInflicted>5)
	//			{Destroy (this.gameObject);}
	//			return;
	//		
	//		}
	//			Destroy (other.gameObject);
	//			takeDamage (other.gameObject.GetComponent<Rigidbody> ().velocity.magnitude);
	//	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy") {
			//			selfInflicted++;
			//			if(selfInflicted>5)
			//			{Destroy (this.gameObject);}
			return;
			
		} else if(other.gameObject.tag == "EnemyBullet"){
			
		}else if(other.gameObject.tag == "Player"){
			other.GetComponent<PlayerController>().takeDamage(getShield()+getHull());
			takeDamage (9999999f);
		}else if(other.gameObject.tag == "Bullet"){
			takeDamage (other.gameObject.GetComponent<Rigidbody> ().velocity.magnitude);
			
		}
	}
	public GameObject deathParticles;
	
	public void onDeath(){

		foreach (Transform child in this.transform)
		{
			if(child.GetComponent<ExteriorBlockCollision>()!= null)
			{child.GetComponent<ExteriorBlockCollision>().onDeath();
			}
		}

		Instantiate( deathParticles, this.transform.position, this.transform.rotation);
	}
	public void destroyObject()
	{
		Destroy(this.gameObject);
	}
}
