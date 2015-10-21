using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour
{
	
	// speed is the rate at which the object will rotate
	public float speed;
	public float rotationSpeed = 1f;
	void FixedUpdate () 
	{

		Vector3 rotationTarget = Camera.main.ScreenToWorldPoint (Input.mousePosition);                      
		
		//var _lookRotation = Quaternion.LookRotation ( Vector3.back, rotationTarget-transform.position);
		//_lookRotation *= Quaternion.Euler(0,180,0);
		
		//_lookRotation.x = 0.0f;
		//_lookRotation.z = 0.0f;
		
		//transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
//		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
////		transform.rotation = Quaternion.LookRotation(Vector3.up, mousePos - transform.position);
//		to = Quaternion.LookRotation(transform.position - mousePos, Vector3.forward);
//		Quaternion.Slerp (transform.rotation, to, Time.time * .1f);


		Vector3 vectorToTarget = rotationTarget - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);









	}
}