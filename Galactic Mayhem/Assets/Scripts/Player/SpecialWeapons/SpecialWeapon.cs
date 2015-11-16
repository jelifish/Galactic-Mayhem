using UnityEngine;
using System.Collections;

public class SpecialWeapon : Weapon{
	//public GameObject weaponSlot; 
	public bool activated = false;
	public GameObject projectile;
	public float coolDown;
	public float minCD = 5f, maxCD = 7f;
	public PlayerController player;
	
	public GameObject interactable;
	public virtual void Fire(Vector3 firePosition){

	}

	IEnumerator interactableCreator()
	{
		yield return new WaitForSeconds (Random.Range(0f,2f));
		while(true){
			yield return new WaitForSeconds (coolDown);
			spawnInteractable();

		}
	}




	protected GameObject spawn;
	public void spawnInteractable(){
		genXY ();
		spawn = (GameObject)Instantiate (interactable, new Vector3 (x, y, 0f), Quaternion.identity);
		attachAttributes ();
	}

	public virtual void attachAttributes(){
	
	}
	private float x,y;
	private void genXY(){
		float playerX = player.transform.position.x;
		float playerY = player.transform.position.y;
		x = Random.Range (playerX - 10, playerX + 10);
		y = Random.Range (playerY - 10, playerY + 10);
		if (x < playerX + 3 && x > playerX - 3 && y < playerY + 3 && y > playerY - 3) {
			genXY ();
		}
	}


	void Start () {
		coolDown = Random.Range (minCD, maxCD);
		if (GameObject.FindWithTag ("Player") != null) {
			player = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();
		}else{ Debug.LogWarning("Cannot Find Player");
		}

		if (GetComponentInParent<WeaponScript>() != null) {
			weaponSlot = GetComponentInParent<WeaponScript> ().weaponSlot;
			//slotPosition = weaponSlot.GetComponent<WeaponSystem> ().getSlotPosition ()+1;

		}
		if (activated) {
			StartCoroutine (interactableCreator ());
		}
		//Debug.Log (fireButton); //"Fire1"
	}


}
