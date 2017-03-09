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
			if(!GetComponent<AudioSource>().isPlaying)
			{
				DestroyImmediate(this.gameObject);
			}
		}
	}
	
	public void playAudioWithDestroy(AudioClip Audio)
	{
		gameObject.GetComponent<AudioSource>().clip = Audio;
		gameObject.GetComponent<AudioSource>().Play();
		Renderer[] rend = gameObject.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < rend.Length; i++) 
		{
			rend[i].enabled = false;
		}
		gameObject.GetComponent<Collider>().enabled = false;
		checkAudioToDestroy = true;
	}
}
