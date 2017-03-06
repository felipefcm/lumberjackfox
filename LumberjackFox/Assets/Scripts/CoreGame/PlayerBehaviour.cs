using UnityEngine;
using System.Collections;


public class PlayerBehaviour : MonoBehaviour {
	
	
	private CharacterMotor motor;
	private PlatformInputController inputPlayer;
	private GameController gameController;
	private CharacterController characterController;
	
	private WeaponBehaviour weapon;
	
	public float attackRate;
	private float currentRateAttack;
	
	public float minPositionToDie;
	
	public Animator animatorPlayer;
	
	public float runSpeedMultiply;
	private bool running = false;
	private bool canPlayJumpAnimation;
	
	public int life = 1;
	private float curretTimeToCancelSuperJump;
	
	private bool isGroundedAcc;
	
	//sounds
	public AudioClip walkingAudio;
	public AudioClip runingAudio;
	public AudioClip jumpAudio;
	public AudioClip landingAudio;
	public AudioClip attackHitAudio;
	public AudioClip attackMissAudio;
	public AudioClip boingAudio;
	public AudioClip death;
	private AudioController _audioController;

	public AudioController audioController {
		get {
			return this._audioController;
		}
		set {
			_audioController = value;
		}
	}
	
	private bool playDeathOnlyOneTime = false;
	
	private bool superJump = false;
	
	private float timeToPlayBoing = 0.8f;
	private float timeToPlayBoingAcc;
	// Use this for initialization
	void Start () {
		
		motor = GetComponent<CharacterMotor>();
		characterController = GetComponent<CharacterController>();
		inputPlayer = GetComponent<PlatformInputController>();
		gameController = GameController.instance;
		weapon = GetComponentInChildren<WeaponBehaviour>();
		animatorPlayer = GetComponentInChildren<Animator>();
		animatorPlayer.SetLayerWeight(1,1);
		animatorPlayer.SetLayerWeight(2,1);
		audioController = FindObjectOfType(typeof(AudioController)) as AudioController;
		playDeathOnlyOneTime = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(gameController.currentState == GameState.WAIT_DIALOG || gameController.currentState == GameState.PAUSE){
			motor.enabled = false;
			inputPlayer.canControl = false;
			return;
		}
		
		
		if(gameController.currentState != GameState.PLAY && gameController.currentState != GameState.START)
		{
			if(gameController.currentState != GameState.WIN && gameController.currentState != GameState.WAIT_DIALOG){
				animatorPlayer.SetBool("die",true);
				motor.enabled = false;
				
				
				if(!playDeathOnlyOneTime)
				{
					audioController.GetComponent<AudioSource>().PlayOneShot(death);
					playDeathOnlyOneTime = true;
				}
			}
			
		}
		else{
			animatorPlayer.SetBool("die",false);
			motor.enabled = true;
			inputPlayer.canControl = true;
			
			Vector3 newPosition = transform.position;
			newPosition.z = gameController.platformZAxi;
			
			if(Input.GetKey(KeyCode.LeftControl)){
				Attack();
			}
			else
				animatorPlayer.SetInteger("attack",0);
			
			if(currentRateAttack < attackRate+1)
				currentRateAttack += Time.deltaTime;
			
			if(transform.position.y <= minPositionToDie)
				gameController.ChangeState(GameState.DIE);
			
			if(Input.GetKey(KeyCode.LeftShift)){
				running = true;
				motor.inputMoveDirection *= runSpeedMultiply;
			}
			else
				running = false;
			
			if(motor.jumping.baseHeight > 1){
				curretTimeToCancelSuperJump  += Time.deltaTime;
				if(curretTimeToCancelSuperJump > 0.2f){
					curretTimeToCancelSuperJump = 0;
					motor.jumping.baseHeight = 1;
				}
			}
			//Sound
			if(characterController.isGrounded)
			{
				if(motor.inputMoveDirection != Vector3.zero)
				{
					if(!running)
					{
						audioController.playAudio(walkingAudio);
					}
					else
					{
						audioController.playAudio(runingAudio);
					}
					
				}
				else
				{
					audioController.playAudio(null);
				}
				
				if(isGroundedAcc != characterController.isGrounded)
				{
					audioController.GetComponent<AudioSource>().Stop();
					audioController.GetComponent<AudioSource>().PlayOneShot(landingAudio);
				}
				
			}
			else
			{
				if(isGroundedAcc != characterController.isGrounded)
				{
					audioController.GetComponent<AudioSource>().Stop();
					if(superJump)
					{
						audioController.GetComponent<AudioSource>().PlayOneShot(boingAudio);
					}
					else
					{
						audioController.GetComponent<AudioSource>().PlayOneShot(jumpAudio);
					}
				}
			}
			if(isGroundedAcc != characterController.isGrounded)
			{
				timeToPlayBoingAcc = Time.time;
			}
			if(Time.time - timeToPlayBoingAcc > timeToPlayBoing)
			{
				superJump = false;
			}
			
			isGroundedAcc = characterController.isGrounded;
			
			//animation
			if(characterController.isGrounded){
				
				if(motor.inputMoveDirection == Vector3.zero){
					//iddle
					
					animatorPlayer.SetBool("running",false);
					animatorPlayer.SetBool("walking",false);
					animatorPlayer.SetBool("jump",false);
					animatorPlayer.SetBool("iddle",true);
				}
				else{
					if(!running){
						//walk
						animatorPlayer.SetBool("walking",true);
						animatorPlayer.SetBool("running",false);
						animatorPlayer.SetBool("jump",false);
						animatorPlayer.SetBool("iddle",false);
						
					}
					else{
						animatorPlayer.SetBool("walking",false);
						animatorPlayer.SetBool("running",true);
						animatorPlayer.SetBool("jump",false);
						
						animatorPlayer.SetBool("iddle",false);
						//run
					}
				}

			}
			else if(!characterController.isGrounded){
				//jump one time
				animatorPlayer.SetBool("running",false);
				animatorPlayer.SetBool("walking",false);
				animatorPlayer.SetBool("jump",true);
				animatorPlayer.SetBool("iddle",false);
				
				
				
			}

		}
		
	
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Coin"))
		{
			gameController.AddCoin();
			
			other.gameObject.AddComponent<CoinController>();
			other.gameObject.AddComponent<AudioSource>();
			other.GetComponent<AudioSource>().playOnAwake = false;
			CoinController coin = other.GetComponent<CoinController>();
			coin.playAudioWithDestroy(attackHitAudio);
		}
		
		if(other.gameObject.CompareTag("EndGame") && gameController.CanFinishLevel()){
			if(SpecialLevelEvent.instance.dialogsEvent.Length > 0)
				gameController.ChangeState(GameState.WAIT_DIALOG);
			else
				gameController.ChangeState(GameState.WIN);
		}
	}
	
	void OnTriggerExit(Collider other){
		motor.jumping.baseHeight = 1;
	}
	
	
	void OnCollisionEnter(Collision other){
		if(other.collider.GetComponent<PlatformBase>() != null){
			motor.enabled = false;
			inputPlayer.enabled = false;
		}
		
	}
	
	void OnCollisionExit(Collision other){
		if(other.collider.GetComponent<PlatformBase>() != null){
			motor.enabled = true;
			inputPlayer.enabled = true;
		}
		
		
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit){
		if(hit.gameObject.GetComponent<PlatformBase>() != null){
			hit.gameObject.GetComponent<PlatformBase>().PlayerCollide();
		}
	}
	
	void Attack(){
		if(currentRateAttack > attackRate){
			animatorPlayer.SetInteger("attack",Random.Range(1,3));
			weapon.GetComponent<Collider>().enabled = true;
			audioController.GetComponent<AudioSource>().PlayOneShot(attackMissAudio);
			currentRateAttack = 0;
		}
	}
	
	public void SupperJump(float jumpForce){
		motor.jumping.baseHeight *= jumpForce;
		motor.requestJump = true;
		superJump = true;
	}
	
	public void ApplyDamage(){
		life--;
		if(life == 0){
			gameController.ChangeState(GameState.DIE);
		}
	}
}
