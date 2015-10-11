using UnityEngine;
using System.Collections;

public class State_Attack2 : StateMachineBehaviour
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
		
		this.exitTime = stateInfo.length;
		this.timer = 0;
	}
	
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		this.timer += Time.deltaTime;
		
		
		if(this.player.canAttack)
		{
			this.NextAttack();
			
			if(this.player.inputAttack)
			{
				//this.player.combo = true;
			}
		}
		
		if(this.timer > this.exitTime)
		{
			if(this.player.combo)
			{
				this.player.combo = false;
				animator.SetBool("Attack3", true);
			}
			else
			{
				this.player.combo = false;
				this.player.attackIsRunning = false;
				animator.SetBool("Attack2", false);
			}
		}
	}
	
	void NextAttack()
	{
		if(this.player.playerID == 0 && !this.player.combo)
		{
			if(Random.value < this.ai.comboRate)
				this.player.inputAttack = true;
		}
	}
}
