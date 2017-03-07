
using UnityEngine;

using System.Collections;

public class Cutscene : MonoBehaviour 
{
	public MovieTexture m_VideoFile;
	public string nextLevelName;
	
	private float m_CurrentTime;

	void Start()
	{
		m_CurrentTime = 0;

		m_VideoFile = GetComponent<Renderer>().material.mainTexture as MovieTexture;
		GetComponent<AudioSource>().clip = m_VideoFile.audioClip;

		GetComponent<AudioSource>().Play();
		m_VideoFile.Play();
	}

	void Update()
	{
		m_CurrentTime += Time.deltaTime;

		if (!m_VideoFile.isPlaying || (m_CurrentTime > 2.0f && Input.GetKeyDown(KeyCode.Return)))
		{
			Application.LoadLevel(nextLevelName);
		}
	}
}
