using UnityEngine;
using System.Collections;

public class WeaponBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<Collider>().enabled)
			GetComponent<Collider>().enabled = false;
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Enemy")){
			if(other.transform.parent.GetComponent<AIController>() != null)
				other.transform.parent.GetComponent<AIController>().Hit();

			//Camera.main.transform.parent.GetComponent<Animation>().Play();
			
		}
		
	}
	
	
}
