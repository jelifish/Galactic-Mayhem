using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
	public GameObject projectile;
	public float coolDown;
	public float minCD = 5f, maxCD = 7f;
	public PlayerController player;
	public float initialSpeed;
	protected bool isTimeSlowed = false;

	public virtual void mouseUpFire(){

	}
	public virtual void mouseDownFire(){

	}
	void OnDestroy() {
		if (isTimeSlowed) {
			Time.timeScale += 0.5F;
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
			isTimeSlowed = false;
		}
	}


}
