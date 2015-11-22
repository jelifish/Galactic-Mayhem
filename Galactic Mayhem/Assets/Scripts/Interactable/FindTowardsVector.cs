using UnityEngine;
using System.Collections;

public class FindTowardsVector : MonoBehaviour {
	public GameObject towardsObject;
	public Vector3 targetVector;

	// Update is called once per frame
	void Update () {
		if (towardsObject != null) {
			targetVector = (towardsObject.transform.position - transform.position).normalized;
		}
	}
}
