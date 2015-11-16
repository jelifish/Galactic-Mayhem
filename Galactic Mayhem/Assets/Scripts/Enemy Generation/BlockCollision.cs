using UnityEngine;
using System.Collections;

public class BlockCollision : CollisionObject {

	public int scoreValue = 10;


	public override void dropOnDeath(){//very crude item generation. will need changes
		if (Random.value > .95) {
			Instantiate( gc.heal, this.transform.position, this.transform.rotation);
		}
		if (Random.value > .95) {
			Instantiate( gc.shield, this.transform.position, this.transform.rotation);
		}
		if (Random.value > .95) {
			Instantiate( gc.boost, this.transform.position, this.transform.rotation);
		}
	}
	public override void onDeath(){
		dropOnDeath ();
		gc.addScore(scoreValue);		
		Instantiate( deathParticles, this.transform.position, this.transform.rotation);
	}
	
}
