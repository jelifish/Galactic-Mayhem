using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//var pos = Camera.main.WorldToScreenPoint(transform.position);

//var dir = Input.mousePosition - pos;
//var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
//transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 

 

public class LeftPlayerController : MonoBehaviour
{
	private Vector2 mousePos;
	private Vector3 screenPos;

	public float speed;
	public float rotationSpeed;
	//	public Boundary boundary;
	public Load load;
	public bool loaded = false;
	
	public GameObject strike1;
	public GameObject fireball2;
	
	public List<Load> loader = new List<Load>();
	//public ArrayList loader;
	//public GameObject loader ;
	
	public Transform shotSpawn;
	
	public float fireRate = 0.5F;
	private float nextFire = 0.0F;
	
	public void shoot(Load load) 
	{
		Instantiate (load.projectile, shotSpawn.position, shotSpawn.rotation * load.rotation);
		//return load;
		//Time.timeScale = .5f;
		
	}
	
	
	
	void Start(){
		//	loader.Add (fireball1);
		//load = new Load ();
		//loader.Add (new Load (fireball1, Quaternion.identity));
		nextFire = Time.time + fireRate;
		mousePos = Input.mousePosition;

		
	}
	
	void Update()
	{
		//this.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90);
		//mousePos = Input.mousePosition;

		//screenPos = Camera.main.ScreenToWorldPoint(Vector3(mousePos.x, mousePos.y, transform.position.z - Camera.main.transform.position.z));
		
		//this.GetComponent<Rigidbody>().transform.eulerAngles = new Vector3(90,Mathf.Atan2((mousePos.y - transform.position.y), (mousePos.x - transform.position.x))*Mathf.Rad2Deg - 90,0);


		if (Input.GetButton ("Fire2") && Time.time > nextFire) {
			if (loaded == false) {
				loader.Add (new Load (strike1, Quaternion.identity));
			}
			
			foreach (Load load in loader) {
				shoot (load); //shoots and removes the loaded object
				
			}
			loader.Clear ();
			loaded = false;//empty magazine
			
			nextFire = Time.time + fireRate;
		} else if (Time.time > nextFire + 2 && loaded == false) {
			nextFire = Time.time + fireRate;
			loaded = true;  //locked and loaded
			//loader.Add (new Load (strike1, Quaternion.identity * new Quaternion (0f, 0.01f, 0f, 0.01f)));
			//loader.Add (new Load (strike1, Quaternion.identity * new Quaternion (0f, -0.01f, 0f, 0.01f)));

			loader.Add (new Load (strike1, Quaternion.identity));
			//loader.Add (new Load (strike1, Quaternion.identity));
			//loader.Add (new Load (strike1, Quaternion.identity));



		} 


		if (Input.GetButtonUp ("Fire2") && loaded == true) {
			foreach (Load load in loader) {
				shoot (load); //shoots and removes the loaded object
				
			}
			loader.Clear ();
			loaded = false;//empty magazine
			
		}
		
		
	}
	
	void FixedUpdate ()
	{
		//float moveHorizontal = Input.GetAxis ("HorizontalLeft");
		//float moveVertical = Input.GetAxis ("Vertical");
		
		//Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		//GetComponent<Rigidbody>().velocity = movement * speed;
		//		GetComponent<Rigidbody>().position = new Vector3 
		//			(
		//				Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
		//				0.0f, 
		//				Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		//				);
		
		
		//GetComponent<Rigidbody>().rotation = GetComponent<Rigidbody>().rotation * Quaternion.Euler (0.0f, 0f, moveHorizontal * rotationSpeed);
		
	}
	//IEnumerator shootOverTime(){
	//}
	
	void LateUpdate(){
		
		
		
	}
	
	
}