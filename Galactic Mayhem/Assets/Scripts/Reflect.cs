using UnityEngine;
using System.Collections;

public class Reflect : MonoBehaviour {
	private Transform originalObject;
	public float totalSectorSize;
	public float age;

	IEnumerator getSectorSize(){
		yield return new WaitForSeconds (.5f);
		totalSectorSize = GameObject.Find ("GameController").GetComponentInChildren<BoxColSetSectorSize> ().getTotalSectorSize (); //expensive call
	
	
	}
	void Start()
	{
		originalObject = GetComponent<Rigidbody> ().transform;
		//Destroy (gameObject, age);
		
		totalSectorSize = Mathf.Infinity;
		StartCoroutine (getSectorSize());
	}
	void Update()
	{
		  
//		totalSectorSize = GameObject.Find ("GameController").GetComponentInChildren<BoxColSetSectorSize> ().getTotalSectorSize (); //expensive call
		//remove glitched objects
//		if (mirrorRate >= 5) {
//			Destroy (gameObject);
//		}
		
		//port object into other side of screen (horizontal)
//		if (Mathf.Abs (GetComponent<Rigidbody> ().position.x) >= totalSectorSize/1.2) 
//		{
//			mirrorRate+=1;
//			GetComponent<Rigidbody>().position =  Vector3.Reflect(originalObject.position, Vector3.right);
//			GetComponent<Rigidbody>().rotation = Quaternion.Euler( 90 , Random.Range(0, 180),0);// , 0);
//			originalObject =GetComponent<Rigidbody>().transform;
//
//		}
//		//port object into other side of screen (vertical)
//		if (Mathf.Abs (GetComponent<Rigidbody> ().position.z) >= totalSectorSize/1.2) 
//		{
//			mirrorRate+=1;
//			GetComponent<Rigidbody>().position =  Vector3.Reflect(originalObject.position, Vector3.forward);
//			//GetComponent<Rigidbody>().rotation = Quaternion.Euler( 0 , Random.Range(0, 180) , 0);
//			originalObject =GetComponent<Rigidbody>().transform;
//		}


		
		//code to reflect off sides
//		Debug.Log (totalSectorSize);

		if (GetComponent<Rigidbody> ().position.x >= totalSectorSize/1.2 || -GetComponent<Rigidbody> ().position.x >= totalSectorSize/1.2)        
				{
			if(GetComponent<Rigidbody> ().position.x >=totalSectorSize/1.2){
						GetComponent<Rigidbody>().position =  originalObject.position- (Vector3.right* .5f);
					}
			else if(GetComponent<Rigidbody> ().position.x <=totalSectorSize/1.2){
						GetComponent<Rigidbody>().position =  originalObject.position- (Vector3.left* .5f);
					}
					GetComponent<Rigidbody>().velocity =  Vector3.Reflect(originalObject.GetComponent<Rigidbody>().velocity, Vector3.right);
					originalObject =GetComponent<Rigidbody>().transform;
				}


		if (GetComponent<Rigidbody> ().position.z >= totalSectorSize/1.2 || -GetComponent<Rigidbody> ().position.z >= totalSectorSize/1.2)        
		{
			if(GetComponent<Rigidbody> ().position.z >=totalSectorSize/1.2){
				GetComponent<Rigidbody>().position =  originalObject.position- (Vector3.forward* .5f);
			}
			else if(GetComponent<Rigidbody> ().position.z <=totalSectorSize/1.2){
				GetComponent<Rigidbody>().position =  originalObject.position- (Vector3.back* .5f);
			}
			GetComponent<Rigidbody>().velocity =  Vector3.Reflect(originalObject.GetComponent<Rigidbody>().velocity, Vector3.forward);
			originalObject =GetComponent<Rigidbody>().transform;
		}
		

	}
}
