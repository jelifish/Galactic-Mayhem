using UnityEngine;
using System.Collections;

public class BlockCollision : CollisionObject {

	public int scoreValue = 0;

	public new void onDeath(){
		gc.addScore(scoreValue);		
		Instantiate( deathParticles, this.transform.position, this.transform.rotation);
	}
	
}
