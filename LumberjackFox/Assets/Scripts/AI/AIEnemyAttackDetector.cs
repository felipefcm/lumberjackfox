using UnityEngine;
using System.Collections;

public class AIEnemyAttackDetector : MonoBehaviour {
	
	private SphereCollider attackTrigger;
	private AIBehaviour behaviour;

	void Awake() 
	{
		attackTrigger = GetComponent<SphereCollider>();
		behaviour = transform.parent.GetComponent<AIBehaviour>();
	}
	
	void Update()
	{
		if(behaviour.currentState == AIController.ENEMY_STATE.DEAD)
		{
			this.collider.enabled = false;
		}
		else
		{
			this.collider.enabled = true;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other == GameController.instance.player.collider)
		{
			if(behaviour.currentState != AIController.ENEMY_STATE.DEAD)
			{
				behaviour.EnemyAttack();
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other == GameController.instance.player.collider)
		{
			if(behaviour.currentState != AIController.ENEMY_STATE.DEAD)
			{
				behaviour.EnemyStopAttack();
			}
		}
	}
}
