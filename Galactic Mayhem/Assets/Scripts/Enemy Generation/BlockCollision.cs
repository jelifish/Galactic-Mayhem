using UnityEngine;
using System.Collections;

public class BlockCollision : CollisionObject {

	public int scoreValue = 10;
//
	public override void onDeath(){

		gc.addScore(scoreValue);		
		Instantiate( deathParticles, this.transform.position, this.transform.rotation);
	}
	
}
