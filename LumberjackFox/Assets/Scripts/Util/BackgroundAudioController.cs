using UnityEngine;
using System.Collections;

public class BackgroundAudioController : MonoBehaviour {
	
	private AudioSource audioSrc;

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		
		BackgroundAudioController[] otherAudio = FindObjectsOfType(typeof(BackgroundAudioController)) as BackgroundAudioController[];
		
		if(otherAudio.Length > 1)
		{
			DestroyImmediate(this.gameObject);
		}
	}
	
	// Use this for initialization
	void Start () {
		audioSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(audioSrc.isPlaying)
		{
			if(	Application.loadedLevelName == "Cutscene1" ||
				Application.loadedLevelName == "Cutscene2" ||
				Application.loadedLevelName == "Cutscene3" ||
				Application.loadedLevelName == "Chapter1" ||
				Application.loadedLevelName == "Chapter2" ||
				Application.loadedLevelName == "Chapter3" ||
				Application.loadedLevelName == "Chapter4")
			{
				audioSrc.Stop();
			}
		}
		else
		{
				if(Application.loadedLevelName == "CreditsScene" ||
					Application.loadedLevelName == "EndStageScene" ||
					Application.loadedLevelName == "LevelSelection" ||
					Application.loadedLevelName == "MainMenuScene")
			{
				audioSrc.Play();
			}
		}
	}
}
