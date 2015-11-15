using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
	public GameObject projectile;
	public float coolDown;
	public float minCD = 5f, maxCD = 7f;
	public PlayerController player;
	public float initialSpeed;

	public virtual void fireAtLocation(){

	}
	public virtual void fireAtTarget(){

	}


}
