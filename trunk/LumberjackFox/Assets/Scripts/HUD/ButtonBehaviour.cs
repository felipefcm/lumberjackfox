using UnityEngine;
using System.Collections;
using TouchScript.Events;
using TouchScript.Gestures;

[ExecuteInEditMode]
public class ButtonBehaviour : MonoBehaviour
{
	public enum ButtonState
	{
		START,
		ACTIVE,
		PRESSED,
		SELECTED,
		DISABLED,
		NULL
	}
	
	#region Attributes
	public SendMessageButtonComponent sndMsgBtCpt;
	
	//button materials
	public Material activeMat;
	public Material pressedMat;
	public Material selectedMat; //not in use
	public Material disabledMat;
	
	//collider responsible for interaction
	public Collider btCollider;
		
	//button text
	public TextMesh buttonLabel;
	
	//object renderer
	public MeshRenderer renderer;
	
	private ButtonState currentState;
	private ButtonState lastState;
	
	private bool buttonPressed;
	private bool buttonReleased;
	#endregion
	
	public void Enable(bool enabled)
	{
		if(enabled)
		{
			SwitchState(ButtonState.START);
		}
		else
		{
			SwitchState(ButtonState.DISABLED);
		}
	}
	
	public void OnButtonDown()
	{
		if(currentState == ButtonState.ACTIVE)
		{
			buttonPressed = true;
			buttonReleased = false;
		}
		else
		{
			Debug.LogWarning("This button cannot be pressed when in " + currentState.ToString());
		}
	}
	
	public void OnButtonUp()
	{
		if(currentState == ButtonState.PRESSED)
		{
			buttonPressed = false;
			buttonReleased = true;
		}
		else
		{
			Debug.LogWarning("This button cannot be released when in " + currentState.ToString());
		}
	}
	
	#region MonoBehaviour
	void Awake( )
	{
		currentState = ButtonState.ACTIVE;
		lastState = ButtonState.NULL;
	}
	
	// Use this for initialization	
	protected void Start ()
	{
		if ( GetComponent<TapGesture>( ) != null ) 
			GetComponent<TapGesture>( ).StateChanged += OnStateChanged;
		
		
	}
	
	// Update is called once per frame
	void Update ( )
	{
		StateMachine();
	}
	#endregion
	
	private void SwitchState(ButtonState newState)
	{
		lastState = currentState;
		currentState = newState;
	}
	
	private void StateMachine()
	{
		switch(currentState)
		{
			case ButtonState.START:
			{
				btCollider.enabled = true;
				renderer.material = activeMat;
				SwitchState(ButtonState.ACTIVE);
				
			}
			break;
			
			case ButtonState.ACTIVE:
			{
				if(buttonPressed)
				{
					renderer.material = pressedMat;
					SwitchState(ButtonState.PRESSED);
				}
			}
			break;
			
			case ButtonState.PRESSED:
			{
				if(buttonReleased)
				{
					renderer.material = activeMat;
					sndMsgBtCpt.OnActivation( );
					SwitchState(ButtonState.ACTIVE);
				}
			}
			break;
			
			case ButtonState.DISABLED:
			{
				btCollider.enabled = false;
				renderer.material = disabledMat;
			}
			break;
			
			case ButtonState.SELECTED:
			{
				renderer.material = selectedMat;
			}
			break;
		}
	}

	private void OnStateChanged(object sender, GestureStateChangeEventArgs e)
    {
		TapGesture target = sender as TapGesture;
		
        switch (e.State)
        {
			case Gesture.GestureState.Began:
			{
				OnButtonDown( );
			}
			break;
			
			case Gesture.GestureState.Changed:
			{
			}
			break;
				
			case Gesture.GestureState.Ended:
			{
				OnButtonUp( );
			}
			break;
			
			case Gesture.GestureState.Cancelled:
			{
				SwitchState( ButtonState.ACTIVE );
				buttonPressed = false;
				buttonReleased = false;
			}
			break;
        }
	}
}
