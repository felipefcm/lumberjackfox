
using UnityEngine;

using System.Collections;

public class FallPlatform : PlatformBase
{
	public	AnimationCurve  m_FallCurve;
	public  float   m_SecureTime;
	public  float   m_MaxTime = 4.0f;

	private	float   m_FallSpeed = 1.5f;
	private float   m_ElapsedTime;
	private bool    m_StartCount;

	public override bool Init()
	{
		m_ElapsedTime = 0;
		m_StartCount = false;

		return true;
	}

	public override void DoMove()
	{
		if(!m_StartCount)
			return;

		m_ElapsedTime += Time.deltaTime;

		if(m_ElapsedTime < m_SecureTime)
			return;

		float currentFallSpeed = m_FallSpeed * m_FallCurve.Evaluate(m_ElapsedTime / m_MaxTime);

		transform.position = transform.position - new Vector3(0, currentFallSpeed, 0);
	}

	public override void Reset()
	{
		m_InitialPosition = transform.position;

		Init();
	}

	public override void PlayerCollide()
	{
		if(transform.position.y < GameController.instance.player.transform.position.y)
			m_StartCount = true;
	}

	public void OnCollisionEnter(Collision col)
	{

	}
}
