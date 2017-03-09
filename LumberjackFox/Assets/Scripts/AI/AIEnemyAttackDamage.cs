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
			GameController.instance.player.ApplyDamage();
		}
	}
}