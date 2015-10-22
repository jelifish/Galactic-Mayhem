using UnityEngine;
using System.Collections;

public class OnDeath : MonoBehaviour {

	public GameObject deathParticles;

	public void onDeath(){
		Instantiate( deathParticles, this.transform.position, this.transform.rotation);
		Destroy(deathParticles);
//		ParticleSystem ps = deathParticles.GetComponent<ParticleSystem>();
//		ps.enableEmission = true;
//		ps.Play();
		//Debug.Log ("should explooode");
	}

}
