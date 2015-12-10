using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Transform follow;
	
	public Vector3 Margin;
	public Vector3 Smoothing;
	public Camera MainCamera;
	
	public BoxCollider Bounds;
	
	//private Vector3 _min,_max;
	public void resetFollow (){
		follow = GameObject.Find("Player").transform;
		Smoothing = new Vector3 (8f, 8f, 0);
	}
	public void followObject(Transform obj, bool smooth = true){
		follow = obj;
		Smoothing = new Vector3 (1.5f, 1.5f, 0);
	}
	
	public bool IsFollowing(){
		return true;}
	
	public void Start()
	{
		follow = GameObject.Find("Player").transform;
		//_min = Bounds.bounds.min;
		//_max = Bounds.bounds.max;
	}
	const int orthographicSizeMin = 10;
	const int orthographicSizeMax = 50;
	public void Update(){

		if (Input.GetAxis("Mouse ScrollWheel") < 0) // forward
		{
			Camera.main.orthographicSize+= 3;
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0) // back
		{
			Camera.main.orthographicSize-= 3;
		}
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, orthographicSizeMin, orthographicSizeMax );

	}
	public void FixedUpdate()
	{

		//_min = Bounds.bounds.min; //////////// DEBUG CODE REMOVE IN FINAL VERSION
		//_max = Bounds.bounds.max; ////////////DEBUG CODE REMOVE IN FINAL VERSION
		float x = transform.position.x;
		float y = transform.position.y;
		
		if (IsFollowing()&& follow != null) {
			if (Mathf.Abs(x-follow.position.x) > Margin.x)
			{x = Mathf.Lerp(x,follow.position.x, Smoothing.x * Time.deltaTime);
			}
			
			if (Mathf.Abs(y-follow.position.y) > Margin.y)
			{y = Mathf.Lerp(y,follow.position.y, Smoothing.y * Time.deltaTime);
			}
		}
		
		//float cameraHalfWidth = GetComponent<Camera>().orthographicSize * ((float)Screen.width / Screen.height);
		//x = Mathf.Clamp (x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
		//z = Mathf.Clamp (z, _min.z + GetComponent<Camera>().orthographicSize, _max.z - GetComponent<Camera>().orthographicSize);
		
		transform.position = new Vector3 (x, y, transform.position.z );
		
		
	}
}
