using UnityEngine;
using System.Collections;

public enum GameState{
	NONE,
	START,
	PLAY,
	PAUSE,
	WIN,
	WAIT_DIALOG,
	DIE
}

public class GameController : MonoBehaviour {
	
	public GameState currentState{private set; get;}
	private GameState nextState = GameState.START;
	public PlayerBehaviour player;
	
	public int coinsToNextLevel;
	private int currentCoins;
	
	public float platformZAxi;
	
	public GameObject startGame;
	public GameObject endGame;
	
	public static GameController instance;
	
	private float timePassed;
	private float timeToDie;
	
	public int currentLevel;

	
	// Use this for initialization
	void Start () {
		instance = this;
		
		
		SpecialLevelEvent.instance.gameObject.SetActive(false);
		
		//Start Elements in z Axi;
			
			GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");				
			
			Vector3 newPosition = Vector3.zero;
			
			foreach(GameObject coin in coins){
				newPosition = coin.transform.position;
				newPosition.z = platformZAxi;
				coin.transform.position = newPosition;
			}
			
			PlatformBase[] platforms = GameObject.FindObjectsOfType(typeof(PlatformBase)) as PlatformBase[];	
			
			foreach(PlatformBase platform in platforms){
				newPosition = platform.transform.position;
				newPosition.z = platformZAxi;
				platform.transform.position = newPosition;
			}
		
			AIController[] enemies = GameObject.FindObjectsOfType(typeof(AIController)) as AIController[];	
			
			foreach(AIController enemy in enemies){
				newPosition = enemy.transform.position;
				newPosition.z = platformZAxi;
				enemy.transform.position = newPosition;
			}
			
			newPosition = endGame.transform.position;
			newPosition.z = platformZAxi;
			endGame.transform.position = newPosition;
			
			newPosition = startGame.transform.position;
			newPosition.z = platformZAxi;
			startGame.transform.position = newPosition;
			
			
			player.transform.position = startGame.transform.position;
			
			//
		
	}
	
	// Update is called once per frame
	void Update () {
		
		StateMachine();
		
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			
			SendMessageButtonComponent sndMsgBtCp = HUDController.instace.pauseButton.GetComponentInChildren<SendMessageButtonComponent>();
			if( sndMsgBtCp )
			{
				sndMsgBtCp.OnActivation();
			}

		}
		
		
	}
	
	void StateMachine(){
		if(nextState != currentState){
			currentState = nextState;
		}
		switch (currentState){
		case GameState.START:			
			nextState = GameState.PLAY;
		break;
		case GameState.PLAY:
			timePassed += Time.deltaTime;
			HUDController.instace.RefreshHUD(timePassed, currentCoins);

		break;
		case GameState.PAUSE:
		break;
		case GameState.DIE:
			timeToDie += Time.deltaTime;
			if(timeToDie > 3){
				HUDController.instace.Die();
				ChangeState(GameState.NONE);
			}
		break;
		case GameState.WAIT_DIALOG:{
			if(!SpecialLevelEvent.instance.gameObject.activeSelf){
				SpecialLevelEvent.instance.gameObject.SetActive(true);
				HUDController.instace.pauseButton.gameObject.SetActive(false);
				SpecialLevelEvent.instance.ShowNextDialog();
			}
		}
		break;
		case GameState.WIN:
			ScoreController.amountCoins = currentCoins;
			ScoreController.totalTimePass = timePassed;
			ScoreController.currentLevel = Application.loadedLevelName;
			Application.LoadLevel("EndStageScene");
		break;
		}
	
	}
	
	public void PauseGame(){
		if(currentState == GameState.PLAY){
			ChangeState(GameState.PAUSE);
			player.animatorPlayer.speed = 0;
		}
		else{
			ChangeState(GameState.PLAY);
			player.animatorPlayer.SetLayerWeight(2,0);
			player.animatorPlayer.speed = 1;
		}
	}
	
	public void AddCoin(){
		currentCoins++;
	}
	
	public int GetCurrentCoins(){
		return currentCoins;
	}
	
	public void ChangeState(GameState newState){
		nextState = newState;
	}
	
	public bool CanFinishLevel(){
		if(currentCoins >= coinsToNextLevel)
			return true;
		else
			return false;
	}
}
