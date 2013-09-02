using UnityEngine;
using System.Collections;

public class LevelButtonBehaviour : ButtonBehaviour
{
	#region Attributes
	private string levelName;
	#endregion
	
	public string LevelName
	{
		set
		{
			levelName = value;
		}
		
		get
		{
			return levelName;
		}
	}
	
	#region Monobehaviour
	void Awake( )
	{
		levelName = "";
	}
	
	void Start( )
	{
		base.Start( );
		SendMessageButtonComponent sndMsgBtCpt = this.GetComponentInChildren<SendMessageButtonComponent>( )
			as SendMessageButtonComponent;
		
		if( sndMsgBtCpt )
		{
			sndMsgBtCpt.parameter = levelName;
			sndMsgBtCpt.target = this.transform.parent.parent.gameObject;
			// Debug.Log( this.transform.parent.parent );
		}
		else
		{
			Debug.Log( "SendMessageButtonComponent not found!" );
		}
		
		base.buttonLabel.text = levelName;
		
	}
	#endregion
}
