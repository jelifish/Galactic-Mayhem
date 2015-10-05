using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	public EnemyBehavior currentObject;
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != "Enemy") {
			currentObject.reduceHealth (other.GetComponent<Rigidbody> ().velocity.magnitude);
			Destroy (other.gameObject);
			if (currentObject.getHealth () <= 0.0f) {
				Destroy (gameObject);
			}
		}
	}

}