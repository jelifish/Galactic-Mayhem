using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class CoordStruct
{
	public int hashableInt()
	{
		int ex = Mathf.RoundToInt(x);
		int why = Mathf.RoundToInt(y);
		Debug.Log("HASH NUMBER: "+ (1000000+ (ex * 10000) + why) );
		return 1000000+(ex * 10000) + why ;
	}
	public int x;
	public int y;
	public CoordStruct(int a, int b){
		x = a;
		y = b;
	}
	public string toString()
	{
		return(x.ToString() + y.ToString());
	}
}
[System.Serializable]
public class BlockVariables
{
	public Dictionary<int,int> coordMap;
	public CoordStruct thisCoord;
	public CoordStruct parentCoord;
	public int level;
	public float chance;
	public float chanceDec;

	public GameObject parent; 
	public BlockVariables(Dictionary<int,int> map, CoordStruct coord,CoordStruct parentCoord,int level, float chance, float chanceDec,GameObject parent)
	{
		this.coordMap = map;
		this.thisCoord = coord;
		this.parentCoord = parentCoord;
		this.level = level;
		this.chance = chance;
		this.chanceDec = chanceDec;
		this.parent = parent;
	}
}
public class CoreScript : MonoBehaviour {
	public GameObject block;
	public int hp = 100; 
	public int level;
	public float chance;
	public float chanceDec;
	Dictionary<int,int> coordMap = new Dictionary<int,int>();
	public BlockVariables blockInfo;

	// Use this for initialization
	void Start () {
		blockInfo = new BlockVariables(coordMap,new CoordStruct(0,0),new CoordStruct(0,0),0,90,1,this.gameObject);

		init ();
	}
	void OnCollisionEnter(Collision collision) {
		//ContactPoint contact = collision.contacts[0];
		hp = hp - 1;
		if(hp <= 0)
		{
		List<GameObject> children = new List<GameObject>();
		foreach (Transform child in transform) children.Add(child.gameObject);
		children.ForEach(child => Destroy(child));
		Destroy(gameObject);
		}
	}
	private void init(){

		createBlockNorth (blockInfo);
		createBlockSouth (blockInfo);
		createBlockEast (blockInfo);
		createBlockWest (blockInfo);
	}





	private GameObject child;
	public void createCore(){
		coordMap.Add(new CoordStruct(0,0).hashableInt(),0);
	}
	private BlockVariables tempInfo;
	public void createBlockEast(BlockVariables blockInfo){
		tempInfo = blockInfo;
		//Debug.Log ("hashed already:  " + new CoordStruct(-1, 0).hashableInt());
		coordMap.Add(new CoordStruct(1, 0).hashableInt(),0);
		child = (GameObject)Instantiate(block, new Vector3(this.transform.position.x + this.transform.lossyScale.x,0, this.transform.position.z), this.transform.rotation);
		child.GetComponent<FixedJoint> ().connectedBody = this.GetComponent<Rigidbody>();
		child.transform.localScale = transform.lossyScale;
		child.transform.parent = transform;
		tempInfo.thisCoord = new CoordStruct (1, 0);
		tempInfo.parentCoord = new CoordStruct (0, 0);
		tempInfo.chance = this.chance;
		tempInfo.chanceDec = this.chanceDec;
		tempInfo.level = this.level;
		child.GetComponent<BlockCreation> ().spawn (tempInfo);
	}
	public void createBlockWest(BlockVariables blockInfo){
		tempInfo = blockInfo;
		//Debug.Log ("hashed already:  " + new CoordStruct(1, 0).hashableInt());
		coordMap.Add(new CoordStruct(-1, 0).hashableInt(),0);
		child = (GameObject)Instantiate(block, new Vector3(this.transform.position.x - this.transform.lossyScale.x,0, this.transform.position.z), this.transform.rotation);
		child.GetComponent<FixedJoint> ().connectedBody = this.GetComponent<Rigidbody>();
		child.transform.localScale = transform.lossyScale;
		child.transform.parent = transform;
		tempInfo.thisCoord = new CoordStruct (-1, 0);
		tempInfo.parentCoord = new CoordStruct (0, 0);
		tempInfo.chance = this.chance;
		tempInfo.chanceDec = this.chanceDec;
		tempInfo.level = this.level;

		child.GetComponent<BlockCreation> ().spawn (tempInfo);
	}
	public void createBlockNorth(BlockVariables blockInfo){
		tempInfo = blockInfo;
		//Debug.Log ("hashed already:  " + new CoordStruct(0,1).hashableInt());
		coordMap.Add(new CoordStruct(0,1).hashableInt(),0);
		child = (GameObject)Instantiate(block, new Vector3(this.transform.position.x,0, this.transform.position.z+ this.transform.lossyScale.z), this.transform.rotation);
		child.GetComponent<FixedJoint> ().connectedBody = this.GetComponent<Rigidbody>();
		child.transform.localScale = transform.lossyScale;
		child.transform.parent = transform;
		tempInfo.thisCoord = new CoordStruct (0, 1);
		tempInfo.parentCoord = new CoordStruct (0, 0);
		tempInfo.chance = this.chance;
		tempInfo.chanceDec = this.chanceDec;
		tempInfo.level = this.level;
		child.GetComponent<BlockCreation> ().spawn (tempInfo);

	}
	public void createBlockSouth(BlockVariables blockInfo){
		tempInfo = blockInfo;
		//Debug.Log ("hashed already:  " + new CoordStruct(0, -1).hashableInt());
		coordMap.Add(new CoordStruct(0, -1).hashableInt(),0);
		child = (GameObject)Instantiate(block, new Vector3(this.transform.position.x,0, this.transform.position.z- this.transform.lossyScale.z), this.transform.rotation);
		child.GetComponent<FixedJoint> ().connectedBody = this.GetComponent<Rigidbody>();
		child.transform.localScale = transform.lossyScale;
		child.transform.parent = transform;
		tempInfo.thisCoord = new CoordStruct (0, -1);
		tempInfo.parentCoord = new CoordStruct (0, 0);
		tempInfo.chance = this.chance;
		tempInfo.chanceDec = this.chanceDec;
		tempInfo.level = this.level;
		child.GetComponent<BlockCreation> ().spawn (tempInfo);
	}


}
