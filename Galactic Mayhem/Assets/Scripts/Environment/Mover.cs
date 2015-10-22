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
		//float alpha = -Mathf.Tan(this.transform.rotation.z * Mathf.Deg2Rad);
		//Debug.Log (transform.up);
		//GetComponent<Rigidbody> ().velocity = speed * Vector3.up;
		GetComponent<Rigidbody>().AddForce(transform.right* speed);
		originalObject = GetComponent<Rigidbody> ().transform;
		Destroy (gameObject, age);

		totalSectorSize = GameObject.Find ("GameController").GetComponentInChildren<BoxColSetSectorSize> ().getTotalSectorSize ()/1.95f; //expensive call
	}
	float velo;
	public GameObject bullet;
	public void BuildSector(){
		totalSectorSize = GameObject.Find ("GameController").GetComponentInChildren<BoxColSetSectorSize> ().getTotalSectorSize (); //expensive call
	}
	//
	void OnCollisionEnter(Collision other)
	{

		if (other.gameObject.tag == "Bullet" && this.gameObject.tag=="Strike") {
			bullet.tag ="TriggeredBullet";
			if(Random.Range(1,3)==1)Instantiate(bullet, transform.localPosition,Quaternion.identity);
			bullet.tag ="Bullet";
			other.gameObject.tag = "TriggeredBullet";
			other.gameObject.GetComponent<Rigidbody>().drag = (other.gameObject.GetComponent<Rigidbody>().drag /Random.Range(2f, 3f));
		}
	


	}
	void Update()
	{
//		velo = this.GetComponent<Rigidbody> ().velocity.magnitude;
//		if(velo > 20.0f){
//		Debug.Log(velo);
//		}
		//remove glitched objects
		if (mirrorRate >= 5) {
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
	void fixedUpdate(){

	}
}
