using UnityEngine;
using System.Collections;

public class RotateTowards : MonoBehaviour {
	public GameObject towardsObject;

	// Use this for initialization
	void Start () {
		//towardsObject = GameObject.FindGameObjectWithTag ("Player");

	}


	public float rotationSpeed = 1f;
	void FixedUpdate () 
	{
		if (towardsObject != null) {
			var offset = new Vector2(towardsObject.transform.position.x - transform.position.x, towardsObject.transform.position.y - transform.position.y);
			var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}

	}
}
