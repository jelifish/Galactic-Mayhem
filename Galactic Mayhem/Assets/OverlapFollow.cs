using UnityEngine;
using System.Collections;

public class OverlapFollow : MonoBehaviour {

	public GameObject player;
	void Update(){
		this.transform.position = player.transform.position;
	}
}
