using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	public EnemyBehavior currentObject;
	void OnTriggerEnter(Collider other)
	{
//		if(other.gameObject.tag == "Strike")
//		{
//			Time.timeScale = 1.0f;
//		}
		if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "TriggeredBullet") {
			currentObject.reduceHealth (other.GetComponent<Rigidbody> ().velocity.magnitude);
			Destroy (other.gameObject);
			if (currentObject.getHealth () <= 0.0f) {
				currentObject.GetComponent<OnDeath>().onDeath();
				Destroy (gameObject);
			
			}
		}
	}

}