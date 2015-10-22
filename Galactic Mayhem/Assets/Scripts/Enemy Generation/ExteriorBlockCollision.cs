using UnityEngine;
using System.Collections;

public class ExteriorBlockCollision : MonoBehaviour {

	public Material onState, offState;
	float maxShield;
	float maxHull;
	public float shield;
	public float hull;

	//HUD Variables
	public int score_value;
	private GameController gc;


	void Start(){
		maxShield = shield;
		maxHull = hull;

		//HUD
		GameObject gc_object = GameObject.FindWithTag ("GameController");
		if (gc_object != null)
			gc = gc_object.GetComponent<GameController> ();
		else
			Debug.Log ("Cannot find 'Gamecontroller' script.\n");
	}


	public float getShield(){
		return shield;
	}
	public float getHull(){
		return hull;
	}
	public void addHull(float heal)
	{
		hull += heal;
		if (hull > maxHull) {
			hull = maxHull;}
	}

	public void addShield(float heal){
		shield += heal;
		if (shield > maxShield) {
			shield = maxShield;}
		
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

//	private int selfInflicted;
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

		}
		else if(other.gameObject.tag == "Player"){
				other.GetComponent<PlayerController>().takeDamage(getShield()+getHull());
				takeDamage (9999999f);
			}
		else if(other.gameObject.tag == "Bullet"){
			takeDamage (other.gameObject.GetComponent<Rigidbody> ().velocity.magnitude);

		}
	}
	public GameObject deathParticles;
	
	public void onDeath(){
		Instantiate( deathParticles, this.transform.position, this.transform.rotation);
		gc.AddScore (score_value);
	}

}