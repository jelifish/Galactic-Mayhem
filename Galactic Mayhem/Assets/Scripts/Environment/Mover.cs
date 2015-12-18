using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
	public float age;
	public float speed;
	private Transform originalObject;
	public int mirrorRate = 0;
	public float totalSectorSize;
	void Start()
	{
		//GetComponent<Rigidbody>().velocity =transform.right.normalized*speed;
		GetComponent<Rigidbody>().AddForce(transform.right * speed*5);


		originalObject = GetComponent<Rigidbody> ().transform;
		Destroy (gameObject, age);

		totalSectorSize = GameObject.Find ("GameController").GetComponentInChildren<BoxColSetSectorSize> ().getTotalSectorSize ()/1.95f; //expensive call
	}


	void FixedUpdate()
	{
		if (mirrorRate >= 10) {
			Destroy (gameObject);
		}

		if (this.gameObject.tag != "EnemyBullet") { ///dont port if enemy bullet. 

			//port object into other side of screen (horizontal)
			if (Mathf.Abs (GetComponent<Rigidbody> ().position.x) >= totalSectorSize) {
				mirrorRate += 1;
				GetComponent<Rigidbody> ().position = Vector3.Reflect (originalObject.position, Vector3.right);
				originalObject = GetComponent<Rigidbody> ().transform;
			}
			//port object into other side of screen (vertical)
			if (Mathf.Abs (GetComponent<Rigidbody> ().position.y) >= totalSectorSize) {
				mirrorRate += 1;
				GetComponent<Rigidbody> ().position = Vector3.Reflect (originalObject.position, Vector3.up);
				originalObject = GetComponent<Rigidbody> ().transform;
			}
		}


	}
}
