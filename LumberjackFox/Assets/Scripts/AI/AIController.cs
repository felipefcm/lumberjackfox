using UnityEngine;
using System.Collections;

public abstract class AIController : MonoBehaviour 
{
	public enum ENEMY_STATE
	{
		INITIAL,
		START,
		ALIVE,
		ATTACK,
		DEAD,
		IMORTAL
	}
	
	public ENEMY_STATE currentState;
	public ENEMY_STATE nextState;
	
	protected CharacterMotor moveController;
	protected CharacterController characterController;
	
	public Vector3 patrolInitialPosition;
	public Vector3 patrolFinalPosition;
	
	protected Vector3 moveVector;
	protected bool jump;
	protected float gravity;
	
	
	public Animator enemyAnimator;
	
	protected void Awake()
	{
		moveController = this.gameObject.GetComponent<CharacterMotor>();
		characterController = this.gameObject.GetComponent<CharacterController>();
	}
	
	protected void  Start ()
	{
		currentState = ENEMY_STATE.INITIAL;
		nextState = ENEMY_STATE.START;
		transform.localEulerAngles = Vector3.zero;
	}
	
	protected void Update () 
	{
		
		if(GameController.instance != null && GameController.instance.currentState != GameState.PLAY){
			if(moveController != null)
				moveController.enabled = false;
			if(characterController != null)
				characterController.enabled = false;
			return;
		}
		else{
			if(currentState != ENEMY_STATE.DEAD)
			{
				moveController.enabled = true;
				characterController.enabled = true;
			}
		}
			
		StartState();
		UpdateStates();
	}
	
	private void StartState()  //Start////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	{
		if( currentState == nextState ){return;}
		
		currentState = nextState;
		
		switch( nextState )
		{		
			case ENEMY_STATE.START:
			{

			}
			break;
			
			case ENEMY_STATE.ALIVE:
			{
				moveController.enabled = true;
				characterController.enabled = true;
				moveController.enabled = true;
				if(enemyAnimator != null)
					enemyAnimator.SetBool("attack",false);
			}
			break;
			
			case ENEMY_STATE.ATTACK:
			{
				if(enemyAnimator != null)
				{
					enemyAnimator.SetBool("attack",true);
				}
			}
			break;
			
			case ENEMY_STATE.DEAD:
			{
				moveController.inputJump = false;
				moveController.inputMoveDirection = Vector3.zero;
				moveController.enabled = false;
				characterController.enabled = false;
				if(enemyAnimator != null)
					enemyAnimator.SetBool("die",true);
				moveController.enabled = false;
			}
			break;
		}
	}
	
	private void UpdateStates()  //Update////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	{
		gravity = GameController.instance.player.GetComponent<CharacterMotor>().movement.gravity;
		
		
		if( currentState != nextState ){return;}
		
		switch(currentState)
		{	
			case ENEMY_STATE.START:
			{
				MoveToState(ENEMY_STATE.ALIVE);
			}
			break;
			
			case ENEMY_STATE.ATTACK:
			{
				
			}
			break;
			
			case ENEMY_STATE.DEAD:
			{
				DestroyImmediate(moveController);
				DestroyImmediate(characterController);
			}
			break;
		}
	}
	
	protected bool MoveToState(ENEMY_STATE st)
	{
		if( st != currentState )
		{
			nextState = st;
			return true;
		}
		return false;
	}
	
	public abstract void Hit();

}
