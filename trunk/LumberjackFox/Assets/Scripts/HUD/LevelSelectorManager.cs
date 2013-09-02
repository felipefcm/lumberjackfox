using UnityEngine;
using System.Collections;

public class LevelSelectorManager : ScreenManager
{	
	public enum LEVEL
	{
		TUTORIAL,
		FIRST,
		SECOND,
		THIRD,
		FINAL
	}
	
	#region Attributes
	public string[] unlockedLevels;
	public float LEVEL_BT_STEP;
	
	public Transform levelBtPrefab;
	
	public Transform levelPlaceHolderUp;
	public Transform levelPlaceHolderDown;
	
	public int LINE_BREAK = 3;
	
	private int MAX_LEVELS = 5;
	
	private string tutorialLevel = "Tutorial";
	private string firstLevel = "Chapter2";
	
	private int numberOfLevels;
	#endregion
	
	public void OnLevelSelection( string levelName )
	{
		Application.LoadLevel( levelName );
	}
	
	public void OnMenu( )
	{
		Application.LoadLevel( "MainMenuScene" );
	}
	
	#region Monobehaviour
	void Awake( )
	{
		base.Awake( );
		LoadUnlockedLevels( );
	}
	
	// Use this for initialization
	void Start ( )
	{
		if( unlockedLevels == null || unlockedLevels.Length <= 0 || numberOfLevels <= 0)
		{
			Debug.Log( "Warning! Any level was found! Loading the two firsts!" );
			unlockedLevels = new string[ 2 ];
			unlockedLevels[ 0 ] = tutorialLevel;
			unlockedLevels[ 1 ] = firstLevel;
			
			numberOfLevels = 2;
		}
		
		InstantiateLevels( );
	}
	
	// Update is called once per frame
	void Update ()
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
	
	private void InstantiateLevels( )
	{
		for( int i = 0; i < numberOfLevels; ++i)
		{
			Transform levelButton = Instantiate( levelBtPrefab )
				as Transform;
			if( i < LINE_BREAK )
			{
				levelButton.transform.parent = levelPlaceHolderUp;
			}
			else
			{
				levelButton.transform.parent = levelPlaceHolderDown;
			}
			
			levelButton.transform.localPosition = Vector3.zero;
			if( i < LINE_BREAK )
			{
				levelButton.transform.position += ( Vector3.right ) * LEVEL_BT_STEP * i;	
			}
			else
			{
				levelButton.transform.position += ( Vector3.right ) * LEVEL_BT_STEP * ( i - LINE_BREAK );
			}
			
			
			LevelButtonBehaviour levelBehaviour = levelButton.GetComponent<LevelButtonBehaviour>( )
				as LevelButtonBehaviour;
			
			if( levelBehaviour )
			{
				levelBehaviour.LevelName = unlockedLevels[ i ];
			}
		}
	}
	
	private void LoadUnlockedLevels( )
	{
		unlockedLevels = new string[ MAX_LEVELS ];
		int defaultValue = 0;
		
		unlockedLevels[ numberOfLevels ] = "Tutorial";
		++numberOfLevels;
	
		unlockedLevels[ numberOfLevels ] = "Chapter2";
		++numberOfLevels;
			
		
		if( PlayerPrefs.GetInt( "Chapter3", defaultValue ) == 1 )
		{
			unlockedLevels[ numberOfLevels ] = "Chapter3";
			++numberOfLevels;
			
			Debug.Log( "Chapter3" );
		}
		
		if( PlayerPrefs.GetInt( "Chapter4", defaultValue ) == 1 )
		{
			unlockedLevels[ numberOfLevels ] = "Chapter4";
			++numberOfLevels;
			
			Debug.Log( "Chapter4" );
		}
		
		//Debug.Log( "Trololol" );
	}
}
