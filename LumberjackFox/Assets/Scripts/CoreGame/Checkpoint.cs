
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	private CheckpointManager manager;

	private void Start()
	{
		manager = FindObjectOfType<CheckpointManager>();
	}

	public void OnTriggerEnter(Collider other)
	{
		if(other.name != "Player")
			return;

		SaveCheckpointPosition();
	}

	private void SaveCheckpointPosition()
	{
		manager.SetPosition(transform.position);
		manager.UpdateTimePassed();
	}
}
