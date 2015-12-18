using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//=== Skill Functions Class ===============================

public class Skillf : MonoBehaviour{
	public static Skillf f = null;
	public static float globalForce = 500;
	public static float lowForce = globalForce * 0.2f;
	public static float medForce = globalForce * 0.5f;
	public static float highForce = globalForce * 1.0f;
	void Awake ()
	{
		f = this;
	}
	public void ForceTowardsPoint (List<GameObject> objs,Vector3 towards,float force = 250)
	{
		foreach(GameObject obj in objs){
			if(obj !=null){
				Vector3 targetVector = (towards - obj.transform.position).normalized;
				obj.GetComponent<Rigidbody>().AddForce(targetVector * force * (1 / Time.timeScale));
			}
		}
	}
	public void ForceFromPoint ( )
	{
		
	}
	public void implosion ( )
	{

	}
	public void explosion ( )
	{
		
	}
	
}