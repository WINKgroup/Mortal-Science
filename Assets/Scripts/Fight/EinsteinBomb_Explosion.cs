using UnityEngine;
using System.Collections;

public class EinsteinBomb_Explosion : StateMachineBehaviour
{
	float timer;
	float exitTime;
	
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		this.exitTime = stateInfo.length;
		this.timer = 0;
	}
	
	
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		this.timer += Time.deltaTime;
		
		if(this.timer > this.exitTime)
			Destroy(animator.gameObject);
	}
}