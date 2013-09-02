using UnityEngine;
using System.Collections;

public class RotatingPlatform : PlatformBase
{
    public Vector3  m_Center;
    public float    m_Radius;
    public float    m_Angle;
    public float    m_Speed;

    public override bool Init()
    {
        
        return true;
    }

    public override void DoMove()
    {
        m_Angle += m_Speed / 100.0f;
        transform.localPosition = new Vector3(m_Center.x + m_Radius * Mathf.Cos(m_Angle), m_Center.y + m_Radius * Mathf.Sin(m_Angle), transform.localPosition.z);
    }

    public override void Reset()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(m_Center, 0.2f);
        Gizmos.DrawWireSphere(transform.localPosition, m_Radius);
    }
}
