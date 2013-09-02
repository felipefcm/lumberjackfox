
using UnityEngine;

using System.Collections;

public class Cutscene : MonoBehaviour 
{
	public MovieTexture m_VideoFile;
	
	private float m_CurrentTime;

	void Start()
	{
		m_CurrentTime = 0;

		m_VideoFile = renderer.material.mainTexture as MovieTexture;
		audio.clip = m_VideoFile.audioClip;

		audio.Play();
		m_VideoFile.Play();
	}

	void Update()
	{
		m_CurrentTime += Time.deltaTime;

		if (!m_VideoFile.isPlaying)
		{
			if(Application.loadedLevel < Application.levelCount - 1)
            {
				Application.LoadLevel(Application.loadedLevel + 1);
            }
			else
				Application.Quit();
		}
	}
}
