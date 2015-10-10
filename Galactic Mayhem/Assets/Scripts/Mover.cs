using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
	public float age;
	public float speed;
	private Transform originalObject;
	public int mirrorRate = 0;
	void Start()
	{
		GetComponent<Rigidbody> ().velocity = transform.forward * speed;
		originalObject = GetComponent<Rigidbody> ().transform;
		Destroy (gameObject, age);
	}
	float velo;
	public GameObject bullet;
	void OnCollisionEnter(Collision other)
	{

		if (other.gameObject.tag == "Bullet" && this.gameObject.tag=="Strike") {
			bullet.tag ="TriggeredBullet";
			//Instantiate(bullet, transform.localPosition,Quaternion.identity);
			bullet.tag ="Bullet";
			other.gameObject.tag = "TriggeredBullet";
		}
	}
	void Update()
	{
//		velo = this.GetComponent<Rigidbody> ().velocity.magnitude;
//		if(velo > 20.0f){
//		Debug.Log(velo);
//		}

		if (mirrorRate >= 5) {
			Destroy (gameObject);
		}
		if (Mathf.Abs (GetComponent<Rigidbody> ().position.x) >= 10) 
		{
			mirrorRate+=1;
			GetComponent<Rigidbody>().position =  Vector3.Reflect(originalObject.position, Vector3.right);
			originalObject =GetComponent<Rigidbody>().transform;
		}

		if (this.GetComponent<Rigidbody>().velocity.magnitude <= .2f) {
			Destroy(gameObject);
		}
	}
	void fixedUpdate(){

	}
}
