using UnityEngine;
using System.Collections;

public class CollisionObject : MonoBehaviour {

	//abstrsct class block collision supports these things:
	//1. shield/hull + damage calculations on collision
	//2. score and score incrementals
	//3. self destruction -- no child destruction
	//4. particle instantiation
	public float sectorDifficulty;
	public float movementSpeedCap;
	public float mitigation = 0;
	public float maxMitigation;
	public float armor = 0;
	public float maxArmor;
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
		maxArmor = armor;
		maxMitigation = mitigation;
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

		Debug.Log (damage);
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
			takeDamage (getShield()+getHull());
		}else if(other.gameObject.tag == "Bullet"||other.gameObject.tag == "TriggeredBullet"){

			float bulletScale = other.transform.localScale.x;
			float x = bulletScale + .9f;

			float bulletMulti = x * x; 
			takeDamage (other.gameObject.GetComponent<Rigidbody> ().velocity.magnitude * bulletMulti);
			other.gameObject.GetComponent<ProjectileCollision>().takeDamage(1);
		}
	}
	public bool killed;
	public virtual void onDeath(){

		Instantiate( deathParticles, this.transform.position, this.transform.rotation);
	}
	public virtual void destroyObject()
	{
		Destroy(this.gameObject);
	}
	public virtual void dropOnDeath(){
	}

	public float maxShieldCooldown = 3;
	public virtual void ShieldUpdate(){
	}
	public float shieldRegenIncrement = 0f;
	public float curShieldCooldown;
	public float currentregenIncrement;
	IEnumerator shieldRegen(){
		float currentShield = 0;
		curShieldCooldown = maxShieldCooldown;
		currentregenIncrement = 0f;
		
		yield return new WaitForSeconds (.2f);
		
		float tempValue = GetComponent<CollisionObject> ().getShield ();
		
		while (true){
			currentShield = getShield ();   //get current shield value
			
			if (currentShield < maxShield) {         //if the unit has taken damage
				
				if(curShieldCooldown >= 0f)        // and if the cooldown for the unit is greater than zero
				{
					curShieldCooldown-=.1f; //reduce the cooldown, should be consistent with the yield of the ienumerator
				}
				if (curShieldCooldown< 0 && tempValue == currentShield) // if the cooldown has reached negative and the tempvalue is the same as the currentShield
				{
					addShield((int)(maxShield * (.01f+currentregenIncrement))); //then we increase health

					tempValue += (int)(maxShield * (.01f+currentregenIncrement));                                        //and increase tempvalue the same 
					currentregenIncrement += shieldRegenIncrement;
					ShieldUpdate();
				}
				else if(tempValue != currentShield){ //other wise
					curShieldCooldown = maxShieldCooldown; //put shields on cooldown
					tempValue = currentShield; //reset the shield value
					currentregenIncrement = 0; //reset regen multiplier value
					ShieldUpdate();
				}
			}
			
			
			yield return new WaitForSeconds (.1f);
		}
		
		
		
	}

}
