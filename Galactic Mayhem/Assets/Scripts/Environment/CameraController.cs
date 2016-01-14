using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Transform follow;

	public Vector3 Margin;
	public Vector3 Smoothing;
	public Camera MainCamera;
    public Transform player;

	public BoxCollider Bounds;

	//private Vector3 _min,_max;
	public void resetFollow (){
        CancelInvoke();
        follow = player;
		Smoothing = new Vector3 (.2f, .2f, 0);
        Invoke("finalReset", 2);
	}
    public void finalReset() {
        follow = player;
        Smoothing = new Vector3(4f, 4f, 0);

    }
    public void followObject(Transform obj, bool smooth = true){
        CancelInvoke();
		follow = obj;
		Smoothing = new Vector3 (1.5f, 1.5f, 0);
	}

	public bool IsFollowing(){
		return true;}

	public void Start()
	{
      
        player = GameObject.Find("Player").transform;
        follow = player;
        //_min = Bounds.bounds.min;
        //_max = Bounds.bounds.max;
    }
	public void Update()
	{
		//_min = Bounds.bounds.min; //////////// DEBUG CODE REMOVE IN FINAL VERSION
		//_max = Bounds.bounds.max; ////////////DEBUG CODE REMOVE IN FINAL VERSION
		float x = transform.position.x;
		float y = transform.position.y;

		if (IsFollowing()&& follow != null) {
			if (Mathf.Abs(x-follow.position.x) > Margin.x)
			{
				x = Mathf.Lerp(x,follow.position.x, Smoothing.x * Time.smoothDeltaTime);
				//x = follow.position.x;
			}

			if (Mathf.Abs(y-follow.position.y) > Margin.y)
			{
				y = Mathf.Lerp(y,follow.position.y, Smoothing.y * Time.smoothDeltaTime);
				//y = follow.position.y;
			}
		}

		//float cameraHalfWidth = GetComponent<Camera>().orthographicSize * ((float)Screen.width / Screen.height);
		//x = Mathf.Clamp (x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
		//z = Mathf.Clamp (z, _min.z + GetComponent<Camera>().orthographicSize, _max.z - GetComponent<Camera>().orthographicSize);

		transform.position = new Vector3 (x, y, transform.position.z );


	}
}
