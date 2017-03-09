using UnityEngine;

using System;
using System.Collections;

public class MainMenuManager : ScreenManager
{
	#region Attributes
	public Animation fadeOut;
	public Animation glowPlay;
	#endregion
	
	public void OnPlay( )
	{
		if( currentState != ScreenManager.ScreenState.INIT &&
			currentState != ScreenManager.ScreenState.START )
		{
			Application.LoadLevel("LevelSelection");
		}
	}
	
	public void OnCredits( )
	{
		if( currentState != ScreenManager.ScreenState.INIT &&
			currentState != ScreenManager.ScreenState.START )
		{
			//Debug.Log( " CREDITS " );
			Application.LoadLevel("CreditsScene");
		}
	}
	
	public void OnQuit( )
	{
		if( currentState != ScreenManager.ScreenState.INIT &&
			currentState != ScreenManager.ScreenState.START )
		{
			//Debug.Log( " QUIT " );
			Application.Quit( );
		}
	}
	
	#region Monobehaviour
	void Awake( )
	{
		base.Awake( );
	}
	
	// Use this for initialization
	void Start( )
	{
	}
	
	// Update is called once per frame
	void Update( )
	{
		ScreenMachine( );
	}
	#endregion
	
	protected void ScreenMachine( )
	{
		switch( currentState )
		{
			case ScreenState.INIT:
			{
				fadeOut.Play( );
				SwitchState( ScreenManager.ScreenState.START );
			}
			break;
			
			case ScreenState.START:
			{
				if( !fadeOut.isPlaying )
				{
					glowPlay.Play( );
					SwitchState( ScreenManager.ScreenState.IDDLE );
				}
			}
			break;
			
			case ScreenState.IDDLE:
			{
				if( !fadeOut.isPlaying )
				{
					glowPlay.Play( );
				}
			}
			break;
			
			case ScreenState.END:
			{
				
			}
			break;
			
			case ScreenState.NULL:
			{
				
			}
			break;
			
			default:
			{
				Debug.Log( currentState );
			}
			break;
		}
	}
}
