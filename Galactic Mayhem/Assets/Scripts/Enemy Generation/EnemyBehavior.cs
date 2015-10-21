using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	public float health;
	public float speed;
//	private float damage;
	public float tumble;
	void Start () 
	{
		GetComponent<Rigidbody> ().velocity = new Vector3(Random.Range(-1f,1f),0,Random.Range(-1f,1f)) * speed *(Random.Range(-5f,5f));
		GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble; 
	}
	public void reduceHealth(float damage){
//		this.damage = 15.0f + (damage + speed * 2f);
//		Debug.Log ("damage taken: " + this.damage);
		health -= damage;
	}
	public float getHealth(){
		return health;
	}

	public GameObject deathParticles;
	
	public void onDeath(){
		Instantiate( deathParticles, this.transform.position, this.transform.rotation);
		//		ParticleSystem ps = deathParticles.GetComponent<ParticleSystem>();
		//		ps.enableEmission = true;
		//		ps.Play();
//		Debug.Log ("should explooode");
	}

	// Update is called once per frame
	void Update () 
	{
	
	}

	void fixedUpdate()
	{

	}
}
