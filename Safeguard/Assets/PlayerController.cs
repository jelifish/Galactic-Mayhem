using UnityEngine;
using System.Collections;

public class PlayerController : Thing {
	float speed = 15.0f;
	
	void FixedUpdate() {
		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		transform.position += move * speed * Time.deltaTime;
	}

}
