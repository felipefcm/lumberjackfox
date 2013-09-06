using UnityEngine;

using System;
using System.Collections;

public class EndStageManager : ScreenManager
{
	#region Attributes
	//public float RESULT_TIME;
	//public int TOTAL_STARS;
	//public int TOTAL_LUMBER;
	//public float STAGE_SCORE;
	
	public LabelBehaviour lumberLabel;
	//public LabelBehaviour starsLabel;
	public LabelBehaviour timeLabel;
	public LabelBehaviour scoreLabel;
	
	private float resultTime;
	private int totalStars;
	private int totalLumber;
	private float stageScore;
	
	private string nextLevel;
	#endregion
	
	public static string ToTime( float time )
	{
		TimeSpan t = TimeSpan.FromSeconds( time );

		string answer = string.Format("{0:D2}:{1:D2}:{2:D2}", 
		    			t.Hours, 
		    			t.Minutes, 
		    			t.Seconds);
		
		return answer;
	}
	
	public void OnMenu( )
	{
		//Debug.Log( " MENU " );
		Application.LoadLevel ( "MainMenuScene" );
	}
	
	public void OnNext( )
	{
		//Debug.Log( " NEXT " );
		Application.LoadLevel ( nextLevel );
	}

	public float ResultTime
	{
		get
		{
			return this.resultTime;
		}
		
		set
		{
			resultTime = value;
		}
	}

	public float StageScore
	{
		get
		{
			return this.stageScore;
		}
		
		set
		{
			stageScore = value;
		}
	}

	public int TotalLumber
	{
		get
		{
			return this.totalLumber;
		}
		
		set
		{
			totalLumber = value;
		}
	}

	public int TotalStars
	{
		get
		{
			return this.totalStars;
		}
		
		set
		{
			totalStars = value;
		}
	}
	#region Monobehaviour
	void Awake( )
	{
		base.Awake( );
		
		//totalStars = TOTAL_STARS;
		totalLumber = ScoreController.amountCoins;//TOTAL_LUMBER;
		resultTime = ScoreController.totalTimePass;//RESULT_TIME;
		stageScore = ScoreController.CalculateScore( ); //STAGE_SCORE;
		nextLevel = this.GetNextLevel( ScoreController.currentLevel );
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
	
	private void SwitchState( ScreenState newState )
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
				lumberLabel.LabelText = totalLumber.ToString( );
				//starsLabel.LabelText = totalStars.ToString( );
				timeLabel.LabelText = ToTime( resultTime );
				scoreLabel.LabelText = ( ( int ) stageScore ).ToString( );
			
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
	
	private string GetNextLevel( string currentLevel )
	{
		switch( currentLevel )
		{
			case "Tutorial":
			{
				PlayerPrefs.SetInt( currentLevel, 1 );
				return "Cutscene1";
			}
			break;
			
			case "Chapter2":
			{
				PlayerPrefs.SetInt( currentLevel, 1 );
				return "Chapter3";
			}
			break;
			
			case "Chapter3":
			{
				PlayerPrefs.SetInt( currentLevel, 1 );
				return "Chapter4";
			}
			break;
			
			case "Chapter4":
			{
				PlayerPrefs.SetInt( currentLevel, 1 );
				return "Cutscene3";
			}
			break;
			
			default:
			{
				return "MainMenuScene";
			}
			break;
		}
	}
}
