
using UnityEngine;

using System.Collections;

public class DestructiveBehaviour : AIController 
{	
	public ParticleSystem   m_LeafParticleSystem;
	public bool activeTrigger = false;

	void Start()
	{
	    
	}
	
	void Update()
	{
	    
	}
	
	public override void Hit()
	{
		Instantiate(m_LeafParticleSystem.gameObject, transform.position, transform.rotation);
		if(activeTrigger)
			GetComponent<ActiveTriggerBehaviour>().ActiveTrigger();
		Destroy(gameObject);
	}
	
	
	
	
}
