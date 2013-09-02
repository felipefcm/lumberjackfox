
using UnityEngine;

using System.Collections;

public abstract class PlatformBase : MonoBehaviour 
{
	public  bool    m_IsMoving;

	//velocity in pixels per frame
	public  float   m_Velocity = 10.0f;
	
	protected	Vector3		m_InitialPosition;
	public  Vector3		m_MoveVector;
	
	#region MONOS
	protected void Start()
	{
		m_InitialPosition = this.transform.position;
		
		if(!Init())
			Debug.LogError("Init falhou!!!");
	}
	
	protected void FixedUpdate()
	{
		if(GameController.instance != null && GameController.instance.currentState != GameState.PLAY)
			return;
		
		if(m_IsMoving)
			DoMove();
	}
	#endregion
	
	public virtual bool Init()
	{
		return true;
	}
	
	public abstract void DoMove();
	public abstract void Reset();

	public virtual void PlayerCollide()
	{
	}
}
