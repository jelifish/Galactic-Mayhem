using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
public class CoreScript : MonoBehaviour {

	private int[][] arr;
	public int size; // (size+1)^2 is the total area of the enemy. THIS MUST BE EVEN
	public int passes;
	public int minValue;
	public float regressionRate;  



	public GameObject block;



	void Awake(){
		regressionRate = (regressionRate * 100) / 100;
		arr = new int[size+1] [];
		for (int i = 0; i<=size; i++) {
			arr[i] = new int[size+1];
		}
		setCore ();
		for (int j=0; j<=Random.Range(passes-passes/2,passes+passes/2); j++) {
			makePass ();
			displayArray ();
		}
		build ();
	}
	private void makePass(){
		for (int i = 0; i<size; i++) {
			for( int j = 0; j<size; j++)
			{
				if(arr[i][j] > 50){
					subRandom(i,j);
				}
				if(arr[i][j] > 50){
					setRandom(i,j);
				}
			}
		}
	}
	private void setRandom(int x, int y){
		if (hasUp(y)) {
			arr[x][y-1] = (int)(((float)(arr[x][y-1])/ regressionRate) +Random.Range(0,100));
		}
		if (hasDown(y)) {
			arr[x][y+1] = (int)(((float)(arr[x][y+1])/regressionRate) + Random.Range(0,100));
		}
		if (hasLeft(x)) {
			arr[x-1][y] = (int)(((float)(arr[x-1][y]) /regressionRate)+ Random.Range(0,100));
		}
		if (hasRight(x)) {
			arr[x+1][y] = (int)(((float)(arr[x+1][y]) /regressionRate)+ Random.Range(0,100));
		}
	}
	private void subRandom(int x, int y){
		if (hasUp(y)) {
			arr[x][y-1] = (int)(((float)(arr[x][y-1])/ regressionRate) -Random.Range(0,100));
		}
		if (hasDown(y)) {
			arr[x][y+1] = (int)(((float)(arr[x][y+1])/regressionRate) - Random.Range(0,100));
		}
		if (hasLeft(x)) {
			arr[x-1][y] = (int)(((float)(arr[x-1][y]) /regressionRate)- Random.Range(0,100));
		}
		if (hasRight(x)) {
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



	private void build()
	{
		int count = 0;
		float hexa = 0.5f;
		//Debug.Log (blockWidth);
		int a =0, b = 0;
		foreach(int[] i in arr){
			foreach(int j in i){
			

				if(j>0&&!((a==size/2)&&(b==size/2))){
				GameObject child = (GameObject)Instantiate(block, this.transform.position, this.transform.rotation);

					child.transform.localScale = transform.lossyScale;
					child.transform.parent = transform;
					float blockWidth = block.transform.lossyScale.x;
				

					child.transform.position = new Vector3(((a-(size/2)))*transform.lossyScale.x + this.transform.position.x,0,((size/2 - b))*transform.lossyScale.x + this.transform.position.z);
					child.GetComponent<FixedJoint> ().connectedBody = this.GetComponent<Rigidbody>();

					//+(a%1)*hexa
				


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
	}



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
