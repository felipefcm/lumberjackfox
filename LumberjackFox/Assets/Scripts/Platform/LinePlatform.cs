
using UnityEngine;

using System.Collections;

public class LinePlatform : PlatformBase
{
	public  bool    m_PingPong;

	private float   m_Time;
	private float   m_TimeNeeded;
	private bool    m_Waiting;
    private float   m_WaitingTime;

	public override bool Init()
	{
		m_Time = 0;
        m_Waiting = true;

		return true;
	}

	public override void DoMove()
	{
		if(transform.position != m_InitialPosition + m_MoveVector)
		{
			//we need to move

			m_TimeNeeded = Mathf.Abs(Vector3.Distance(m_InitialPosition, m_InitialPosition + m_MoveVector)) / m_Velocity;

			float alpha = 1.0f - ((m_TimeNeeded - m_Time) / m_TimeNeeded);

			this.transform.position = Vector3.Lerp(m_InitialPosition, m_InitialPosition + m_MoveVector, alpha);

			m_Time += Time.deltaTime;
		}
		else
		{
			//arrived
			m_Time = 0;

            if(!m_Waiting)
                m_Waiting = !m_Waiting;

            if(m_Waiting)
            {
                m_WaitingTime += Time.deltaTime;

                if (m_WaitingTime > 0.4f)
                { 
                    m_Waiting = false;
                    m_WaitingTime = 0;
                }
				else
					return;
            }

			if(m_PingPong)
			{
				m_InitialPosition = transform.position;
				m_MoveVector = -m_MoveVector;
			}
		}
	}

	public override void Reset()
	{
		this.transform.position = m_InitialPosition;

		m_Time = 0;
		m_TimeNeeded = 0;

		Init();
	}
	
	private void OnDrawGizmosSelected()
	{
		if(!Application.isPlaying)
			Gizmos.DrawLine(transform.position, transform.position + m_MoveVector);
		else
			Gizmos.DrawLine(m_InitialPosition, m_InitialPosition + m_MoveVector);
	}
}
