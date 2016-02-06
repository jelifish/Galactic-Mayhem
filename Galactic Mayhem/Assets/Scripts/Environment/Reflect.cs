using UnityEngine;
using System.Collections;

public class Reflect : MonoBehaviour {
	private Transform originalObject;
	public float totalSectorSize;
	public float age;


    public bool sectorClear = false;

    public GameController gc;
    protected Rigidbody rb;

 //   IEnumerator getSectorSize(){
	//	yield return new WaitForSeconds (.5f);
	//	totalSectorSize = GameObject.Find ("GameController").GetComponentInChildren<BoxColSetSectorSize> ().getTotalSectorSize (); //expensive call
	//	totalSectorSize = totalSectorSize / 1.95f;
	
	//}
	void Start()
	{
        //get gamecontroller
        if (GameObject.FindWithTag("GameController") != null)
        {
            gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        }
        else {
            Debug.LogWarning("Cannot Find GameController");
        }
        rb = this.GetComponent<Rigidbody>();

        sectorClear = false;
        originalObject = GetComponent<Rigidbody> ().transform;
		//Destroy (gameObject, age);
		
		totalSectorSize = Mathf.Infinity;
		//StartCoroutine (getSectorSize());
	}
    void FixedUpdate()
    {
        //Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);

        if (!sectorClear)
        {
            this.GetComponent<Rigidbody>().position = new Vector3
                (
                    Mathf.Clamp(this.GetComponent<Rigidbody>().position.x, gc.GetComponent<GameController>().boundary.xMin, gc.GetComponent<GameController>().boundary.xMax),
                    Mathf.Clamp(this.GetComponent<Rigidbody>().position.y, gc.GetComponent<GameController>().boundary.yMin, gc.GetComponent<GameController>().boundary.yMax),
                    0.0f
            );
        }

        if (!sectorClear)
        {

            // this.GetComponent<Rigidbody>().position = new Vector3
            //if(this.riigid)
            if (rb.position.y >= gc.boundary.yMax)
            {
                rb.velocity = new Vector3(rb.velocity.x, -rb.velocity.y, rb.velocity.z);
            }
            else if (rb.position.y <= gc.boundary.yMin)
            {
                rb.velocity = new Vector3(rb.velocity.x, -rb.velocity.y, rb.velocity.z);
            }
            else if (rb.position.x >= gc.boundary.xMax)
            {
                rb.velocity = new Vector3(-rb.velocity.x, rb.velocity.y, rb.velocity.z);
            }
            else if (rb.position.x <= gc.boundary.xMin)
            {
                rb.velocity = new Vector3(-rb.velocity.x, rb.velocity.y, rb.velocity.z);
            }
        }

        if (sectorClear)
        {
            if (this.GetComponent<Rigidbody>().position.x > gc.GetComponent<GameController>().boundary.xMax + 5)
            {
                gc.moveEast();
            }
            if (this.GetComponent<Rigidbody>().position.x < gc.GetComponent<GameController>().boundary.xMin - 5)
            {
                gc.moveWest();
            }
            if (this.GetComponent<Rigidbody>().position.y > gc.GetComponent<GameController>().boundary.yMax + 5)
            {
                gc.moveNorth();
            }
            if (this.GetComponent<Rigidbody>().position.y < gc.GetComponent<GameController>().boundary.xMin - 5)
            {
                gc.moveSouth();
            }
        }


    }
}
