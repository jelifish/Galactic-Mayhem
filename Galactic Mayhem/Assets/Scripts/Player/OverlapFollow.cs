using UnityEngine;
using System.Collections;

public class OverlapFollow : MonoBehaviour {

	public GameObject player;
	void Update(){
		if(player != null)this.transform.position = player.transform.position;
	}
}
