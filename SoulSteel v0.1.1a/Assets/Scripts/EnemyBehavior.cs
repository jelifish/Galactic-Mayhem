using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	public float health;
	public float speed;
	private float damage;
	public float tumble;
	void Start () 
	{
		GetComponent<Rigidbody> ().velocity = transform.forward * -speed;
		GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble; 
	}
	public void reduceHealth(float damage){
		this.damage = 15.0f + (damage + speed * 2f);
		Debug.Log ("damage taken: " + this.damage);
		health -= damage;
	}
	public float getHealth(){
		return health;
	}
	// Update is called once per frame
	void Update () 
	{
	
	}

	void fixedUpdate()
	{

	}
}
