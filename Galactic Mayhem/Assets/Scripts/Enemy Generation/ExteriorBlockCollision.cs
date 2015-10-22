using UnityEngine;
using System.Collections;

public class ExteriorBlockCollision : MonoBehaviour {

	public Material onState, offState;
	float maxShield;
	float maxHull;
	public float shield;
	public float hull;

	//hud vars
	public int score_value;
	private GameController gc;




	void Start(){
		maxShield = shield;
		maxHull = hull;
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

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy") {
			return;
			
		} else if(other.gameObject.tag == "EnemyBullet"){

		}
		else if(other.gameObject.tag == "Player"){
				other.GetComponent<PlayerController>().takeDamage(getShield()+getHull());
				takeDamage (other.GetComponent<PlayerController>().getHull() + other.GetComponent<PlayerController>().getShield());
			}
		else if(other.gameObject.tag == "Bullet"){
			takeDamage (other.gameObject.GetComponent<Rigidbody> ().velocity.magnitude);
			Destroy(other.gameObject);
		}
	}
	public GameObject deathParticles;
	
	public void onDeath(){
		Instantiate( deathParticles, this.transform.position, this.transform.rotation);
		gc.addScore (score_value);

	}
	public void destroyObject()
	{

		Destroy(this.gameObject);
	}
























}
