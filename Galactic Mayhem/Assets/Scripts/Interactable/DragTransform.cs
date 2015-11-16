using System.Collections;
using UnityEngine;

class DragTransform : MonoBehaviour
{
	public bool limitdrag = true;
	private bool finished = false;
	private float distance;
	public GameObject spawn;
	
	private Vector3 screenPoint;
	//private Vector3 offset;
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
		Interactable[] interArr;
		if (spawn != null) {
			interArr = spawn.GetComponents<Interactable> ();
			foreach (Interactable inter in interArr) {
				inter.mouseUpFire ();
			}
		}
		if(limitdrag){
			Destroy(this.gameObject);
		}

			
		


	}
	void OnMouseDrag()
	{
		if (!finished) {
			Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		
			Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint);// &#43; offset;
			transform.position = curPosition;
		}
	}
}