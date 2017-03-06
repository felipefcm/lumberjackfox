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
			this.GetComponent<Collider>().enabled = false;
		}
		else
		{
			this.GetComponent<Collider>().enabled = true;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other == GameController.instance.player.GetComponent<Collider>())
		{
			if(behaviour.currentState != AIController.ENEMY_STATE.DEAD)
			{
				behaviour.EnemyAttack();
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other == GameController.instance.player.GetComponent<Collider>())
		{
			if(behaviour.currentState != AIController.ENEMY_STATE.DEAD)
			{
				behaviour.EnemyStopAttack();
			}
		}
	}
}
