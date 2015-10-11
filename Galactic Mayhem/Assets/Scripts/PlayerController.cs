using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
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
	private GameObject GameController;

	public float chargeShotDelay;
	public float fireStormDelay;
	public int fireStormDuration;
	public float rotationSpeed;
	public Load load;
	bool loaded = false;

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


	float sectorSize = GameController.GetComponent<GameController> ().sectorSize;
	fireball1.transform.localScale = attack1DefaultSize;
	fireball1.GetComponent<Rigidbody>().drag = attack1DefaultDrag;
	ready = true;
	//freezeChara = false;


		boundary.xMin = - (sectorSize / 2);
		boundary.xMax = (sectorSize / 2);
		boundary.zMin = - (sectorSize / 2);
		boundary.zMax = (sectorSize / 2);
		GameController.GetComponentInChildren<BoxColSetSectorSize> ().setBounds (sectorSize);

	}

	IEnumerator fireStorm()
	{
		fireball1.transform.localScale = new Vector3(0.5F,0.5F,0.5F);
		//freezeChara = true;
		ready = false;
		while (true) {
			for(int i=0;i<fireStormDuration;i++){
				fireball1.GetComponent<Rigidbody>().drag = 1.0f;
				shoot(new Load (fireball1, Quaternion.identity));
				yield return new WaitForSeconds (.003f);
			}
			break;
		}
		fireball1.transform.localScale = attack1DefaultSize;
		fireball1.GetComponent<Rigidbody>().drag = attack1DefaultDrag;
		//freezeChara = false;
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
				
				loader.Add (new Load (fireball1, Quaternion.identity * Quaternion.Euler(0.0f, Random.Range(-20.0f, 20.0f), 0.0f) ));
				
			}
			
			foreach (Load load in loader) {
				shoot (load); //shoots and removes the loaded object
				
			}
			loader.Clear ();
			loaded = false;//empty magazine
			
			nextFire = Time.time + fireRate;
		} else if (Input.GetButton ("Fire1") && Time.time > nextFire + chargeShotDelay && loaded == false) {
			nextFire = Time.time;
			loaded = true;  //locked and loaded
			//				loader.Add (new Load (fireball1, Quaternion.identity * new Quaternion (0f, 0.01f, 0f, 0.1f))); angular change left
			//				loader.Add (new Load (fireball1, Quaternion.identity * new Quaternion (0f, -0.01f, 0f, 0.1f))); angular change right
			for(int i=0;i<chargeShotNum;i++){
				loader.Add (new Load (fireball1, Quaternion.identity));
			}
			
		} else if (Input.GetButton ("Fire1") && Time.time > nextFire && loaded == true) {
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
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			this.GetComponent<Rigidbody>().velocity = movement * speed;
			
			this.GetComponent<Rigidbody>().position = new Vector3 
				(
					Mathf.Clamp (this.GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
					0.0f, 
					Mathf.Clamp (this.GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
					);



		this.GetComponent<Rigidbody>().position = new Vector3 (this.GetComponent<Rigidbody>().position.x, 0.0f, this.GetComponent<Rigidbody>().position.z);

			//GetComponent<Rigidbody> ().position = GetComponent<Rigidbody> ().rotation * Quaternion.Euler (0.0f, 0f, moveHorizontal * rotationSpeed);
		}
	//}

	void LateUpdate(){



	}


}