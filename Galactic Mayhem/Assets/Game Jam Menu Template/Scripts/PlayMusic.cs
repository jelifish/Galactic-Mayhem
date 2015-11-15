using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class PlayMusic : MonoBehaviour {
	
	public AudioClip[] musicClips;					//Array of music clips to play
	public AudioMixerSnapshot volumeDown;			//Reference to Audio mixer snapshot in which the master volume of main mixer is turned down
	public AudioMixerSnapshot volumeUp;				//Reference to Audio mixer snapshot in which the master volume of main mixer is turned up


	private AudioSource musicSource;				//Reference to the AudioSource which plays music
	private AudioSource tempMusic;

	void Awake () 
	{
		//Get a component reference to the AudioSource attached to the UI game object
		musicSource = GetComponent<AudioSource> ();
		tempMusic = musicSource;
	}
	
	//Used if running the game in a single scene, takes an integer music source allowing you to choose a clip by number and play.
	public void PlaySelectedMusic(int musicChoice)
	{
		//Play the music clip at the array index musicChoice
		tempMusic.clip = musicClips[Random.Range(1,musicClips.Length)];
		while (tempMusic!= musicSource) {
			tempMusic.clip = musicClips[Random.Range(1,musicClips.Length)];
		}

		musicSource.clip = tempMusic.clip;

		//Play the selected clip
		musicSource.Play ();
		musicSource.loop = false;
		Invoke("PlayNextSong", musicSource.clip.length);
	}
	public void PlayNextSong()
	{
		PlaySelectedMusic (1);
	}

	//Call this function to very quickly fade up the volume of master mixer
	public void FadeUp(float fadeTime)
	{
		//call the TransitionTo function of the audioMixerSnapshot volumeUp;
		volumeUp.TransitionTo (fadeTime);
	}

	//Call this function to fade the volume to silence over the length of fadeTime
	public void FadeDown(float fadeTime)
	{
		//call the TransitionTo function of the audioMixerSnapshot volumeDown;
		volumeDown.TransitionTo (fadeTime);
	}
}
