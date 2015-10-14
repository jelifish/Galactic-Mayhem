using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockCreation : MonoBehaviour {



	void Update(){}










//	Dictionary<int,int> tempMap;
//	public int intHash;
//	public float chance;
//	private GameObject child;
//	public GameObject block;
//	public void spawn(BlockVariables blockInfo){
//		intHash = blockInfo.thisCoord.hashableInt ();
//		CoordStruct parentCoo = blockInfo.parentCoord;
//		CoordStruct thisCoo = blockInfo.thisCoord;
//		chance = blockInfo.chance;
//		tempMap = blockInfo.coordMap;
//		BlockVariables tempInfo = blockInfo;
//		CoordStruct newCoord;
//		tempInfo = blockInfo;
//		bool turntOn = false;
//		Debug.Log ("first: "+tempInfo.parentCoord.y);
//		Debug.Log ("second: "+(tempInfo.thisCoord.y - 1));
//		Debug.Log(thisCoo.x + ", " + thisCoo.y);
//		if (tempInfo.parentCoord.y != tempInfo.thisCoord.y +1 && !checkNorth(tempInfo.thisCoord)) {
//			if(spawnRoll(tempInfo.chance)){
//				tempInfo.parentCoord = tempInfo.thisCoord;
//				
//				child = (GameObject)Instantiate(block, new Vector3(this.transform.position.x,0, this.transform.position.z+ this.transform.lossyScale.z), this.transform.rotation);
//				child.GetComponent<FixedJoint> ().connectedBody = this.GetComponent<Rigidbody>();
//				child.transform.localScale = transform.lossyScale; // localscale?
//				child.transform.parent = transform;
//				
//				newCoord = new CoordStruct(tempInfo.thisCoord.x,tempInfo.thisCoord.y+1);
//				tempInfo.coordMap.Add(new CoordStruct (tempInfo.thisCoord.x, tempInfo.thisCoord.y + 1).hashableInt(), 0);
//
//				if(tempInfo.parentCoord.y == 0 && tempInfo.parentCoord.x ==1)turntOn = true;
//				tempInfo.level++;
//				tempInfo.chance -= tempInfo.chanceDec;
//				tempInfo.parent = this.gameObject;
//				tempInfo.thisCoord.y +=1;
//				child.GetComponent<BlockCreation> ().spawn (tempInfo);
//				
//			}
//			
//		}
//		if (turntOn) {
//			Debug.Log("wtf this gets reached??????????????????????????????????????");
//			Debug.Log(thisCoo.x + ", " + thisCoo.y);
//			Debug.Log(parentCoo.x + ", " + parentCoo.y);
//		}
//		//Debug.Log (blockInfo.thisCoord.x + "," + blockInfo.thisCoord.y);
//		if ( !checkSouth(thisCoo)) {
//
//			if(spawnRoll(100)){
//				tempInfo.parentCoord = tempInfo.thisCoord;
//				child = (GameObject)Instantiate(block, new Vector3(this.transform.position.x,0, this.transform.position.z- this.transform.lossyScale.z), this.transform.rotation);
//				child.GetComponent<FixedJoint> ().connectedBody = this.GetComponent<Rigidbody>();
//				child.transform.localScale = transform.lossyScale; // localscale?
//				child.transform.parent = transform;
//				
//				newCoord = new CoordStruct(tempInfo.thisCoord.x,tempInfo.thisCoord.y-1); ///////
//				tempInfo.coordMap.Add(new CoordStruct (tempInfo.thisCoord.x, tempInfo.thisCoord.y - 1).hashableInt(), 0);//////////
//				
//				tempInfo.level++;
//				tempInfo.chance -= tempInfo.chanceDec;
//				tempInfo.parent = this.gameObject;
//				tempInfo.thisCoord.y -=1;////////////////////
//
//				child.GetComponent<BlockCreation> ().spawn (tempInfo);
//				
//			}
//		}
//
//		tempInfo = blockInfo;
//		if (blockInfo.parentCoord.x != blockInfo.thisCoord.x +1 &&!checkEast(blockInfo.thisCoord)) {
//			
//		}
//		tempInfo = blockInfo;
//		if (blockInfo.parentCoord.x != blockInfo.thisCoord.x -1 &&!checkWest(blockInfo.thisCoord)) {
//			
//		}
//	}
//	private bool spawnRoll(float chance)
//	{
//		return(chance > Random.Range(0,100));
//	}
//	
//	
//	public bool checkNorth(CoordStruct coord){
//		return (coordCheck (coord.x, coord.y + 1));
//	}
//	public bool checkSouth(CoordStruct coord){
//		return (coordCheck (coord.x, coord.y - 1));
//	}
//	public bool checkEast(CoordStruct coord){
//		return (coordCheck (coord.x + 1, coord.y));
//	}
//	public bool checkWest(CoordStruct coord){
//		return (coordCheck (coord.x - 1, coord.y));
//	}
//	
//	public bool coordCheck(int x, int y){
////		Debug.Log ("map count" + tempMap.Count);
//		int value;
//		if (x == 0 && y == 0){
//			return true;
//	}
//		if (!tempMap.ContainsKey(new CoordStruct(x,y).hashableInt()))
//		{
//			return false;
//		}
//		return true;
//	}
//
//
//
//
//
//
//
//
//
//
//	//public int recur = 0;
//	// Use this for initialization
//	void Start () {
////		if (recur <=3)createBlockEast();
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
//
////	public void createBlockEast(){
////		GameObject child;
////		child = (GameObject)Instantiate(block, new Vector3(this.transform.position.x + this.transform.lossyScale.x,0, this.transform.position.z), this.transform.rotation);
////		child.GetComponent<BlockCreation>().recur = this.recur + 1;
////		child.GetComponent<FixedJoint> ().connectedBody = this.GetComponent<Rigidbody>();
////		child.transform.localScale = transform.lossyScale;
////		child.transform.parent = transform;
////	}
}
