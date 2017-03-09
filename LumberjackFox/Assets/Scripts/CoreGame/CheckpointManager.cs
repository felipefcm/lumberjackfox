
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
	private Vector3 lastPosition;
	private float lastTimePassed;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	public void ResetPosition()
	{
		lastPosition = Vector3.zero;
		lastTimePassed = 0f;
	}

	public void SetPosition(Vector3 pos)
	{
		lastPosition = pos;
	}

	public void UpdateTimePassed()
	{
		if(GameController.instance.timePassed > lastTimePassed)
			lastTimePassed = GameController.instance.timePassed;
	}

	public Vector3 GetPosition()
	{
		return lastPosition;
	}

	public float GetLastTimePassed()
	{
		return lastTimePassed;
	}
}
