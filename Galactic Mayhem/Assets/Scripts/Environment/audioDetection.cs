using UnityEngine;
using System.Collections;

public class audioDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	if (FindObjectOfType<AudioListener>() == null) {
			this.gameObject.AddComponent<AudioListener>();}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
