using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour
{
	
	// speed is the rate at which the object will rotate
	public float speed;
	
	void FixedUpdate () 
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.rotation = Quaternion.LookRotation(Vector3.up, mousePos - transform.position);
	}
}