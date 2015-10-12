using UnityEngine;
using System.Collections;

public class ReadyFight_EndAnimation : StateMachineBehaviour
{
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Game.Instance.arena.StartMatch();
	}
}
