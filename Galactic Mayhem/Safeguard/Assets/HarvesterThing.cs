using UnityEngine;
using System.Collections;

public class HarvesterThing : Thing {

	public GameObject towardsObject;

	public void findNearestPixeroid()
	{
		GameObject closest = null;
		//foreach (GameObject pix in gc.pixeroids) {
			//if(closest == null){closest = pixeroid;}
//			else{
//				if(getDistance(this.transform.gameObject, pixeroid) < getDistance(this.transform.gameObject, closest))
//				{
//					closest = pixeroid;
//				}
//			}
		//}

		towardsObject = closest;

		//gc.pixeroids
	}

	private float getDistance(GameObject obj1, GameObject obj2)
	{
		return Vector3.Distance(obj1.transform.position, obj2.transform.position);
	}

	void Start(){
		StartCoroutine (slowUpdate ());

	}

	public Vector3 targetVector;
	void FixedUpdate(){
		if (towardsObject != null) {
		
			targetVector = (towardsObject.transform.position - transform.position).normalized;
			this.GetComponent<Rigidbody> ().AddForce (targetVector * 2);
		}
		}
	IEnumerator slowUpdate(){
		yield return new WaitForSeconds (2f);
		Debug.Log("Hit");
		findNearestPixeroid();
		//this.GetComponent<Rigidbody>().AddForce(bolt.GetComponent<FindTowardsVector>().targetVector * 25);
	}


}
