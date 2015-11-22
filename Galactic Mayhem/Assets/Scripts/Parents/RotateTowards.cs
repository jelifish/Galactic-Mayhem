using UnityEngine;
using System.Collections;

public class RotateTowards : MonoBehaviour {
	public GameObject towardsObject;
	public Vector3 targetVector;


	void FixedUpdate () 
	{
		if (towardsObject != null) {
			var offset = new Vector2(towardsObject.transform.position.x - transform.position.x, towardsObject.transform.position.y - transform.position.y);
			var angleofFire = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
			targetVector = towardsObject.transform.position - transform.position;

			transform.rotation = Quaternion.Euler(0, 0, angleofFire);
		}

	}
}
