using UnityEngine;
using System.Collections;

public class RotateTowardsPlayer : RotateTowards {
	
	void Start () {
		towardsObject = GameObject.FindGameObjectWithTag ("Player");
	}
}
