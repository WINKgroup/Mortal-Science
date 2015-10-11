using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerFight : MonoBehaviour
{
	int tap;
	bool attacking = true;
	float lastTap;
	float timer;
	float timeForCombo = 0.3f;

	public bool slash1 = false;
	public bool slash2 = false;
	public bool slash3 = false;
	public int currentComboState = 1;
	
	private bool canAttack;
	private Rigidbody rb;
	private GravityObject feet;
	private Animator animator;
	private PlayerMovement player;
	

	void Awake()
	{
		this.rb 		= this.GetComponent<Rigidbody>();
		this.feet 		= this.GetComponentInChildren<GravityObject>();
		this.animator 	= this.GetComponentInChildren<Animator>();
		this.player		= this.GetComponent<PlayerMovement>();
	}
//	
//	void Update()
//	{
//		this.GetInputAttack();
//
//		this.CalculateCooldown();
//	}
//	
//	void GetInputAttack()
//	{
//		this.timer = Time.timeSinceLevelLoad;
//		
//		if(Input.GetButtonDown("Attack" + this.player.playerID))
//		{
//			if(this.GetCooldown() == 0)
//			{
//				if 		(this.slash1 && this.slash2 && !this.slash3 && this.currentComboState == 3)
//				{
//					this.slash3 = true;
//					this.lastTap = Time.timeSinceLevelLoad;
//				}
//				else if (this.slash1 && !this.slash2 && this.currentComboState == 2)
//				{
//					this.slash2 = true;
//					this.lastTap = Time.timeSinceLevelLoad;
//				}
//				else if (!this.slash1 && this.currentComboState == 1)
//				{
//					this.slash1 = true;
//					this.lastTap = Time.timeSinceLevelLoad;
//				}
//			}
//
//			/*this.lastTap = Time.timeSinceLevelLoad;
//
//			this.tap++;
//			
//			if(this.tap > 3)
//				this.tap = 3;
//			
//			this.Combo();*/
//		}
//	}
//
//	void ExecuteAttack()
//	{
//		if 		(this.slash3)
//		{
//
//		}
//		else if (this.slash2)
//		{
//
//		}
//		else if (this.slash1)
//		{
//
//		}
//		else
//		{
//			return;
//		}
//	}
//
//	void Combo()
//	{
//		if(this.attacking)
//		{
//			AnimatorStateInfo animatorStateInfo;
//
//			switch (this.tap)
//			{
//				
//			case 0:
//				//Reset to Idle
//				break;
//				
//			case 1:
//				//Combo Start
//				animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
//
//				this.timeForCombo = animatorStateInfo.length;
//				this.animator.SetBool("Attack0", true);
//
//				break;
//				
//			case 2:
//				//Combo Prolonged
//
//				this.animator.SetBool("Attack0", true);
//				break;
//				
//			case 3:
//				//Combo Finished
//				this.timeForCombo += GetComponent<Animation>()["Attack3"].length;
//				GetComponent<Animation>().PlayQueued("Attack3", QueueMode.CompleteOthers);
//				break;
//			}
//		}
//	}
//
//	void CalculateCooldown()
//	{
//		if(this.timer - this.lastTap > 1)
//		{
//			this.tap = 0;
//			
//			if(this.timer - this.lastTap < this.timeForCombo)
//			{
//				this.attacking = false;
//			}
//			else
//			{
//				this.attacking = true;
//			}
//		}	
//	}
//
//	float GetCooldown()
//	{
//		if(!this.slash1)
//			return 0;
//
//		return this.timer - this.lastTap;
//	}
}
