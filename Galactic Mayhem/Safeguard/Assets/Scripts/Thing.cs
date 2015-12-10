using UnityEngine;
using System.Collections;

public class Thing : MonoBehaviour {

	//reference to gamecontroller
	public GameController gc;

	//attributes
	public float shield;
	public float hull;
	[HideInInspector] public float maxShield;
	[HideInInspector] public float maxHull;
	public bool shieldRegenEnabled = true;

	//damage logic
	public float mitigation = 0;
	public float maxMitigation;
	public float armor = 0;
	public float maxArmor;

	//random needed variables
	public bool killed;
	public float maxShieldCooldown = 3;
	public virtual void ShieldUpdate(){
	}
	public float shieldRegenIncrement = 0f;
	public float curShieldCooldown;
	public float currentregenIncrement;

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

	//death logic
	public virtual void onDeath(){
		
		//Instantiate( deathParticles, this.transform.position, this.transform.rotation);
	}
	public virtual void destroyObject()
	{
		Destroy(this.gameObject);
	}

	public void addShield(float heal){
		shield += heal;
		if (shield > maxShield) {
			shield = maxShield;}
		
	}

	IEnumerator shieldRegen(){
		float currentShield = 0;
		curShieldCooldown = maxShieldCooldown;
		currentregenIncrement = 0f;
		
		yield return new WaitForSeconds (.2f);
		
		float tempValue = GetComponent<Thing> ().getShield ();
		
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
