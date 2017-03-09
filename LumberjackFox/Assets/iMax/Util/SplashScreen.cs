using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class SplashScreen : MonoBehaviour
{
	public bool autoLoadNextLevel;
	public Texture[] splashTextures;
	public int waitSeconds;
	
	private Texture currentTexture;
	private int currentTextureIndex;
	private float ttl;
	
	// Use this for initialization
	void Start ()
	{
		ttl = waitSeconds;
		
		//FIXME: Need think another aproach
		if( ttl <= 0 )
			ttl = 1.0f; //HACK: Set for 1sec min to Avoid Load next Level straitgh way on Editor
		
		currentTextureIndex = 0;
		if( splashTextures.Length > 0 )
			currentTexture = splashTextures[currentTextureIndex];
		
		
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		ttl -= Time.deltaTime;
		if( ttl <= 0 )
		{
			ttl = waitSeconds;
			currentTextureIndex++;
			if( currentTextureIndex < splashTextures.Length )
				currentTexture = splashTextures[currentTextureIndex];
			else
			if( autoLoadNextLevel )
				Application.LoadLevel(Application.loadedLevel+1);
		}
	}
	
	void OnGUI()
	{
		Rect rect;
		if( currentTexture != null )
		{
			rect = new Rect(0.0f, 0.0f, Screen.width, Screen.height);
			GUI.DrawTexture(rect, currentTexture, ScaleMode.StretchToFill);
		}
		
		//rect = new Rect(5.0f, 5.0f, 100.0f, 20.0f);
		//GUI.TextField(rect, "V. "+Application.unityVersion);		
	}	
}

