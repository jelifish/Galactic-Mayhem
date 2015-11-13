using UnityEngine;
using System.Collections;

public class PlayerDrag : MonoBehaviour {

	public GameObject playerObject;
	public GameObject dragObject;
	private float distance;
	
	private Vector3 screenPoint;
	//private Vector3 offset;
	void OnMouseEnter()
	{
	}
	
	void OnMouseExit()
	{
		//use onmouseup instead
	}
	
	void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		//offset = (gameObject.transform.position) - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
	
	void OnMouseUp()
	{
		if((dragObject.transform.position.x - playerObject.transform.position.x < 1 &&
		    dragObject.transform.position.x - playerObject.transform.position.x > -1)&&
		  (( dragObject.transform.position.y - playerObject.transform.position.y < 1)&&
		   dragObject.transform.position.y - playerObject.transform.position.y > -1)
		   ){
			playerObject.GetComponent<Rigidbody>().velocity = Vector3.zero;


		}else{
		playerObject.GetComponent<Rigidbody>().velocity = (dragObject.transform.position - playerObject.transform.position).normalized * playerObject.GetComponent<PlayerController>().speed;
		}




	}
	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
		
		Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint);// &#43; offset;
		dragObject.transform.position = curPosition;

		playerObject.GetComponent<Rigidbody>().velocity = (dragObject.transform.position - playerObject.transform.position).normalized * playerObject.GetComponent<PlayerController>().speed;
	}
}
