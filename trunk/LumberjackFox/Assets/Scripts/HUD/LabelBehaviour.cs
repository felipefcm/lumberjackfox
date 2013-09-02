using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LabelBehaviour : MonoBehaviour
{
	public enum LabelState
	{
		START,
		ACTIVE,
		DISABLED,
		NULL
	};
	
	#region Attributes
	public string label;
	public bool activeBackground;
	
	private TextMesh labelText;
	private MeshRenderer bgRenderer;
	
	private LabelState currentState;
	private LabelState lastState;
	#endregion
	
	public string LabelText
	{
		set
		{
			labelText.text = label = value;	
		}
	}
	
	public void Disable()
	{
		SwitchState(LabelState.DISABLED);
	}
	
	#region Monobehaviour
	void Awake()
	{
		labelText = (TextMesh) GetComponentInChildren<TextMesh>();
		if(label.Equals(string.Empty))
			label = "";
		labelText.text = label;
		
		bgRenderer = GetComponentInChildren<MeshRenderer>();
		bgRenderer.enabled = activeBackground;
	}
	
	// Use this for initialization
	void Start ()
	{
		currentState = LabelState.START;
		lastState = LabelState.NULL;
	}
	
	// Update is called once per frame
	void Update ()
	{
		StateMachine();
	}
	#endregion
	
	private void SwitchState(LabelState newState)
	{
		lastState = currentState;
		currentState = newState;
	}
	
	private void StateMachine()
	{
		switch(currentState)
		{
			case LabelState.START:
			{
				SwitchState(LabelState.ACTIVE);
			}
			break;
			
			case LabelState.ACTIVE:
			{
				
			}
			break;
			
			case LabelState.DISABLED:
			{
				gameObject.SetActive(false);
			}
			break;
			
			case LabelState.NULL:
			{
				
			}
			break;
		}
	}

}
