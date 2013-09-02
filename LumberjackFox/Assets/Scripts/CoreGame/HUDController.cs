using UnityEngine;
using System.Collections;

public class HUDController : MonoBehaviour {
	
	public TextMesh timeDisplay;
	public TextMesh coinsDisplay;
	public static HUDController instace;
	
	public GameObject pauseGame;
	public Renderer pauseButton;
	public Texture pauseTexture;
	public Texture playTexture;
	
	public GameObject youLose;
	

	// Use this for initialization
	void Start () {
		instace = this;
		pauseGame.SetActive(false);
		youLose.SetActive(false);
		pauseButton.material.mainTexture = pauseTexture;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(pauseGame.activeSelf && Input.GetKeyDown(KeyCode.Return)){
			Restart();
		}
	
	}
	
	public void RefreshHUD(float timeLevel, int coins){
		coinsDisplay.text = "x"+coins.ToString("D2");
		int minutes = (int)timeLevel/60;
		int seconds = (int)(timeLevel - minutes*60);
		timeDisplay.text = minutes.ToString("D2")+":"+seconds.ToString("D2");
				
	}
	
	public void ShowPause(){
		GameController.instance.PauseGame();
		
		if(GameController.instance.currentState != GameState.PLAY){			
			pauseGame.SetActive(false);
			pauseButton.material.mainTexture = pauseTexture;
		}
		else{
			pauseGame.SetActive(true);
			pauseButton.material.mainTexture = playTexture;
		}
	}
	
	void ExitGame(){
		Application.LoadLevel("LevelSelection");
	}
	
	void Restart(){
		
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void Die(){
		youLose.SetActive(true);
		youLose.animation.Play();
		pauseGame.SetActive(true);
		pauseButton.gameObject.SetActive(false);
	}
	
	
}
