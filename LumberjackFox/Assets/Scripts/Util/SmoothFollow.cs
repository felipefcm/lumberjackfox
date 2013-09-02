using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {
	
	public Transform target;
		
	public float minHeightToFolow;
	
	private float startHeight;
	
	void Start(){
		startHeight = transform.position.y;
	}
	
	void LateUpdate () {
		if (!target)
			return;
		Vector3 newPosition = transform.position;
		newPosition.x = target.position.x;
		if(target.transform.position.y > minHeightToFolow)
			newPosition.y = Mathf.Lerp(transform.position.y, target.transform.position.y+10, 0.1f);
		else
			newPosition.y = Mathf.Lerp(transform.position.y, startHeight, 0.1f);
			
		
		transform.position = newPosition;
	}
}
