using UnityEngine;
using System.Collections;

public class State_Idle : StateMachineBehaviour
{
	PlayerMovement player;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		this.player = animator.gameObject.GetComponentInParent<PlayerMovement>();
		this.player.canMove = true;
		this.player.canAttack = true;

		animator.SetBool("Attack0", false);
		animator.SetBool("Attack1", false);
		animator.SetBool("Attack2", false);
	}
	
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if(this.player.canAttack)
		{
			if(this.player.inputAttack)
			{
				this.player.attackIsRunning = true;
				animator.SetBool("Attack0", true);
			}
		}

		if(this.player.inputGuard)
		{
			animator.SetBool("Guard", true);
		}
	}
}
