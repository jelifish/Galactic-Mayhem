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

			//var targetRotation = Quaternion.LookRotation (player.transform.position - transform.position, Vector3.forward);
//			var angle = Mathf.Atan2(player.transform.position.y, player.transform.position.x) * Mathf.Rad2Deg;
//			transform.eulerAngles = new Vector3(0,0,angle);
//			var mouse = Input.mousePosition;
			var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
			var offset = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
			var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0, 0, angle);
			//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 2.0f);
			//transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);

//			var dir = WorldPos - transform.position;
//			var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
//			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


		}

		
		
		//_lookRotation.x = 0.0f;
		//_lookRotation.z = 0.0f;
		

		//		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		////		transform.rotation = Quaternion.LookRotation(Vector3.up, mousePos - transform.position);
		//		to = Quaternion.LookRotation(transform.position - mousePos, Vector3.forward);
		//		Quaternion.Slerp (transform.rotation, to, Time.time * .1f);
	}
}
