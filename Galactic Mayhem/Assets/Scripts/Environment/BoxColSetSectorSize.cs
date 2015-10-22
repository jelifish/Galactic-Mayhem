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
		this.GetComponent<BoxCollider>().size = new Vector3 ( size+padding,size+padding,5);  
//		}
		float cornerOffset = .25f;
		//north.GetComponent<LineRenderer>().SetVertexCount (2);
		north.GetComponent<LineRenderer>().SetPosition (0, new Vector3 (-SectorSize.size.x/2-1-cornerOffset, SectorSize.size.y/2+1, 0f));
		north.GetComponent<LineRenderer>().SetPosition(1, new Vector3(SectorSize.size.x/2+1+cornerOffset,SectorSize.size.y/2+1,0f));
		
		south.GetComponent<LineRenderer>().SetPosition (0, new Vector3 (-SectorSize.size.x/2-1f-cornerOffset, -SectorSize.size.y/2-1f, 0f));
		south.GetComponent<LineRenderer>().SetPosition(1, new Vector3(SectorSize.size.x/2 +1+cornerOffset,-SectorSize.size.y/2-1f,0f));
		
		west.GetComponent<LineRenderer>().SetPosition (0, new Vector3 (-SectorSize.size.x/2-1, SectorSize.size.y/2+1,0f));
		west.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-SectorSize.size.x/2-1,-SectorSize.size.y/2-1,0f));
		
		east.GetComponent<LineRenderer>().SetPosition (0, new Vector3 (SectorSize.size.x/2+1, SectorSize.size.y/2+1, 0f));
		east.GetComponent<LineRenderer>().SetPosition(1, new Vector3(SectorSize.size.x/2+1,-SectorSize.size.y/2-1,0f));





		totalSectorSize = size + padding;

	}


	public float getTotalSectorSize(){
		return totalSectorSize;
	}

}
