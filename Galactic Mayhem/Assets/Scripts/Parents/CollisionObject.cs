using UnityEngine;
using System.Collections;

public class CollisionObject : MonoBehaviour {

	//abstrsct class block collision supports these things:
	//1. shield/hull + damage calculations on collision
	//2. score and score incrementals
	//3. self destruction -- no child destruction
	//4. particle instantiation
	public float movementSpeedCap;
	public float mitigation = 0;

	[HideInInspector] public float maxShield;
	public float shield;
	public float hull;
	[HideInInspector] public float maxHull;
	
	public GameObject deathParticles;

	public GameController gc;
	public bool shieldRegenEnabled = true;
	
	void Start(){


		maxShield = shield;
		maxHull = hull;
		
		//get gamecontroller
		if (GameObject.FindWithTag ("GameController") != null) {
			gc = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
		}else{ Debug.LogWarning("Cannot Find GameController");
		}
		if (shieldRegenEnabled) {
			StartCoroutine (shieldRegen ());
		}
		
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
			if(!killed){
			onDeath ();
			destroyObject();
			killed = true;
			}
		}
	}
	public void addShield(float heal){
		shield += heal;
		if (shield > maxShield) {
			shield = maxShield;}
		
	}
	
	public virtual void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy") {
			return;
			
		} else if(other.gameObject.tag == "EnemyBullet"){
			
		}else if(other.gameObject.tag == "Player"){
			other.GetComponent<PlayerController>().takeDamage((getShield()+getHull())/other.GetComponent<PlayerController>().mitigation); //effective damage to player
			takeDamage (other.GetComponent<PlayerController>().getHull() + other.GetComponent<PlayerController>().getShield());
		}else if(other.gameObject.tag == "Bullet"||other.gameObject.tag == "TriggeredBullet"){
			takeDamage (other.gameObject.GetComponent<Rigidbody> ().velocity.magnitude);
			other.gameObject.GetComponent<ProjectileCollision>().takeDamage(1);
			//Destroy(other.gameObject);
		}
	}
	public bool killed;
	public virtual void onDeath(){

		Instantiate( deathParticles, this.transform.position, this.transform.rotation);
	}
	public void destroyObject()
	{
		Destroy(this.gameObject);
	}

	public float maxShieldCooldown = 3;
	public virtual void ShieldUpdate(){
	}
	public float shieldRegenIncrement = 0f;
	IEnumerator shieldRegen(){
		float currentShield = 0;
		float cooldown = maxShieldCooldown;
		float currentregenIncrement = 0f;
		
		yield return new WaitForSeconds (.2f);
		
		float tempValue = GetComponent<CollisionObject> ().getShield ();
		
		while (true){
			currentShield = getShield ();   //get current shield value
			
			if (currentShield < maxShield) {         //if the unit has taken damage
				
				if(cooldown >= 0f)        // and if the cooldown for the unit is greater than zero
				{
					cooldown-=.2f; //reduce the cooldown, should be consistent with the yield of the ienumerator
				}
				if (cooldown< 0 && tempValue == currentShield) // if the cooldown has reached negative and the tempvalue is the same as the currentShield
				{
					addShield((int)(maxShield * (.01f+currentregenIncrement))); //then we increase health

					tempValue += (int)(maxShield * (.01f+currentregenIncrement));                                        //and increase tempvalue the same 
					currentregenIncrement += shieldRegenIncrement;
					ShieldUpdate();
				}
				else if(tempValue != currentShield){ //other wise
					cooldown = maxShieldCooldown; //put shields on cooldown
					tempValue = currentShield; //reset the shield value
					currentregenIncrement = 0; //reset regen multiplier value
					ShieldUpdate();
				}
			}
			
			
			yield return new WaitForSeconds (.2f);
		}
		
		
		
	}

}
