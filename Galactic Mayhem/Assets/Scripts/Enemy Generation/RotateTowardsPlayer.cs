using UnityEngine;
using System.Collections;

public class RotateTowardsPlayer : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	public float rotationSpeed = 1f;
	void FixedUpdate () 
	{
		if (player != null) {
			var offset = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
			var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}

		

	}
}
