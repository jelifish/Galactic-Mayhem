using UnityEngine;
using System.Collections;

public class PlayerDrag : MonoBehaviour {

	public GameObject playerObject;
	public GameObject dragObject;
	private float distance;
	//private Vector3 screenPoint;
	//private Vector3 offset;
	void OnMouseDown()
	{
		//screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		//offset = (gameObject.transform.position) - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
	
	void OnMouseUp()
	{
		if (playerObject != null) {
			if (Vector2.Distance(playerObject.transform.position,dragObject.transform.position)<=1
		   ) {
				playerObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;


			} else {
				Vector3 dragVector = dragObject.transform.position - playerObject.transform.position;

//				if (dragVector.x>playerObject.GetComponent<PlayerController> ().movementSpeedCap)
//				{
//					dragVector.x = playerObject.GetComponent<PlayerController> ().movementSpeedCap;
//				}
//				if (dragVector.x<-playerObject.GetComponent<PlayerController> ().movementSpeedCap)
//				{
//					dragVector.x = -playerObject.GetComponent<PlayerController> ().movementSpeedCap;
//				}
//				if (dragVector.y>playerObject.GetComponent<PlayerController> ().movementSpeedCap)
//				{
//					dragVector.y = playerObject.GetComponent<PlayerController> ().movementSpeedCap;
//				}
//				if (dragVector.y<-playerObject.GetComponent<PlayerController> ().movementSpeedCap)
//				{
//					dragVector.y = -playerObject.GetComponent<PlayerController> ().movementSpeedCap;
//				}
				//Debug.Log(dragVector);
				playerObject.GetComponent<Rigidbody> ().velocity = dragVector.normalized * playerObject.GetComponent<PlayerController> ().movementSpeedCap * 1.5f;
			}

		}


	}
	Vector3 curScreenPoint;
	Vector3 curPosition;
	Vector3 playerPosition;



	void OnMouseDrag()
	{
		if (playerObject != null) {
			
			curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 1);
			curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint);// &#43; offset;
			curPosition.z = -2f;
			//Debug.Log(curPosition);

			playerPosition = playerObject.transform.position;
			if(Vector2.Distance(curPosition,playerPosition) >= 7){
				dragObject.transform.position = playerPosition+((curPosition - playerPosition).normalized*7);
			}
			else{
				dragObject.transform.position = curPosition;
			}


			playerObject.GetComponent<Rigidbody> ().velocity = (dragObject.transform.position - playerObject.transform.position).normalized * playerObject.GetComponent<PlayerController> ().movementSpeedCap * 3;
	
		}
	}
}
