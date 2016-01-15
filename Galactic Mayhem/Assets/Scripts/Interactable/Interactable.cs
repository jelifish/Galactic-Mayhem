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
        if (this.skillType == SkillType.MaterialType)
        {

        }
        else if (this.skillType == SkillType.ControlType)
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(247 / 255.0F, 216 / 255.0F, 66 / 255.0F, 255f));

        }
        else if (this.skillType == SkillType.GuardType)
        {

        }
        else if (this.skillType == SkillType.AdvanceType)
        {
        }
        else if (this.skillType == SkillType.AuraType)
        {
        }
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
    public virtual void OnDestroy()
    {
        timeResume();
        Debug.Log(skillName);
        SpawnPool.pool.executeSpawn(this.gameObject);
    }
	public virtual void mouseUpFire(){

	}
	public virtual void mouseDownFire(){

	}


}
