using UnityEngine;
using System.Collections;

public class UITurboAnimation_Show : StateMachineBehaviour
{
	//Sorry for this class :( it triggers the end of its animation and disables its gameobject

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
			animator.gameObject.SetActive(false);
	}
}
