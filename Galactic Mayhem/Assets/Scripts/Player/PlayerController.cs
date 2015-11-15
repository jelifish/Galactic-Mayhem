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
	public float speed;
	public GameObject sound = GameObject.Find("BoltSound");
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
	public Load(GameObject projectile,Vector3 offset,Quaternion rotation, float speed)
	{
		this.offset = offset;
		this.projectile = projectile;
		this.rotation = rotation;
		this.speed = speed;
	}
}

public class PlayerController : CollisionObject
{
	public override void ShieldUpdate(){
		updateHUD ();
	}
	public void updateHUD(){
		if (hull <= 0) {
			gc.GetComponent<GameController> ().setHealth (0f, maxHull);
		} else {
			gc.GetComponent<GameController> ().setHealth (hull, maxHull);
		}
		gc.GetComponent<GameController> ().setShield (shield, maxShield);
	}
	public override void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy") {
			updateHUD();
		} else if(other.gameObject.tag == "EnemyBullet"){
			takeDamage (other.gameObject.GetComponent<Rigidbody> ().velocity.magnitude*2);
			Destroy(other.gameObject);
			updateHUD();

		}else if(other.gameObject.tag == "Player"){

		}else if(other.gameObject.tag == "Bullet"||other.gameObject.tag == "TriggeredBullet"){

		}
	}





	public GameObject weaponSlot;
	private List<GameObject> weaponSlots = new List<GameObject>();
	public int numOfWeaponSlots;


	//private GameObject GameController;

	public float chargeShotDelay;
	public float fireStormDelay;
	public int fireStormDuration;
	public float rotationSpeed;
	public Load load;

	public Vector3 attack1DefaultSize = new Vector3(1F,1F,1F);
	public float attack1DefaultDrag = .1f;
	public int chargeShotNum;

	public List<Load> loader = new List<Load>();
	
//	public void setBoundary(){
//		float sectorSize = GameController.GetComponent<GameController> ().sectorSize;
//		boundary.xMin = - (sectorSize / 2);
//		boundary.xMax = (sectorSize / 2);
//		boundary.yMin = - (sectorSize / 2);
//		boundary.yMax = (sectorSize / 2);
//		GameController.GetComponentInChildren<BoxColSetSectorSize> ().setBounds (sectorSize);
//	}
//	public void newSector(){
//		setBoundary ();
//		sectorClear = false;
//
//	}

	public void init(){
		
		
		//GameController = GameObject.Find ("GameController");
		maxHull = hull;
		maxShield = shield;

		
		//setBoundary (); /////////////////sets bounds for player movement.
		sectorClear = false;
		
		for (int i=0; i<numOfWeaponSlots; i++) {
			GameObject tempSlot = (GameObject)Instantiate (weaponSlot, this.GetComponent<Rigidbody>().position, GetComponent<Rigidbody>().rotation);
			tempSlot.GetComponent<WeaponSystem>().setSlotPosition(i);
			tempSlot.transform.parent = this.transform;
			weaponSlots.Add(tempSlot);
			if(i==0){
				tempSlot.GetComponent<WeaponSystem>().initWeapon();
			}
			if(i==1){
				tempSlot.GetComponent<WeaponSystem>().initWeapon();
			}
			if(i==2){
				tempSlot.GetComponent<WeaponSystem>().initWeapon();
			}
			if(i==3){
				tempSlot.GetComponent<WeaponSystem>().initWeapon();
			}
		}
		

	}





	
	//public Boundary boundary;
	public bool sectorClear = false;
	public void setMoveDirection(Vector3 move){
		this.GetComponent<Rigidbody>().velocity += move * movementSpeedCap*5;
		this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity.normalized * movementSpeedCap;
	}
	void FixedUpdate ()
	{

			
		if (!sectorClear) {
			this.GetComponent<Rigidbody> ().position = new Vector3 
				(
					Mathf.Clamp (this.GetComponent<Rigidbody> ().position.x, gc.GetComponent<GameController>().boundary.xMin, gc.GetComponent<GameController>().boundary.xMax), 
					Mathf.Clamp (this.GetComponent<Rigidbody> ().position.y, gc.GetComponent<GameController>().boundary.yMin, gc.GetComponent<GameController>().boundary.yMax), 
					0.0f//Mathf.Clamp (this.GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
			);
		}

		if (sectorClear) {
			if(this.GetComponent<Rigidbody> ().position.x > gc.GetComponent<GameController>().boundary.xMax+5)
			{
				gc.moveEast();
				Debug.Log("est");
				//sectorClear = false;
			}
			if(this.GetComponent<Rigidbody> ().position.x < gc.GetComponent<GameController>().boundary.xMin-5)
			{
				gc.moveWest();Debug.Log("est");
				//sectorClear = false;
			}
			if(this.GetComponent<Rigidbody> ().position.y > gc.GetComponent<GameController>().boundary.yMax+5)
			{
				gc.moveNorth();Debug.Log("ntrt");
				//sectorClear = false;
			}
			if(this.GetComponent<Rigidbody> ().position.y < gc.GetComponent<GameController>().boundary.xMin-5)
			{
				gc.moveSouth();Debug.Log("sth");
				//sectorClear = false;
			}
		}


		}
	public override void onDeath(){
		updateHUD ();
		Instantiate( deathParticles, this.transform.position, this.transform.rotation);

	}

}