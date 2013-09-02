using UnityEngine;
using System.Collections;

public class ActiveTriggerBehaviour : MonoBehaviour {
	
	public Collider trigger;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ActiveTrigger(){
		trigger.enabled = true;
	}
}
