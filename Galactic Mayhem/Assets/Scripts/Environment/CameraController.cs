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
		float y = transform.position.y;

		if (IsFollowing()&& Player != null) {
		if (Mathf.Abs(x-Player.position.x) > Margin.x)
			{x = Mathf.Lerp(x,Player.position.x, Smoothing.x * Time.deltaTime);
			}

			if (Mathf.Abs(y-Player.position.y) > Margin.y)
			{y = Mathf.Lerp(y,Player.position.y, Smoothing.y * Time.deltaTime);
			}
		}

		//float cameraHalfWidth = GetComponent<Camera>().orthographicSize * ((float)Screen.width / Screen.height);
		//x = Mathf.Clamp (x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
		//z = Mathf.Clamp (z, _min.z + GetComponent<Camera>().orthographicSize, _max.z - GetComponent<Camera>().orthographicSize);

		transform.position = new Vector3 (x, y, transform.position.z );


	}
}
