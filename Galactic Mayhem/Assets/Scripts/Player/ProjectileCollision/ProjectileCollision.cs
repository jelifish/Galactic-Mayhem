using UnityEngine;
using System.Collections;

public class ProjectileCollision : MonoBehaviour {
//	public float shield;
//	[HideInInspector] public float maxShield;
	public float hull;
	[HideInInspector] public float maxHull;

	public bool killed;
	public GameObject deathParticles;

	public void takeDamage(float damage){
		//Debug.Log (damage);
//		shield -= damage;
//		if (shield < 0) {
//			if(armor > (shield*-1))
//			{
//			}else{
//				hull += shield+armor;}
//			shield = 0f;
//		}
		hull -= damage;///added line
		if (hull <= 0.0f) {
			if(!killed){
//				onDeath ();
//				destroyObject();
				ObjectPool.pool.DestroyObject (this.gameObject);
				killed = true;
			}
		}
	}
//	public virtual void onDeath(){
//		
//		//Instantiate( deathParticles, this.transform.position, this.transform.rotation);
//	}
//	public virtual void destroyObject()
//	{
//		//Destroy(this.gameObject);
//		ObjectPool.pool.DestroyObject (this.gameObject);
//
//	}

	private Transform originalObject;
	public int mirrorRate = 0;
	public float totalSectorSize;

	void Start()
	{
		//GetComponent<Rigidbody>().velocity =transform.right.normalized*speed;
		//GetComponent<Rigidbody>().AddForce(transform.right * speed*5);
		
		
		originalObject = GetComponent<Rigidbody> ().transform;


		SetSectorSize ();
		sizeSet = true;
	}
	public bool sizeSet;
	public void SetSectorSize(float size){
			totalSectorSize = size / 1.95f; //non expensive call
		sizeSet = true;
	}
	public void SetSectorSize(){
		if (sizeSet == false) {
			totalSectorSize = GameObject.Find ("GameController").GetComponentInChildren<BoxColSetSectorSize> ().getTotalSectorSize () / 1.95f; //expensive call
		}
		sizeSet = true;
	}
    public virtual void OnTriggerEnter(Collider other)
    {
        if (this.GetComponent<Rigidbody>().velocity.magnitude<1&& other.GetComponent<Rigidbody>().velocity.magnitude < 1 && (other.gameObject.tag == "Bullet" || other.gameObject.tag == "TriggeredBullet"))
        {
            Debug.Log("trigger");
            //float bulletScale = other.transform.localScale.x;
            //float x = bulletScale + .9f;

            //float bulletMulti = x * x;
            //takeDamage(other.gameObject.GetComponent<Rigidbody>().velocity.magnitude * bulletMulti);
            //other.gameObject.GetComponent<ProjectileCollision>().takeDamage(1);
        }
    }
    void FixedUpdate()
	{
		if (mirrorRate >= 10) {
			mirrorRate = 0;

			this.gameObject.SetActive(false);
			//this.gameObject.transform.position = Vector3.zero;

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
