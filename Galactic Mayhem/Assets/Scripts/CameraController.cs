using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Transform Player;

	public Vector3 Margin;
	public Vector3 Smoothing;
	public Camera MainCamera;

	public BoxCollider Bounds;

	//private Vector3 _min,_max;


	public bool IsFollowing(){
		return true;}

	public void Start()
	{
		Player = GameObject.Find("Player").transform;
		//_min = Bounds.bounds.min;
		//_max = Bounds.bounds.max;
	}
	public void FixedUpdate()
	{
		//_min = Bounds.bounds.min; //////////// DEBUG CODE REMOVE IN FINAL VERSION
		//_max = Bounds.bounds.max; ////////////DEBUG CODE REMOVE IN FINAL VERSION
		float x = transform.position.x;
		float z = transform.position.z;

		if (IsFollowing()) {
		if (Mathf.Abs(x-Player.position.x) > Margin.x)
			{x = Mathf.Lerp(x,Player.position.x, Smoothing.x * Time.deltaTime);
			}

			if (Mathf.Abs(z-Player.position.z) > Margin.z)
			{z = Mathf.Lerp(z,Player.position.z, Smoothing.z * Time.deltaTime);
			}
		}

		//float cameraHalfWidth = GetComponent<Camera>().orthographicSize * ((float)Screen.width / Screen.height);
		//x = Mathf.Clamp (x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
		//z = Mathf.Clamp (z, _min.z + GetComponent<Camera>().orthographicSize, _max.z - GetComponent<Camera>().orthographicSize);

		transform.position = new Vector3 (x, transform.position.y, z);


	}
}
