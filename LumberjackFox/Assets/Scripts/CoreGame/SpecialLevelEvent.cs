using UnityEngine;
using System.Collections;

public class SpecialLevelEvent : MonoBehaviour {
	
	public Dialog[] dialogsEvent;
	
	[System.Serializable]
	public class Dialog{
		public Texture imageDialog;
		public string textDialog;
	}
	
	public Renderer rendererImageDialog;
	public TextMesh textMeshDiloag;
	
	private int currentDialog = -1;
	
	public static SpecialLevelEvent instance;
	
	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void ShowNextDialog(){
		if(currentDialog+1 < dialogsEvent.Length){
			currentDialog++;
			
			rendererImageDialog.material.mainTexture = dialogsEvent[currentDialog].imageDialog;
			textMeshDiloag.text = dialogsEvent[currentDialog].textDialog;

		}
		else{
			GameController.instance.ChangeState(GameState.WIN);
		}
	}
	
}
