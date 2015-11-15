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
		
			Debug.Log (interArr.Length);
//		if ((transform.position.x - spawn.transform.position.x < 1 &&
//		     transform.position.x - spawn.transform.position.x > -1) &&
//		    ((transform.position.y - spawn.transform.position.y < 1) &&
//		 transform.position.y - spawn.transform.position.y > -1)
//		    ) {
			//spawn.GetComponent<Rigidbody> ().velocity = Vector3.zero;
//			foreach (Interactable inter in interArr) {
//				Debug.Log("sharting");
//				inter.fireAtLocation();
//				//Destroy(this);
//			}
			
			
			//} else {
			
			foreach (Interactable inter in interArr) {
				Debug.Log ("shooting");
				inter.fireAtTarget ();

			}
			
			
			
			
			
			
			//Vector3 dragVector = transform.position - spawn.transform.position;
			
			
			//Debug.Log(dragVector);
			//playerObject.GetComponent<Rigidbody> ().velocity = dragVector.normalized * playerObject.GetComponent<PlayerController> ().movementSpeedCap * 1.5f;
			//}
			//Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		
			//Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint);
			//offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}
	}
	
	void OnMouseUp()
	{

		if(limitdrag)Destroy(this.gameObject);;

			
		


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