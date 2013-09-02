using UnityEngine;

using System;
using System.Collections;

public class ScreenManager : MonoBehaviour
{
	public enum ScreenState
	{
		INIT,
		START,
		IDDLE,
		END,
		NULL
	}
	
	#region Attributes
	protected ScreenState currentState;
	protected ScreenState lastState;
	#endregion
	
	#region Monobehaviour
	protected void Awake( )
	{		
		currentState = ScreenState.INIT;
		lastState = ScreenState.NULL;
	}
	#endregion
	
	protected void SwitchState( ScreenState newState )
	{
		lastState = currentState;
		currentState = newState;
	}
	
	protected void ScreenMachine( )
	{
		switch( currentState )
		{
			case ScreenState.INIT:
			{
			}
			break;
			
			case ScreenState.START:
			{
				
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
