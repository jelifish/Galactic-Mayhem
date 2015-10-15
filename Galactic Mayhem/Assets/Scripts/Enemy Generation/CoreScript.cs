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

	void Start(){
		//GetComponent<Rigidbody> ().rotation = Quaternion.Euler (0, Random.Range (0, 360), 0);
		//GetComponent<Rigidbody>().rotation= Quaternion.Euler(GetComponent<Rigidbody>().rotation.eulerAngles.x,Random.Range (0, 360),GetComponent<Rigidbody>().rotation.eulerAngles.z);
		regressionRate = (regressionRate * 100) / 100;
		arr = new int[size+1] [];
		for (int i = 0; i<=size; i++) {
			arr[i] = new int[size+1];
		}
		setCore ();


		int finalPass = Random.Range (passes - passes / 2, passes + passes / 2);
		for (int j=0; j<=finalPass; j++) {
			makePass ();
			displayArray ();

			if(Random.value>=.7f && finalPass - j >3)
			{
				arr[Random.Range(0,size)][Random.Range(0,size)] += 50;
			}


		}
		StartCoroutine (build ());
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



	private IEnumerator build()
	{
//		Vector3 origpos = transform.position;
//		Vector3 origrot = transform.rotation;

		//GetComponent<Rigidbody>()
		//yield return new WaitForSeconds (5f);
		//Time.timeScale = 0.0f;
		int count = 0;
		int a =0, b = 0;
		yield return new WaitForSeconds (.5f);
		foreach(int[] i in arr){
			foreach(int j in i){
				if(j>0&&!((a==size/2)&&(b==size/2))){
				GameObject child = (GameObject)Instantiate(block, this.transform.position, this.transform.rotation);

					child.transform.localScale = transform.lossyScale;
					child.transform.parent = transform;
					float blockWidth = block.transform.lossyScale.x;
					int adjust = 0;
					if (size%4 == 0)
					{adjust = 0;}
					else {adjust = 1;}

					float hexa = .5f;
					child.transform.position = new Vector3(((a-(size/2)-(b%2)*hexa))*transform.lossyScale.x + this.transform.position.x +((hexa)* (adjust)),0,((size/2 - b))*transform.lossyScale.x + this.transform.position.z); /// HEXA ON
					//child.transform.position = new Vector3(((a-(size/2)))*transform.lossyScale.x + this.transform.position.x ,0,((size/2 - b))*transform.lossyScale.x + this.transform.position.z); ////HEXA OFF
					//+(a%2)*hexa // this code is in there TWICE^^ and makes shifts that creates the hexagon tile look vs the square tiles. 
					//one shift to shift separate rows, the other to shift the core. imo better looking than tile.
					child.GetComponent<ConfigurableJoint> ().connectedBody = this.GetComponent<Rigidbody>();
					yield return new WaitForSeconds (.02f);
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
		GetComponent<Rigidbody> ().AddTorque (new Vector3 (0, Random.Range (-100, 100), 0), ForceMode.Impulse);
	}
//
//	IEnumerator fireStorm()
//	{
//		fireball1.transform.localScale = new Vector3(0.5F,0.5F,0.5F);
//		//freezeChara = true;
//		ready = false;
//		while (true) {
//			for(int i=0;i<fireStormDuration;i++){
//				fireball1.GetComponent<Rigidbody>().drag = 1.0f;
//				shoot(new Load (fireball1, Quaternion.identity));
//				yield return new WaitForSeconds (.003f);
//			}
//			break;
//		}
//		fireball1.transform.localScale = attack1DefaultSize;
//		fireball1.GetComponent<Rigidbody>().drag = attack1DefaultDrag;
//		//freezeChara = false;
//		yield return new WaitForSeconds (fireStormDelay);//cooldown before cast again
//		ready = true;
//		
//		
//	}





	//old algorithm as a reminder to never use recursion.

	//[System.Serializable]
	//public class CoordStruct
	//{
	//	public int hashableInt()
	//	{
	//		int ex = Mathf.RoundToInt(x);
	//		int why = Mathf.RoundToInt(y);
	//		Debug.Log("HASH NUMBER: "+ (1000000+ (ex * 10000) + why) );
	//		return 1000000+(ex * 10000) + why ;
	//	}
	//	public int x;
	//	public int y;
	//	public CoordStruct(int a, int b){
	//		x = a;
	//		y = b;
	//	}
	//	public string toString()
	//	{
	//		return(x.ToString() + y.ToString());
	//	}
	//}
	//[System.Serializable]
	//public class BlockVariables
	//{
	//	public Dictionary<int,int> coordMap;
	//	public CoordStruct thisCoord;
	//	public CoordStruct parentCoord;
	//	public int level;
	//	public float chance;
	//	public float chanceDec;
	//
	//	public GameObject parent; 
	//	public BlockVariables(Dictionary<int,int> map, CoordStruct coord,CoordStruct parentCoord,int level, float chance, float chanceDec,GameObject parent)
	//	{
	//		this.coordMap = map;
	//		this.thisCoord = coord;
	//		this.parentCoord = parentCoord;
	//		this.level = level;
	//		this.chance = chance;
	//		this.chanceDec = chanceDec;
	//		this.parent = parent;
	//	}
	//}
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//	public int hp = 100; 
//	public int level;
//	public float chance;
//	public float chanceDec;
//	Dictionary<int,int> coordMap = new Dictionary<int,int>();
//	public BlockVariables blockInfo;
//
//	// Use this for initialization
//	void Start () {
//		blockInfo = new BlockVariables(coordMap,new CoordStruct(0,0),new CoordStruct(0,0),0,90,1,this.gameObject);
//
//		init ();
//	}
//	void OnCollisionEnter(Collision collision) {
//		//ContactPoint contact = collision.contacts[0];
//		hp = hp - 1;
//		if(hp <= 0)
//		{
//		List<GameObject> children = new List<GameObject>();
//		foreach (Transform child in transform) children.Add(child.gameObject);
//		children.ForEach(child => Destroy(child));
//		Destroy(gameObject);
//		}
//	}
//	private void init(){
//
//		createBlockNorth (blockInfo);
//		createBlockSouth (blockInfo);
//		createBlockEast (blockInfo);
//		createBlockWest (blockInfo);
//	}
//
//
//
//
//
//	private GameObject child;
//	public void createCore(){
//		coordMap.Add(new CoordStruct(0,0).hashableInt(),0);
//	}
//	private BlockVariables tempInfo;
//	public void createBlockEast(BlockVariables blockInfo){
//		tempInfo = blockInfo;
//		//Debug.Log ("hashed already:  " + new CoordStruct(-1, 0).hashableInt());
//		coordMap.Add(new CoordStruct(1, 0).hashableInt(),0);
//		child = (GameObject)Instantiate(block, new Vector3(this.transform.position.x + this.transform.lossyScale.x,0, this.transform.position.z), this.transform.rotation);
//		child.GetComponent<FixedJoint> ().connectedBody = this.GetComponent<Rigidbody>();
//		child.transform.localScale = transform.lossyScale;
//		child.transform.parent = transform;
//		tempInfo.thisCoord = new CoordStruct (1, 0);
//		tempInfo.parentCoord = new CoordStruct (0, 0);
//		tempInfo.chance = this.chance;
//		tempInfo.chanceDec = this.chanceDec;
//		tempInfo.level = this.level;
//		child.GetComponent<BlockCreation> ().spawn (tempInfo);
//	}
//	public void createBlockWest(BlockVariables blockInfo){
//		tempInfo = blockInfo;
//		//Debug.Log ("hashed already:  " + new CoordStruct(1, 0).hashableInt());
//		coordMap.Add(new CoordStruct(-1, 0).hashableInt(),0);
//		child = (GameObject)Instantiate(block, new Vector3(this.transform.position.x - this.transform.lossyScale.x,0, this.transform.position.z), this.transform.rotation);
//		child.GetComponent<FixedJoint> ().connectedBody = this.GetComponent<Rigidbody>();
//		child.transform.localScale = transform.lossyScale;
//		child.transform.parent = transform;
//		tempInfo.thisCoord = new CoordStruct (-1, 0);
//		tempInfo.parentCoord = new CoordStruct (0, 0);
//		tempInfo.chance = this.chance;
//		tempInfo.chanceDec = this.chanceDec;
//		tempInfo.level = this.level;
//
//		child.GetComponent<BlockCreation> ().spawn (tempInfo);
//	}
//	public void createBlockNorth(BlockVariables blockInfo){
//		tempInfo = blockInfo;
//		//Debug.Log ("hashed already:  " + new CoordStruct(0,1).hashableInt());
//		coordMap.Add(new CoordStruct(0,1).hashableInt(),0);
//		child = (GameObject)Instantiate(block, new Vector3(this.transform.position.x,0, this.transform.position.z+ this.transform.lossyScale.z), this.transform.rotation);
//		child.GetComponent<FixedJoint> ().connectedBody = this.GetComponent<Rigidbody>();
//		child.transform.localScale = transform.lossyScale;
//		child.transform.parent = transform;
//		tempInfo.thisCoord = new CoordStruct (0, 1);
//		tempInfo.parentCoord = new CoordStruct (0, 0);
//		tempInfo.chance = this.chance;
//		tempInfo.chanceDec = this.chanceDec;
//		tempInfo.level = this.level;
//		child.GetComponent<BlockCreation> ().spawn (tempInfo);
//
//	}
//	public void createBlockSouth(BlockVariables blockInfo){
//		tempInfo = blockInfo;
//		//Debug.Log ("hashed already:  " + new CoordStruct(0, -1).hashableInt());
//		coordMap.Add(new CoordStruct(0, -1).hashableInt(),0);
//		child = (GameObject)Instantiate(block, new Vector3(this.transform.position.x,0, this.transform.position.z- this.transform.lossyScale.z), this.transform.rotation);
//		child.GetComponent<FixedJoint> ().connectedBody = this.GetComponent<Rigidbody>();
//		child.transform.localScale = transform.lossyScale;
//		child.transform.parent = transform;
//		tempInfo.thisCoord = new CoordStruct (0, -1);
//		tempInfo.parentCoord = new CoordStruct (0, 0);
//		tempInfo.chance = this.chance;
//		tempInfo.chanceDec = this.chanceDec;
//		tempInfo.level = this.level;
//		child.GetComponent<BlockCreation> ().spawn (tempInfo);
//	}


}
