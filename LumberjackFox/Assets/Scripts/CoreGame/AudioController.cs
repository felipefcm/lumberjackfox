using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour 
{
	private AudioClip lastFramePlayingAudioClip = new AudioClip();
	
	public void playAudio(AudioClip clip)
	{
		if(clip == null)
		{
			audio.Stop();
			return;
		}
		
		if(audio.isPlaying)
		{
			if(lastFramePlayingAudioClip == clip)
			{
				//do nothing
			}
			else
			{
				audio.Stop();
				audio.clip = clip;
				audio.Play();
			}
			lastFramePlayingAudioClip = clip;
		}
		else
		{
			audio.clip = clip;
			audio.Play();	
		}
	}
}
