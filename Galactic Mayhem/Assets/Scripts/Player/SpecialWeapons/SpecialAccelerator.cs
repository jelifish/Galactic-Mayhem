using UnityEngine;
using System.Collections;

public class SpecialAccelerator : SpecialWeapon {

	public override void attachAttributes(){
		spawn.AddComponent<AcceleratorInteractable> ();
		spawn.GetComponent<AcceleratorInteractable> ().projectile = this.projectile;
	}
}
