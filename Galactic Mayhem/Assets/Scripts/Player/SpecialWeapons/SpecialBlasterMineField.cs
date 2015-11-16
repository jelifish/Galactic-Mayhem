using UnityEngine;
using System.Collections;

public class SpecialBlasterMineField : SpecialWeapon{

	public override void attachAttributes(){
		spawn.AddComponent<BlasterMineFieldInteractable> ();
		spawn.GetComponent<BlasterMineFieldInteractable> ().projectile = this.projectile;
	}

}
