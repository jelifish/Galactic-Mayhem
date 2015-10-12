using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawn : MonoBehaviour {
	Dictionary<int,int> tempMap;
	public GameObject exteriorBlock;
	public void spawn(Dictionary<int,int> coordMap, KeyValuePair<int,int> thisCoord,int level, float chance, float chanceDec,GameObject parent){
		tempMap = coordMap;
		KeyValuePair<int,int> newCoord;
		if (!checkNorth(thisCoord)) {
			if(spawnRoll(chance)){
				newCoord = new KeyValuePair<int, int>(thisCoord.Key,thisCoord.Value);
				coordMap.Add(thisCoord.Key, thisCoord.Value + 1);
				spawn(coordMap, newCoord,level ++,chance - chanceDec, chanceDec, this.gameObject);
			}

		}
		if (!checkSouth(thisCoord)) {
			
		}
		if (!checkEast(thisCoord)) {
			
		}
		if (!checkWest(thisCoord)) {
			
		}
	}
	private bool spawnRoll(float chance)
	{
		return(chance > Random.Range(0,100));
	}


	public bool checkNorth(KeyValuePair<int,int> coord){
		return (coordCheck (coord.Key, coord.Value + 1));
	}
	public bool checkSouth(KeyValuePair<int,int> coord){
		return (coordCheck (coord.Key, coord.Value - 1));
	}
	public bool checkEast(KeyValuePair<int,int> coord){
		return (coordCheck (coord.Key + 1, coord.Value));
	}
	public bool checkWest(KeyValuePair<int,int> coord){
		return (coordCheck (coord.Key - 1, coord.Value));
	}

	public bool coordCheck(int x, int y){

		int value;
		if (!tempMap.TryGetValue(x, out value))
		{
			return false;
		}
		return value == y;
	}



}
