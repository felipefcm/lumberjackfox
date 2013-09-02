
using UnityEngine;

using System.Collections;

public class SuperJump : PlatformBase
{
	public float    m_JumpPower;
	
	void Update()
	{
		if(Vector3.Distance(GameController.instance.player.transform.position, transform.position) < 3.5f &&
			GameController.instance.player.GetComponent<CharacterMotor>().jumping.baseHeight == 1 &&
			GameController.instance.player.transform.position.y > transform.position.y)
		{
			if(Input.GetButton("Jump"))
			{
				GameController.instance.player.SupperJump(m_JumpPower);
			}
			else
			{
				GameController.instance.player.SupperJump(m_JumpPower/3);
			}
		}
	}
	
	public override void DoMove()
	{
		
	}
	
	public override void Reset ()
	{
		throw new System.NotImplementedException ();
	}
}
