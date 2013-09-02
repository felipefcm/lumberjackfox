using UnityEngine;

using System;
using System.Collections;

public class CreditsManager : ScreenManager
{
	#region Attributes
	#endregion
	
	public void OnMenu( )
	{
		Debug.Log( " MENU " );
		Application.LoadLevel( "MainMenuScene" );
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
				SwitchState( ScreenManager.ScreenState.START );
			}
			break;
			
			case ScreenState.START:
			{
				SwitchState( ScreenManager.ScreenState.IDDLE );
			}
			break;
			
			case ScreenState.IDDLE:
			{
				
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
