using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

[System.Serializable]
public class Load
{
	public Vector3 offset = new Vector3(0,0,0);
	public Quaternion rotation;
	//GameObject projectile = Instantiate (loader, shotSpawn.position, shotSpawn.rotation) as GameObject;
	public GameObject projectile;


	public Load(GameObject projectile,Quaternion rotation)
	{
		this.projectile = projectile;
		this.rotation = rotation;
	}

	public Load(GameObject projectile,Vector3 offset,Quaternion rotation)
	{
		this.offset = offset;
		this.projectile = projectile;
		this.rotation = rotation;
	}
}

public class PlayerController : MonoBehaviour
{
	public GameObject weaponSlot;
	private List<GameObject> weaponSlots = new List<GameObject>();
	public int numOfWeaponSlots;

	




	private GameObject GameController;

	public float playerHull;
	private float playerMaxHull;
	public float playerShield;
	private float playerMaxShield;

	public float getShield(){
		return playerShield;
	}
	public void addShield(float heal){
		playerShield += heal;
		if (playerShield > playerMaxShield) {
			playerShield = playerMaxShield;}
		
	}
	public float getHull(){
		return playerHull;
	}
	public void addHull(float heal)
	{
		playerHull += heal;
		if (playerHull > playerMaxHull) {
			playerHull = playerMaxShield;}
	}
	public void takeDamage(float damage){
		playerShield -= damage;
		if (playerShield < 0) {
			
			playerHull += playerShield;
			playerShield = 0f;
		}
		if (playerHull <= 0.0f) {
			this.onDeath ();
			this.destroyObject();

		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy") {
			return;
			
		} else if(other.gameObject.tag == "EnemyBullet"){
			Destroy (other.gameObject);
			this.takeDamage (other.gameObject.GetComponent<Rigidbody> ().velocity.magnitude);
		}
	}

	public GameObject deathParticles;
	public void onDeath(){
		Instantiate( deathParticles, this.transform.position, this.transform.rotation); //do death particles
	}

	public void destroyObject()
	{
		Destroy(this.gameObject); ////start game over screen
	}

	public float chargeShotDelay;
	public float fireStormDelay;
	public int fireStormDuration;
	public float rotationSpeed;
	public Load load;
	private bool loaded = false;

	public GameObject fireball1;
	public Vector3 attack1DefaultSize = new Vector3(1F,1F,1F);
	public float attack1DefaultDrag = .1f;
	public int chargeShotNum;
	//public GameObject fireball2;

	public List<Load> loader = new List<Load>();

	public Transform shotSpawn;
	public GameObject fireballSound;
	
	public float fireRate = 0.5F;
	private float nextFire = 0.0F;
	private bool channeling;

	//private Animator anim;

	public void shoot(Load load) 
	{
		Instantiate (load.projectile, shotSpawn.position + load.offset, shotSpawn.rotation * load.rotation);
		fireballSound.GetComponent<AudioSource> ().Play ();
		//anim.SetBool (0, true);
		//return load;
	}

	void Awake(){
	//	anim = GetComponent<Animator> ();
	}

	void Start(){



	GameController = GameObject.Find ("GameController");
		playerMaxHull = playerHull;
		playerMaxShield = playerShield;



	float sectorSize = GameController.GetComponent<GameController> ().sectorSize;
	fireball1.transform.localScale = attack1DefaultSize;
	fireball1.GetComponent<Rigidbody>().drag = attack1DefaultDrag;
	ready = true;
	channeling = false;


		boundary.xMin = - (sectorSize / 2);
		boundary.xMax = (sectorSize / 2);
		boundary.yMin = - (sectorSize / 2);
		boundary.yMax = (sectorSize / 2);
		GameController.GetComponentInChildren<BoxColSetSectorSize> ().setBounds (sectorSize);

		for (int i=0; i<numOfWeaponSlots; i++) {
			GameObject tempSlot = (GameObject)Instantiate (weaponSlot, transform.position, transform.rotation);
			tempSlot.transform.parent = this.transform;
			weaponSlots.Add(tempSlot);
		}
		shotSpawn = weaponSlots [0].transform;  


	}

	IEnumerator fireStorm()
	{
		fireball1.transform.localScale = new Vector3(0.5F,0.5F,0.5F);
		channeling = true;
		ready = false;
		while (true) {
			for(int i=0;i<fireStormDuration;i++){
				fireball1.GetComponent<Rigidbody>().drag = 3.0f;
				
				loader.Add (new Load (fireball1, Quaternion.identity * Quaternion.Euler(0f, 0.0f, Random.Range(-20.0f, 20.0f)) ));
				foreach (Load load in loader) {
					shoot (load); //shoots and removes the loaded object
					
				}
				yield return new WaitForSeconds (.01f);
			}
			break;
		}
		fireball1.transform.localScale = attack1DefaultSize;
		fireball1.GetComponent<Rigidbody>().drag = attack1DefaultDrag;
		channeling = false;
		fireball1.transform.localScale = new Vector3(1F,1F,1F);
		yield return new WaitForSeconds (fireStormDelay);//cooldown before cast again
		ready = true;


	}

	private bool ready;
	//private bool freezeChara;

	void Update()
	{
		if (ready) {
			if (Input.GetButtonDown ("Fire3")) {
				StartCoroutine (fireStorm());
				ready = false;
				
				nextFire = Time.time;
				
			}
		}
		if (!Input.GetButton ("Fire1") && Time.time > nextFire) {
			if (loaded == false) {
				
				fireball1.GetComponent<Rigidbody>().drag = attack1DefaultDrag;
				
				loader.Add (new Load (fireball1, Quaternion.identity * Quaternion.Euler(0f, 0.0f, Random.Range(-20.0f, 20.0f)) ));
				
			}
			
			foreach (Load load in loader) {
				shoot (load); //shoots and removes the loaded object
				
			}
			loader.Clear ();
			loaded = false;//empty magazine
			
			nextFire = Time.time + fireRate;
		} else if (Input.GetButton ("Fire1") && Time.time > nextFire + chargeShotDelay && !loaded) {
			nextFire = Time.time;
			loaded = true;  //locked and loaded
			//				loader.Add (new Load (fireball1, Quaternion.identity * new Quaternion (0f, 0.01f, 0f, 0.1f))); angular change left
			//				loader.Add (new Load (fireball1, Quaternion.identity * new Quaternion (0f, -0.01f, 0f, 0.1f))); angular change right
			for(int i=0;i<chargeShotNum;i++){
				loader.Add (new Load (fireball1, Quaternion.identity));
			}
			
		} else if (Input.GetButton ("Fire1") && Time.time > nextFire && loaded) {
			nextFire = Time.time + fireRate;
			shoot (new Load (fireball1, Quaternion.identity));
		}
		
		
		if ((Input.GetButtonUp ("Fire1") && loaded == true) || Input.GetButtonDown ("Fire3")) { 
			foreach (Load load in loader) {
				shoot (load); //shoots and removes the loaded object
			}
			loader.Clear ();
			loaded = false;//empty magazine
		}

	}


	public float speed;
	
	public Boundary boundary;

	void FixedUpdate ()
	{
		//if (!freezeChara) {

			float moveHorizontal = Input.GetAxis ("HorizontalPlayer");
			float moveVertical = Input.GetAxis ("VerticalPlayer");
		Vector3 movement = new Vector3 (moveHorizontal,moveVertical, 0.0f);
			this.GetComponent<Rigidbody>().velocity = movement * speed;
			
			this.GetComponent<Rigidbody>().position = new Vector3 
				(
					Mathf.Clamp (this.GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
				    Mathf.Clamp (this.GetComponent<Rigidbody>().position.y, boundary.yMin, boundary.yMax), 
					0.0f//Mathf.Clamp (this.GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
					);



		this.GetComponent<Rigidbody>().position = new Vector3 (this.GetComponent<Rigidbody>().position.x, this.GetComponent<Rigidbody>().position.y,0.0f);

			//GetComponent<Rigidbody> ().position = GetComponent<Rigidbody> ().rotation * Quaternion.Euler (0.0f, 0f, moveHorizontal * rotationSpeed);
		}
	//}

	void LateUpdate(){



	}


}