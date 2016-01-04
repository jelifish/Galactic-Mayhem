using UnityEngine;
using System.Collections;

public class Timef : MonoBehaviour {
	public static Timef f = null;
	void Awake ()
	{
		f = this;
	}
	public void SlowTime(float multiplier){
//		Time.timeScale *= 1/multiplier;
	}
	public void SpeedTime(float multiplier){
//		Time.timeScale *= multiplier;
//		Time.fixedDeltaTime = 0.02F * Time.timeScale;
	}
	public void SlowTimeforDuration(float multiplier, float time){
//		SlowTime (multiplier);
//		StartCoroutine (Timer (multiplier, time));
	}
	IEnumerator Timer(float multiplier, float delay){
		yield return new WaitForSeconds(delay);
		SpeedTime (multiplier);
	}

	public static float saveTime = 0;
	public void FreezeTime(float time){
		saveTime = Time.timeScale;
		SlowTime (0);
		ResetTime();
	}
	public void ResetTime(){
		Time.timeScale = saveTime;
	}
}
