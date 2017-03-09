using UnityEngine;
using System.Collections;

public class SendMessageButtonComponent : MonoBehaviour
{
	#region Attributes
	public SendMessageOptions Options = SendMessageOptions.RequireReceiver;
	public GameObject target;
	public string message;
	public float delay = 0.0f;
	public string parameter;
	private float elapsed = 0;
	private ButtonBehaviour button;
	#endregion
	
	#region Monobehaviour
	void Start( )
	{
		button = this.transform.parent.gameObject.GetComponent<ButtonBehaviour>( ) as ButtonBehaviour;
		if( !button )
			Debug.LogError("Could not find the button");
		else if( parameter == null || parameter.Equals( "" ) )
				parameter = button.tag;
	}
	
	void Update( )
	{
		if( elapsed > 0 )
		{
			elapsed -= Time.deltaTime;
			if( elapsed < 0 )
			{
				Action();
			}
		}
	}
	#endregion
	
	public void OnActivation ( )
	{
		if( delay <= 0 )
		{
			Action();
		}
		else
		{
			if( elapsed <= 0 )
				elapsed = delay;
		}	
	}
	
	private void Action( )
	{
		if ( !( message.Trim( ) == "" ) ) 
		{
			if( target != null )
			{
				//Debug.Log("Action");
				target.SendMessage(message, parameter, Options);
			}
			else
			{
				SendMessageUpwards(message, parameter, Options);
			}
		}
	}
}
