using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
	//these variables are also passed down to the spawn
	public SkillType skillType = SkillType.MaterialType;
	public float coolDown = 999f;
	public string skillName = "Name";

	public GameObject projectile;
	public float minCD = 5f, maxCD = 7f;
	public PlayerController player;
	public float initialSpeed = 1f;
	protected bool isTimeSlowed = false;
	public Vector3 targetPosition = Vector3.zero;
	public float timeMulti = 2f;
	void Awake(){
		projectile = Resources.Load ("Projectiles/Bolt")as GameObject;
	}

	public virtual void timeSlow(){
		if (!isTimeSlowed) {	
			Timef.f.SlowTime(timeMulti);
			isTimeSlowed = true;
		}
	}
	public virtual void timeResume(){
		if (isTimeSlowed) {
			Timef.f.SpeedTime(timeMulti);
			isTimeSlowed = false;
		}
	}
	public virtual void OnDestroy() {
		timeResume ();
        Debug.Log(skillName);
        SpawnPool.pool.executeSpawn(this.gameObject);
	}
	public virtual void mouseUpFire(){

	}
	public virtual void mouseDownFire(){

	}


}
