using UnityEngine;
using System.Collections;

public class WeaponSystem : MonoBehaviour {

	[System.Serializable]
	public class BlasterAttributes
	{
		public float projectileSize; //between .5 and 2 or 3
		public float initialSpeed; //
		public float rateOfFire; // between 1 and .001 left tilt random
		public float fireAngleVariance; //between 0 and 45 normalized random (22.5 for v shape)
		public float directionOfFire; // should be converted into quaternion
		public float projectileAge; //between 2 and 10 normalized random

		//public bool projectileTrajectory1; //0straight, 1sin wave, 2triangle wave, 3curved
		//public bool projectileTrajectory2l //0normal, 1alternating speed, random
		//public bool semiAuto;
		//public float semiAutoTime; //pause time between attacks
		
		public int specialAtkType; //0.fire fast and strong, 1.spray short ranged, 2.piercing snipe, 3.Strike
	}

	public BlasterAttributes generateBlaster(){
		return null;
	}





}
