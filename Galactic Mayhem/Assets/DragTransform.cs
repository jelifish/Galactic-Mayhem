using System.Collections;
using UnityEngine;

class DragTransform : MonoBehaviour
{
	public bool limitdrag = true;
	private bool finished = false;
	private float distance;
	
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
		//Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		
		//Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint);
		//offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
	
	void OnMouseUp()
	{
		if(limitdrag)finished = true;

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