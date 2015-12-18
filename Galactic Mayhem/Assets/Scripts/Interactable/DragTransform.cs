using System.Collections;
using UnityEngine;

class DragTransform : MonoBehaviour
{
	public bool limitdrag = true;
	private bool finished = false;
	private float distance;
	public GameObject spawn;
	public Transform spawnTransform;
	
	private Vector3 screenPoint;
	public CameraController mainCamera;
	//private Vector3 offset;
	void Start(){
		if (spawn != null) {
			spawnTransform = spawn.transform;
		}
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraController> ();
	}
	void OnMouseEnter()
	{

	}
	
	void OnMouseExit()
	{
	}
	
	void OnMouseDown()
	{

		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		Interactable[] interArr;
		if (spawn != null) {
				interArr = spawn.GetComponents<Interactable> ();
				foreach (Interactable inter in interArr) {
				inter.mouseDownFire ();
				}
			}
	}
	
	void OnMouseUp()
	{
		mainCamera.resetFollow(); //reset camera view back to player
		Interactable[] interArr;
		if (spawn != null) {
			interArr = spawn.GetComponents<Interactable> ();
			foreach (Interactable inter in interArr) {
				inter.targetPosition = this.transform.position;
				inter.mouseUpFire ();
			}
		}
		if(limitdrag){
			Destroy(this.gameObject);
		}

			
		


	}
	private bool setFollow;
	void OnMouseDrag()
	{
		if (spawnTransform!=null && Vector3.Distance (transform.position, spawnTransform.position) > 7 && !setFollow) {
			mainCamera.followObject (this.transform); //set camera view to this player
			setFollow = true;

		}
		if (!finished) {
			Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		
			Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint);// &#43; offset;
			transform.position = curPosition;
		}
	}
}