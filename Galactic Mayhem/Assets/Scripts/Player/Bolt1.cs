using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bolt1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public BlasterAttributes blaster;


	public void init(BlasterAttributes blaster)
	{
		this.blaster = blaster;
	}

}
