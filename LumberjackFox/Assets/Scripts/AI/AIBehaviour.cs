using UnityEngine;
using System.Collections;

public class AIBehaviour : AIController
{	
	public enum ENEMY_TYPE
	{
		JUMPER,
		JUMPER_PATROL,
		RUNNER,
		WALKER,
		PATROL,
		STATIC,
		STATIC_IMORTAL
	}
	
	public ENEMY_TYPE myType;
	
	private Vector3 initialPosition;
	private bool patrolSignal;
	private Collider[] patrolColliders;
	public float runnerDistanceFromPlayer;
	public float runnerSpeedMultiply;
	
	void Awake()
	{
		base.Awake();
	}
	
	void Start ()
	{
		base.Start();
		EnemyStart();
		initialPosition = this.transform.position;
	}
	
	void Update () 
	{
		base.Update();
		EnemyUpdate();
	}
	
	private void EnemyStart()/////////////////////////////////////START/////////////////////////////////////
	{
		switch(myType)
		{
			case ENEMY_TYPE.JUMPER_PATROL:
			{
				CreatePatrolColliders();
			}
			break;
			
			case ENEMY_TYPE.PATROL:
			{
				CreatePatrolColliders();
			}
			break;
			
			case ENEMY_TYPE.STATIC_IMORTAL:
			{
				MoveToState(AIController.ENEMY_STATE.IMORTAL);
			}
			break;
		}
	}
	
	private void EnemyUpdate()////////////////////////////////////////UPDATE////////////////////////////////
	{	
		switch(myType)
		{
			case ENEMY_TYPE.JUMPER:
			{
				if(currentState == AIController.ENEMY_STATE.ALIVE)
				{
					if(characterController.isGrounded)
					{
						jump = true;
					}
					else
					{
						jump = false;
					}
				
					moveVector.x = -moveController.movement.maxSidewaysSpeed;
					
					moveController.inputJump = jump;
					moveController.inputMoveDirection = moveVector;
				}
			}
			break;
			
			case ENEMY_TYPE.JUMPER_PATROL:
			{
				if(currentState == AIController.ENEMY_STATE.ALIVE)
				{
					if(characterController.isGrounded)
					{
						jump = true;
					}
					else
					{
						jump = false;
					}
				
					if(patrolSignal)
					{
						moveVector.x = -moveController.movement.maxSidewaysSpeed;
					}
					else
					{
						moveVector.x = moveController.movement.maxSidewaysSpeed;
					}
					
					moveController.inputJump = jump;
					moveController.inputMoveDirection = moveVector;
				}
			}
			break;
			
			case ENEMY_TYPE.RUNNER:
			{
				if(currentState == AIController.ENEMY_STATE.ALIVE)
				{
					if(GameController.instance.player.gameObject.transform.position.x - this.transform.position.x < runnerDistanceFromPlayer)
					{
						moveVector.x = -moveController.movement.maxSidewaysSpeed*runnerSpeedMultiply;
						moveController.inputMoveDirection = moveVector;
					}
					else
					{
						moveVector.x = -moveController.movement.maxSidewaysSpeed;
						moveController.inputMoveDirection = moveVector;
					}
				}
			}
			break;
			
			case ENEMY_TYPE.WALKER:
			{
				if(currentState == AIController.ENEMY_STATE.ALIVE)
				{
					moveVector.x = -moveController.movement.maxSidewaysSpeed;
					moveController.inputMoveDirection = moveVector;
				}
			}
			break;
			
			case ENEMY_TYPE.PATROL:
			{
				if(currentState == AIController.ENEMY_STATE.ALIVE)
				{
				
					if(enemyAnimator!= null){
						enemyAnimator.SetBool("walk",true);
						enemyAnimator.SetBool("die",false);
					}
					if(patrolSignal)
					{
						moveVector.x = -moveController.movement.maxSidewaysSpeed;
					}
					else
					{
						moveVector.x = moveController.movement.maxSidewaysSpeed;
					}
				
					moveController.inputMoveDirection = moveVector;
				}
			}
			break;
			
			case ENEMY_TYPE.STATIC:
			{
				moveVector = Vector3.zero;
				jump = false;
			}
			break;
			
			case ENEMY_TYPE.STATIC_IMORTAL:
			{
				moveVector = Vector3.zero;
				jump = false;
			}
			break;
		}
	}
	
	void OnDrawGizmos()
	{
		if(myType == ENEMY_TYPE.JUMPER_PATROL || myType == ENEMY_TYPE.PATROL)
		{
			if(Application.isPlaying)
			{
				Gizmos.color = new Color(0f,0f,1f,0.5f);
				Gizmos.DrawCube(initialPosition + patrolInitialPosition, new Vector3(1f,1f,1f));
				Gizmos.DrawCube(initialPosition + patrolFinalPosition, new Vector3(1f,1f,1f));
			}
			else
			{
				Gizmos.color = new Color(0f,0f,1f,0.5f);
				Gizmos.DrawCube(this.transform.position + patrolInitialPosition, new Vector3(1f,1f,1f));
				Gizmos.DrawCube(this.transform.position + patrolFinalPosition, new Vector3(1f,1f,1f));
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(myType == ENEMY_TYPE.JUMPER_PATROL || myType == ENEMY_TYPE.PATROL)
		{
			for (int i = 0; i < patrolColliders.Length; i++)
			{
				if(other == patrolColliders[i])
				{
					patrolSignal = !patrolSignal;
					TurnEnemy();
				}
			}
		}
	}
	
	public override void Hit ()
	{
		base.MoveToState(AIController.ENEMY_STATE.DEAD);
	}
	
	private void CreatePatrolColliders()
	{
		GameObject parent = new GameObject("PatrolColliders");
		parent.transform.position = Vector3.zero;
		parent.transform.eulerAngles = Vector3.zero;
	
		patrolColliders = new BoxCollider[2];
		
		GameObject collider0 = new GameObject("collider0");
		collider0.transform.position = Vector3.zero;
		collider0.transform.eulerAngles = Vector3.zero;
	
		GameObject collider1 = new GameObject("collider1");
		collider1.transform.position = Vector3.zero;
		collider1.transform.eulerAngles = Vector3.zero;
		
		collider0.transform.parent = parent.transform;
		collider1.transform.parent = parent.transform;
	
		collider0.AddComponent<BoxCollider>();
		collider1.AddComponent<BoxCollider>();
	
		patrolColliders[0] = collider0.GetComponent<Collider>();
		patrolColliders[1] = collider1.GetComponent<Collider>();
	
		patrolColliders[0].isTrigger = true;
		patrolColliders[1].isTrigger = true;
		
		patrolColliders[0].transform.localScale = new Vector3(0.1f,100f,100f);
		patrolColliders[1].transform.localScale = new Vector3(0.1f,100f,100f);
	
		patrolColliders[0].transform.position = new Vector3(transform.position.x + patrolInitialPosition.x,transform.position.y,transform.position.z);
		patrolColliders[1].transform.position = new Vector3(transform.position.x + patrolFinalPosition.x,transform.position.y,transform.position.z);
	}
	
	private void TurnEnemy()
	{
		this.transform.Rotate(new Vector3(0f,180f,0f));
	}
	
	public void EnemyAttack()
	{
		MoveToState(AIController.ENEMY_STATE.ATTACK);
	}
	
	public void EnemyStopAttack()
	{
		if(currentState != AIController.ENEMY_STATE.DEAD)
		{
			MoveToState(AIController.ENEMY_STATE.ALIVE);
		}
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) 
	{
		if(hit.gameObject.tag == "Player")
		{
			base.MoveToState(AIController.ENEMY_STATE.DEAD);
		}
	}
}