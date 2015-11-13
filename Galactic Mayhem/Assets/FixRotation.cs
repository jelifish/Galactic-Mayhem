using UnityEngine;
using System.Collections;

public class FixRotation : MonoBehaviour {

	void LateUpdate() {
		transform.rotation=Quaternion.identity;
	}
}
