using UnityEngine;
using System.Collections;

public class ExteriorBlockCollision : MonoBehaviour {

	public Material onState, offState;
	float maxShield;
	public float shield;
	public float hull;

	void Start(){
		maxShield = shield;
	
	}







	public float getShield(){
		return shield;
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
			
		} else {
			Destroy (other.gameObject);
			takeDamage (other.gameObject.GetComponent<Rigidbody> ().velocity.magnitude);
		}
	}
	public GameObject deathParticles;
	
	public void onDeath(){
		Instantiate( deathParticles, this.transform.position, this.transform.rotation);
	}
	public void destroyObject()
	{
		Destroy(this.gameObject);
	}
}
