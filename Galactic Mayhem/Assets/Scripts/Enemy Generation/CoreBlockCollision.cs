using UnityEngine;
using System.Collections;

public class CoreBlockCollision : BlockCollision{

	public override void onDeath(){
		dropOnDeath ();
		gc.addScore(scoreValue);
		gc.addWave ();
		foreach (Transform child in this.transform)
		{
			if(child.GetComponent<OuterBlockCollision>()!= null)
			{child.GetComponent<OuterBlockCollision>().onDeath();
			}
		}

		Instantiate( deathParticles, this.transform.position, this.transform.rotation);
	}
}
