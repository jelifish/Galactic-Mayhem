﻿using UnityEngine;
using System.Collections;

public class OuterBlockCollision : BlockCollision {
	public override void onDeath(){
		Instantiate( deathParticles, this.transform.position, this.transform.rotation);
		gc.addScore (scoreValue); 
		dropOnDeath ();
	}

}
