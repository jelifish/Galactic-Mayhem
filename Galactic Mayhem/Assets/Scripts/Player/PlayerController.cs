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
		gc.GetComponent<GameController> ().setBoost(curSpeedBoostDuration);
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
	public void recoverHull(){
		float healAmount = maxHull / 5;
		hull += healAmount;
		if (hull > maxHull) {
			hull = maxHull;
		}
		updateHUD ();
	}
	public void recoverShield(){

		curShieldCooldown = -0.1f;
	}
	public void boostSpeed (){
		if (curSpeedBoostDuration >= 6) {
			curSpeedBoostDuration+=speedBoostDuration/3;
		} else {
			curSpeedBoostDuration += speedBoostDuration;
		}
	}

	public float speedBoostDuration = 3;
	public float curSpeedBoostDuration = 0;

	IEnumerator speedBoost(){
		bool boostTime = false;
		while (true) {
		
			//curSpeedBoostDuration = 3;
			if (curSpeedBoostDuration > 0) {
				if(boostTime == false){
					Time.timeScale *=.6f;
					Time.fixedDeltaTime = 0.02F * Time.timeScale;
					boostTime = true;
				}
				movementSpeedCap = 10;
				armor = maxArmor * 2;
				mitigation = maxMitigation * 2;

				curSpeedBoostDuration -= .1f;
				gc.setBoost(curSpeedBoostDuration);
			}else{
				if(boostTime == true&&curSpeedBoostDuration <= 0){
					Time.timeScale /=.6f;
					Time.fixedDeltaTime = 0.02F * Time.timeScale;
					boostTime = false;
				}
				armor = maxArmor;
				mitigation = maxMitigation;
				movementSpeedCap = 2;
			}
			yield return new WaitForSeconds (.1f);
		}
	}



	public GameObject weaponSlot;
//	private Queue<GameObject> weaponSlots = new Queue<GameObject>();
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

	public void init(){

		StartCoroutine (speedBoost ()); //this controls speedBoost of the player
		//GameController = GameObject.Find ("GameController");
		maxHull = hull;
		maxShield = shield;

		
		//setBoundary (); /////////////////sets bounds for player movement.
		sectorClear = false;
		
//		for (int i=0; i<numOfWeaponSlots; i++) {
//			GameObject tempSlot = (GameObject)Instantiate (weaponSlot, this.GetComponent<Rigidbody>().position, GetComponent<Rigidbody>().rotation);
//			tempSlot.GetComponent<WeaponSystem>().setSlotPosition(i);
//			tempSlot.transform.parent = this.transform;
//			weaponSlots.Enqueue(tempSlot);
//			if(i==0){
//				tempSlot.GetComponent<WeaponSystem>().initWeapon();
//			}
//			if(i==1){
//				tempSlot.GetComponent<WeaponSystem>().initWeapon();
//			}
//			if(i==2){
//				tempSlot.GetComponent<WeaponSystem>().initWeapon();
//			}
//			if(i==3){
//				tempSlot.GetComponent<WeaponSystem>().initWeapon();
//			}
//			if(i==4){
//				tempSlot.GetComponent<WeaponSystem>().initWeapon();
//			}
//		}
		

	}


	
	//public Boundary boundary;
	public bool sectorClear = false;
	public void setMoveDirection(Vector3 move){
		this.GetComponent<Rigidbody>().velocity += move * movementSpeedCap*5;
		this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity.normalized * movementSpeedCap;
	}
	void FixedUpdate ()
	{
		//Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
			
		if (!sectorClear) {
			this.GetComponent<Rigidbody> ().position = new Vector3 
				(
					Mathf.Clamp (this.GetComponent<Rigidbody> ().position.x, gc.GetComponent<GameController>().boundary.xMin, gc.GetComponent<GameController>().boundary.xMax), 
					Mathf.Clamp (this.GetComponent<Rigidbody> ().position.y, gc.GetComponent<GameController>().boundary.yMin, gc.GetComponent<GameController>().boundary.yMax), 
					0.0f
			);
		}

		if (sectorClear) {
			if(this.GetComponent<Rigidbody> ().position.x > gc.GetComponent<GameController>().boundary.xMax+5)
			{
				gc.moveEast();
			}
			if(this.GetComponent<Rigidbody> ().position.x < gc.GetComponent<GameController>().boundary.xMin-5)
			{
				gc.moveWest();
			}
			if(this.GetComponent<Rigidbody> ().position.y > gc.GetComponent<GameController>().boundary.yMax+5)
			{
				gc.moveNorth();
			}
			if(this.GetComponent<Rigidbody> ().position.y < gc.GetComponent<GameController>().boundary.xMin-5)
			{
				gc.moveSouth();
			}
		}


		}

	public override void onDeath(){
		updateHUD ();
		Instantiate( deathParticles, this.transform.position, this.transform.rotation);
		gc.restart ();

	}



}