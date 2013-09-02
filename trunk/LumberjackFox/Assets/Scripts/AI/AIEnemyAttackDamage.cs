using UnityEngine;
using System.Collections;

public class AIEnemyAttackDamage : MonoBehaviour 
{
	private SphereCollider attackTrigger;
	public AIBehaviour AI;

	void Awake() 
	{
		attackTrigger = GetComponent<SphereCollider>();
	}
	
	void Update()
	{
		if(AI.currentState == AIController.ENEMY_STATE.DEAD)
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
			GameController.instance.player.ApplyDamage();
		}
	}
}