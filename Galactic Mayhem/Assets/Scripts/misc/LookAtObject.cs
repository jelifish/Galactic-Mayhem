using UnityEngine;
using System.Collections;

public class LookAtObject : MonoBehaviour {
	public float speed;
	public float rotationSpeed = 1f;
	public GameObject targetObject;
	void FixedUpdate () 
	{
		
		var target = targetObject.transform.position;
		//var screenPoint = Camera.main.WorldToScreenPoint (transform.localPosition);
		var offset = new Vector2 (target.x , target.y);
		var angle = Mathf.Atan2 (offset.y, offset.x) * Mathf.Rad2Deg;
		//transform.rotation = Quaternion.Euler(0, 0, angle);
		transform.rotation = Quaternion.Euler (0, 0, angle);
		
		
	}

}
