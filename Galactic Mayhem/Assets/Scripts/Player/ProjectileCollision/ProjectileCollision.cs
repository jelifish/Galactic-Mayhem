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
		//Destroy(this.gameObject);
		this.gameObject.SetActive (false);
	}
	
	public float age;
	private Transform originalObject;
	public int mirrorRate = 0;
	public float totalSectorSize;
	void Start()
	{
		//GetComponent<Rigidbody>().velocity =transform.right.normalized*speed;
		//GetComponent<Rigidbody>().AddForce(transform.right * speed*5);
		
		
		originalObject = GetComponent<Rigidbody> ().transform;
		//Destroy (gameObject, age);


		SetSectorSize ();
	}
	public void SetSectorSize(){
		totalSectorSize = GameObject.Find ("GameController").GetComponentInChildren<BoxColSetSectorSize> ().getTotalSectorSize ()/1.95f; //expensive call
	}

	void FixedUpdate()
	{
		if (mirrorRate >= 10) {
			mirrorRate = 0;

			this.gameObject.SetActive(false);
			//this.gameObject.transform.position = Vector3.zero;

		}
		
		if (this.gameObject.tag != "EnemyBullet") { ///dont port if enemy bullet. 
			
			//port object into other side of screen (horizontal)
			if (Mathf.Abs (GetComponent<Rigidbody> ().position.x) >= totalSectorSize) {
				mirrorRate += 1;
				GetComponent<Rigidbody> ().position = Vector3.Reflect (originalObject.position, Vector3.right);
				originalObject = GetComponent<Rigidbody> ().transform;
			}
			//port object into other side of screen (vertical)
			if (Mathf.Abs (GetComponent<Rigidbody> ().position.y) >= totalSectorSize) {
				mirrorRate += 1;
				GetComponent<Rigidbody> ().position = Vector3.Reflect (originalObject.position, Vector3.up);
				originalObject = GetComponent<Rigidbody> ().transform;
			}
		}
		
		
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
