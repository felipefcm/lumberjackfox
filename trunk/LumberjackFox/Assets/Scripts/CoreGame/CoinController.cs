using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {
	
	private bool checkAudioToDestroy =	false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(checkAudioToDestroy)
		{
			if(!audio.isPlaying)
			{
				DestroyImmediate(this.gameObject);
			}
		}
	}
	
	public void playAudioWithDestroy(AudioClip Audio)
	{
		gameObject.audio.clip = Audio;
		gameObject.audio.Play();
		Renderer[] rend = gameObject.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < rend.Length; i++) 
		{
			rend[i].enabled = false;
		}
		gameObject.collider.enabled = false;
		checkAudioToDestroy = true;
	}
}
