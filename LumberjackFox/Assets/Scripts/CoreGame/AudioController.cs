using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour 
{
	private AudioClip lastFramePlayingAudioClip = new AudioClip();
	
	public void playAudio(AudioClip clip)
	{
		if(clip == null)
		{
			GetComponent<AudioSource>().Stop();
			return;
		}
		
		if(GetComponent<AudioSource>().isPlaying)
		{
			if(lastFramePlayingAudioClip == clip)
			{
				//do nothing
			}
			else
			{
				GetComponent<AudioSource>().Stop();
				GetComponent<AudioSource>().clip = clip;
				GetComponent<AudioSource>().Play();
			}
			lastFramePlayingAudioClip = clip;
		}
		else
		{
			GetComponent<AudioSource>().clip = clip;
			GetComponent<AudioSource>().Play();	
		}
	}
}
