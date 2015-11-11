using UnityEngine;
using System.Collections;

public class ShieldScript : MonoBehaviour {
	private float maxShield;
	public float maxCooldown = 3;


	IEnumerator shieldRegen(){
		float currentShield = 0;
		float cooldown = maxCooldown;

		yield return new WaitForSeconds (.2f);

		float tempValue = GetComponent<CollisionObject> ().getShield ();

		while (true){
			currentShield = GetComponent<CollisionObject> ().getShield ();   //get current shield value

			if (currentShield < maxShield) {         //if the unit has taken damage

			 if(cooldown >= 0f)        // and if the cooldown for the unit is greater than zero
				{
					cooldown-=.2f; //reduce the cooldown, should be consistent with the yield of the ienumerator
				}
				if (cooldown< 0 && tempValue == currentShield) // if the cooldown has reached negative and the tempvalue is the same as the currentShield
				{
					GetComponent<CollisionObject> ().addShield(maxShield * .01f); //then we increase health
					tempValue +=maxShield * .01f;                                        //and increase tempvalue the same 
				}
				else if(tempValue != currentShield){ //other wise
					cooldown = maxCooldown; //put shields on cooldown
					tempValue = currentShield; //reset the shield value
				}
			}
				

			yield return new WaitForSeconds (.2f);
			}

			



	
	}




	// Use this for initialization
	void Start () {
		maxShield = GetComponent<BlockCollision> ().getShield ();
		StartCoroutine (shieldRegen ());

	}
}
