using UnityEngine;
using System.Collections;

public class BlasterModule : MonoBehaviour {
	public Transform aim;
	public Load load;
	
	public void shoot(Load load) 
	{
		Instantiate (load.projectile, aim.position + load.offset, aim.rotation * load.rotation);
		//.	fireballSound.GetComponent<AudioSource> ().Play ();
		//anim.SetBool (0, true);
		//return load;
	}

}
