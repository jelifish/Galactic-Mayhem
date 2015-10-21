using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour
{
	
	// speed is the rate at which the object will rotate
	public float speed;
	public Quaternion to;
	public float rotationSpeed = 1f;
	void FixedUpdate () 
	{

		Vector3 rotationTarget = Camera.main.ScreenToWorldPoint (Input.mousePosition);                      
		
		var _lookRotation = Quaternion.LookRotation ( Vector3.down, rotationTarget-transform.position);

		
		//_lookRotation.x = 0.0f;
		//_lookRotation.z = 0.0f;
		
		transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
//		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
////		transform.rotation = Quaternion.LookRotation(Vector3.up, mousePos - transform.position);
//		to = Quaternion.LookRotation(transform.position - mousePos, Vector3.forward);
//		Quaternion.Slerp (transform.rotation, to, Time.time * .1f);
	}
}