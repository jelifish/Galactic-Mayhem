using UnityEngine;
using System.Collections;

public class RotateTowardsPlayer : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	public float rotationSpeed = 1f;
	void FixedUpdate () 
	{
		if (player != null) {
//			Vector3 rotationTarget = player.transform.position;//Camera.main.ScreenToWorldPoint (Input.mousePosition);    \
			//Vector3 rotationTarget = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//var _lookRotation = Quaternion.LookRotation ( Vector3.forward, rotationTarget-transform.position);

			var targetRotation = Quaternion.LookRotation (player.transform.position - transform.position, Vector3.down);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);

			//transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
		}

		
		
		//_lookRotation.x = 0.0f;
		//_lookRotation.z = 0.0f;
		

		//		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		////		transform.rotation = Quaternion.LookRotation(Vector3.up, mousePos - transform.position);
		//		to = Quaternion.LookRotation(transform.position - mousePos, Vector3.forward);
		//		Quaternion.Slerp (transform.rotation, to, Time.time * .1f);
	}
}
