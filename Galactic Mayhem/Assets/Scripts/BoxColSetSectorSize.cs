using UnityEngine;
using System.Collections;

public class BoxColSetSectorSize : MonoBehaviour {
	public BoxCollider SectorSize;
	public float padding;
	private float totalSectorSize; //size + padding
	// Use this for initialization
	public void setBounds(float size){  //size is the width AND the height of the boundary box
//		Debug.Log (size);
//		if (size < 35) {//size is at least 10
//			this.GetComponent<BoxCollider>().size = new Vector3 ( 35,50,35);
//		}
//		else{
			this.GetComponent<BoxCollider>().size = new Vector3 ( size+padding,5,size+padding);  
//		}

		totalSectorSize = size + padding;

	}


	public float getTotalSectorSize(){
		return totalSectorSize;
	}

}
