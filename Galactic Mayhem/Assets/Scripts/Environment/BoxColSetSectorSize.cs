using UnityEngine;
using System.Collections;

public class BoxColSetSectorSize : MonoBehaviour {
	public BoxCollider SectorSize;
	public float padding =0;
	public LineRenderer north;
	public LineRenderer south;
	public LineRenderer east;
	public LineRenderer west;

	public LineRenderer north2;
	public LineRenderer south2;
	public LineRenderer east2;
	public LineRenderer west2;

	public Camera minimap;

	public void liftBorder(){
		north.transform.gameObject.SetActive (false);
		south.transform.gameObject.SetActive (false);
		east.transform.gameObject.SetActive (false);
		west.transform.gameObject.SetActive (false);

		north2.transform.gameObject.SetActive (true);
		south2.transform.gameObject.SetActive (true);
		east2.transform.gameObject.SetActive (true);
		west2.transform.gameObject.SetActive (true);

	}
	public void setBorder(){
		north.transform.gameObject.SetActive (true);
		south.transform.gameObject.SetActive (true);
		east.transform.gameObject.SetActive (true);
		west.transform.gameObject.SetActive (true);
		
		north2.transform.gameObject.SetActive (false);
		south2.transform.gameObject.SetActive (false);
		east2.transform.gameObject.SetActive (false);
		west2.transform.gameObject.SetActive (false);
	}


	private float totalSectorSize; //size + padding
	// Use this for initialization

	public void setBounds(float size){  //size is the width AND the height of the boundary box

		this.GetComponent<BoxCollider>().size = new Vector3 ( size+padding,size+padding,5);  

		float cornerOffset = .25f;
		north.GetComponent<LineRenderer>().SetPosition (0, new Vector3 (-SectorSize.size.x/2-1-cornerOffset, SectorSize.size.y/2+1, 0f));
		north.GetComponent<LineRenderer>().SetPosition(1, new Vector3(SectorSize.size.x/2+1+cornerOffset,SectorSize.size.y/2+1,0f));
		
		south.GetComponent<LineRenderer>().SetPosition (0, new Vector3 (-SectorSize.size.x/2-1f-cornerOffset, -SectorSize.size.y/2-1f, 0f));
		south.GetComponent<LineRenderer>().SetPosition(1, new Vector3(SectorSize.size.x/2 +1+cornerOffset,-SectorSize.size.y/2-1f,0f));
		
		west.GetComponent<LineRenderer>().SetPosition (0, new Vector3 (-SectorSize.size.x/2-1, SectorSize.size.y/2+1,0f));
		west.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-SectorSize.size.x/2-1,-SectorSize.size.y/2-1,0f));
		
		east.GetComponent<LineRenderer>().SetPosition (0, new Vector3 (SectorSize.size.x/2+1, SectorSize.size.y/2+1, 0f));
		east.GetComponent<LineRenderer>().SetPosition(1, new Vector3(SectorSize.size.x/2+1,-SectorSize.size.y/2-1,0f));




		north2.GetComponent<LineRenderer>().SetPosition (0, new Vector3 (-SectorSize.size.x/2-1-cornerOffset, SectorSize.size.y/2+1, 0f));
		north2.GetComponent<LineRenderer>().SetPosition(1, new Vector3(SectorSize.size.x/2+1+cornerOffset,SectorSize.size.y/2+1,0f));
		
		south2.GetComponent<LineRenderer>().SetPosition (0, new Vector3 (-SectorSize.size.x/2-1f-cornerOffset, -SectorSize.size.y/2-1f, 0f));
		south2.GetComponent<LineRenderer>().SetPosition(1, new Vector3(SectorSize.size.x/2 +1+cornerOffset,-SectorSize.size.y/2-1f,0f));
		
		west2.GetComponent<LineRenderer>().SetPosition (0, new Vector3 (-SectorSize.size.x/2-1, SectorSize.size.y/2+1,0f));
		west2.GetComponent<LineRenderer>().SetPosition(1, new Vector3(-SectorSize.size.x/2-1,-SectorSize.size.y/2-1,0f));
		
		east2.GetComponent<LineRenderer>().SetPosition (0, new Vector3 (SectorSize.size.x/2+1, SectorSize.size.y/2+1, 0f));
		east2.GetComponent<LineRenderer>().SetPosition(1, new Vector3(SectorSize.size.x/2+1,-SectorSize.size.y/2-1,0f));

		setBorder ();



		totalSectorSize = size + padding;


		minimap.orthographicSize = totalSectorSize * 0.80f;
        if (Screen.height > Screen.width) {
            minimap.orthographicSize *= 2;
        }
	}


	public float getTotalSectorSize(){
		return totalSectorSize;
	}

}
