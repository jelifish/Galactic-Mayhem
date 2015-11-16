using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoreScript : MonoBehaviour {

	private int[][] arr;
	public int size; // (size+1)^2 is the total area of the enemy. THIS MUST BE EVEN
	public int passes;
	public int minValue;
	public float regressionRate;  

	public GameObject block;
	public GameObject core;

	public GameController gc;
	public float sectorDifficulty;
	void Start(){
		if (GameObject.FindWithTag ("GameController") != null) {
			gc = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
		}else{ Debug.LogWarning("Cannot Find GameController");
		}
		addDifficulty ();
		StartCoroutine (design ());
		StartCoroutine (build ());
		StartCoroutine (move ());
	}
	private void addDifficulty(){
		sectorDifficulty = ((sectorDifficulty / 2) * 2)+1;
		this.size += (int)sectorDifficulty;
		this.passes += (int)sectorDifficulty/2;
	}
	private void makePass(){
		for (int i = 0; i<size; i++) {
			for( int j = 0; j<size; j++)
			{
				if(arr[i][j] >= 49){            //50
					subRandom(i,j);
				}
				if(arr[i][j] >= 50){            //50
					setRandom(i,j);
				}
			}
		}
	}
	private void subThisRandom(int x, int y)
	{
		arr[x][y-1] = (int)(((float)(arr[x][y])/ regressionRate) -Random.Range(0,100));
	}
	private void setRandom(int x, int y){
		if (y >= 1) {
			arr[x][y-1] = (int)(((float)(arr[x][y-1])/ regressionRate) +Random.Range(0,100));
		}
		if (y <= size-1) {
			arr[x][y+1] = (int)(((float)(arr[x][y+1])/regressionRate) + Random.Range(0,100));
		}
		if (x >= 1) {
			arr[x-1][y] = (int)(((float)(arr[x-1][y]) /regressionRate)+ Random.Range(0,100));
		}
		if (x <= size-1) {
			arr[x+1][y] = (int)(((float)(arr[x+1][y]) /regressionRate)+ Random.Range(0,100));
		}
	}
	private void subRandom(int x, int y){
		if (y >= 1){
			arr[x][y-1] = (int)(((float)(arr[x][y-1])/ regressionRate) -Random.Range(0,100));
		}
		if (y <= size-1) {
			arr[x][y+1] = (int)(((float)(arr[x][y+1])/regressionRate) - Random.Range(0,100));
		}
		if (x >= 1) {
			arr[x-1][y] = (int)(((float)(arr[x-1][y]) /regressionRate)- Random.Range(0,100));
		}
		if (x <= size-1) {
			arr[x+1][y] = (int)(((float)(arr[x+1][y]) /regressionRate)- Random.Range(0,100));
		}
	}
	void displayArray(){
		Debug.Log("---------------------------");
		int count = 0;
		string total = "";
		foreach(int[] i in arr){
			foreach(int j in i){
			total+= "   " + j;
			count++;
			if (count>size){
				Debug.Log(total);
				total = "";
					count = 0;
			}
			}
		}

	}

	private void setCore(){
		arr [(size / 2)] [(size / 2)] = 100;
	}
	private bool hasUp(int y)
	{
		return (y >= 1);
	}
	private bool hasDown(int y)
	{
		return (y <= size-1);
	}
	private bool hasLeft(int x)
	{
		return (x >= 1);
	}
	private bool hasRight(int x)
	{
		return (x <= size-1);
	}
	public bool isChild =false;
	public int childCore = 1;
	public GameObject blaster;
	public GameObject shortBlaster;
	private IEnumerator design()
	{

		regressionRate = (regressionRate * 100) / 100;
		arr = new int[size+1] [];
		for (int i = 0; i<=size; i++) {
			arr[i] = new int[size+1];
		}
		//Debug.Log (isChild + " " + childCore);
		setCore ();
		yield return new WaitForSeconds (.01f);
		
		int finalPass = Random.Range (passes - passes / 2, passes + passes / 2);
		for (int j=0; j<=finalPass; j++) {
			makePass ();
			//displayArray ();
			
			if(Random.value>=.7f && finalPass - j >3)
			{
				arr[Random.Range(0,size)][Random.Range(0,size)] += 50;
				yield return new WaitForSeconds (.001f);
			}
			
			
		}


		yield return new WaitForSeconds (.01f);
	}
	private IEnumerator build()
	{
		int count = 0;
		int a =0, b = 0;
		yield return new WaitForSeconds (.5f);
		foreach(int[] i in arr){
			foreach(int j in i){
				if(j>0&&!((a==size/2)&&(b==size/2))){
					float adjust = 0;
					if (size%4 == 0)
					{adjust = 0;}
					else {adjust = .5f;}
					float hexa = .5f;
					GameObject child;
//					if(childCore>=1&&Random.value>=.95){
//						//CoreSpawner 5%chance?
//
//							child = (GameObject)Instantiate(core, new Vector3(((a-(size/2)-(b%2)*hexa))*transform.lossyScale.x + this.transform.position.x +((hexa)* (adjust)),((size/2 - b))*transform.lossyScale.y + this.transform.position.y ,Random.Range(-.1f,.1f)), this.transform.rotation);
//
//							child.transform.localScale = transform.lossyScale; 
//							child.transform.parent = transform;//child; 
//							child.GetComponent<CoreScript>().childCore = 0;
//							child.GetComponent<CoreScript>().isChild = true;
//
//					}else{
					child = (GameObject)Instantiate(block, new Vector3(((a-(size/2)-(b%2)*hexa))*transform.lossyScale.x + this.transform.position.x +((hexa)* (adjust)),((size/2 - b))*transform.lossyScale.y + this.transform.position.y ,Random.Range(-.1f,.1f)), this.transform.rotation);
					child.transform.localScale = transform.lossyScale;
					child.transform.parent = transform;


					if(Random.Range(0,100)>=90){
						GameObject thisBlaster = (GameObject)Instantiate(blaster, child.transform.position, this.transform.rotation);
						thisBlaster.transform.localScale = child.transform.lossyScale;
						thisBlaster.transform.parent = child.transform;//child;
					}
					else if(Random.Range(0,100)>= 90&&gc.difficulty>4){
						GameObject thisBlaster = (GameObject)Instantiate(shortBlaster, child.transform.position, this.transform.rotation);
						thisBlaster.transform.localScale = child.transform.lossyScale;
						thisBlaster.transform.parent = child.transform;
					}












					yield return new WaitForSeconds (.05f);
				}

				count++;
				b++;
				if (count>size){
					count = 0;
				}
			}
			b=0;
			a++;
		}
		if (!isChild) {
			GetComponent<Rigidbody> ().AddForce (Random.insideUnitSphere * 200);
			GetComponent<Rigidbody> ().AddTorque (new Vector3 (0, 0, Random.Range (-.5f, .5f)), ForceMode.Impulse);
		}
	}
	IEnumerator move(){
		while (true&&!isChild) {
			yield return new WaitForSeconds( Random.Range (1F,5F));

			GetComponent<Rigidbody> ().AddForce (Random.insideUnitSphere* Random.Range (50,300));
			GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * 7;
			
		}
	}

}
