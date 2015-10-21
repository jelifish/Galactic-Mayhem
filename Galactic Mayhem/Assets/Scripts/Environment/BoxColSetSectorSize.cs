using UnityEngine;
using System.Collections;

public class BoxColSetSectorSize : MonoBehaviour {
	public BoxCollider SectorSize;
	public float padding =0;
	public LineRenderer north;
	public LineRenderer south;
	public LineRenderer east;
	public LineRenderer west;




	private float totalSectorSize; //size + padding
	// Use this for initialization
	public void setBounds(float size){  //size is the width AND the height of the boundary box
		Debug.Log (size);
//		if (size < 35) {//size is at least 10
//			this.GetComponent<BoxCollider>().size = new Vector3 ( 35,50,35);
//		}
//		else{
			this.GetComponent<BoxCollider>().size = new Vector3 ( size+padding,5,size+padding);  
//		}
		float cornerOffset = .25f;
		//north.GetComponent<LineRenderer>().SetVertexCount (2);
		north.GetComponent<LineRenderer>().SetPosition (0, new Vector3 (-SectorSize.size.x/2-1-cornerOffset, 0f, SectorSize.size.z/2+1));
		north.GetComponent<LineRenderer>().SetPosition(1, new Vector3(SectorSize.size.x/2+1+cornerOffset,0f,SectorSize.size.z/2+1));
		
		south.GetComponent<LineRenderer>().SetPosition (0, new Vector3 (-SectorSize.size.x/2-1f-cornerOffset, 0f, -SectorSize.size.z/2-1f));
		south.GetComponent<LineRenderer>().SetPosition(1, new Vector3(SectorSize.size.x/2 +1+cornerOffset,0f,-SectorSize.size.z/2-1f));
		
		west.GetComponent<LineRenderer>().SetPosition (0, new Vector3 (-SectorSize.size.x/2-1, 0f, SectorSize.size.z/2+1));
		west.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-SectorSize.size.x/2-1,0f,-SectorSize.size.z/2-1));
		
		east.GetComponent<LineRenderer>().SetPosition (0, new Vector3 (SectorSize.size.x/2+1, 0f, SectorSize.size.z/2+1));
		east.GetComponent<LineRenderer>().SetPosition(1, new Vector3(SectorSize.size.x/2+1,0f,-SectorSize.size.z/2-1));





		totalSectorSize = size + padding;

	}


	public float getTotalSectorSize(){
		return totalSectorSize;
	}

}
