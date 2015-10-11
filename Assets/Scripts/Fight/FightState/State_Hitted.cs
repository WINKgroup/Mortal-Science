using UnityEngine;
using System.Collections;

public class State_Hitted : StateMachineBehaviour
{
	PlayerMovement player;
	AIEnemy ai;
	
	float timer;
	float exitTime;
	
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		this.player = animator.gameObject.GetComponentInParent<PlayerMovement>();
		if(this.player.playerID == 0)
			this.ai = this.player.gameObject.GetComponentInParent<AIEnemy>();
		
		this.player.canMove = false;
		this.player.canAttack = false;
		
		this.exitTime = stateInfo.length;
		this.timer = 0;
	}
	
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		this.timer += Time.deltaTime;
		

		// Se preme il tasto parata
		if(true)
		{
			// GuardInput
		}
		
		if(this.timer > this.exitTime)
		{
			// Fine animazione
		}
	}
}
