using UnityEngine;
using System.Collections;

public class WeaponBehaviour : MonoBehaviour {

	public float timeEnabled = 1.0f;
	private float timeEnabledCounter = 0f;

	private Collider weaponCollider;

	// Use this for initialization
	void Start () {
		weaponCollider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {

		if(weaponCollider.enabled)
		{
			timeEnabledCounter += Time.deltaTime;

			if(timeEnabledCounter >= timeEnabled)
				weaponCollider.enabled = false;
		}
		else
		{
			timeEnabledCounter = 0;
		}
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Enemy")){
			if(other.transform.parent.GetComponent<AIController>() != null)
				other.transform.parent.GetComponent<AIController>().Hit();

			//Camera.main.transform.parent.GetComponent<Animation>().Play();
			
		}
		
	}
	
	
}
