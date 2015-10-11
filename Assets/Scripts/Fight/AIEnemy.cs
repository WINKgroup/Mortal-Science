using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AIEnemy : MonoBehaviour
{
	public enum AIState	{far, close, attacking1, attacking2, attacking3, idle}
	public AIState aiState;

	public float distance;
	public float closeDistance = 2f;
	public float inertia = 0.5f;

	public float actionTime = 2f;
	public float comboRate = 0.05f;
	public float attackProbability = 0.5f;
	public float actionID;

	public float currentTime;
	public bool doAction;

	public bool closeCombat;
	public GameObject target;

	private PlayerMovement playerMovement;

	void Awake()
	{
		this.playerMovement = GetComponent<PlayerMovement>();
	}

	void Start()
	{
		this.TakeTarget();
	}

	void Update()
	{
		if(this.playerMovement.arena.status != ArenaStatus.Fight)
			return;

		// Calculate distance from target
		this.CheckDistance();

		// Move
		this.DoMovementInput();

		// Is time to do something?
		if(!this.CheckAction())
			return;

		// Jump
		this.JumpInput();

		// Flip
		this.CheckForFlip();

		// Close
		if(this.closeCombat)
		{
			if(this.actionID < this.attackProbability)
			{
				this.DoAttackInput();
			}
		}
	}

	void LateUpdate()
	{
		this.playerMovement.inputAttack = false;
	}

	bool CheckAction()
	{
		if(this.currentTime > this.actionTime)
		{
			this.actionID = Random.value;
			this.currentTime = 0;
			this.doAction = true;
		}
		else
		{
			this.actionID = 0;
			this.currentTime += Time.deltaTime;
			//this.playerMovement.inputJump = 0;
			this.doAction = false;
		}

		return this.doAction;
	}

	bool CheckDistance()
	{
		this.distance = Vector3.Distance(this.transform.position, this.target.transform.position);
		this.closeCombat = this.distance < this.closeDistance;

		return this.closeCombat;
		/*if(this.distance < this.closeDistance)
		{
			this.aiState = AIState.close;
		}
		else if(this.distance >= this.closeDistance)
		{	
			this.aiState = AIState.far;
		}*/
	}

	void CheckForFlip()
	{
		if(this.target.transform.position.x > this.transform.position.x)
			this.playerMovement.LookingRight();
		else
			this.playerMovement.LookingLeft();
	}

	void DoAttackInput()
	{
		this.playerMovement.inputAttack = true;
	}

	void DoMovementInput()
	{
		if(this.playerMovement.canMove)
		{
			// Too far from target
			if(!this.closeCombat)
			{
				this.playerMovement.inputHorizontal = (this.transform.position.x >= this.target.transform.position.x) ? -1 : 1;
				this.playerMovement.inputVertical 	= (this.transform.position.z >= this.target.transform.position.z) ? -1 : 1;
			}
			// Inertia...
			else if(this.playerMovement.inputHorizontal != 0 || this.playerMovement.inputVertical != 0)
			{
				this.playerMovement.inputHorizontal = Mathf.Lerp(this.playerMovement.inputHorizontal, 0f, this.inertia);
				this.playerMovement.inputVertical 	= Mathf.Lerp(this.playerMovement.inputVertical, 0f, this.inertia);
			}
			// Near to target
			else
			{
				this.playerMovement.inputHorizontal = 0f;
				this.playerMovement.inputVertical = 0f;
			}

			this.playerMovement.inputJump = 0;
		}
	}

	void JumpInput()
	{
		this.playerMovement.inputJump = (Random.value > 0.6f) ? 1 : 0;
		if(this.playerMovement.inputJump == 1)
			Debug.Log("Jump");
	}
	
	void TakeTarget()
	{
		List<GameObject> players = new List<GameObject>();
		players = GameObject.FindGameObjectsWithTag("Player").ToList();
		foreach(GameObject pl in players)
		{
			PlayerMovement pm = pl.GetComponent<PlayerMovement>();
			if(pm != null && pm.playerID == 1)
			{
				this.target = pl;
				break;
			}
		}
		
		if(this.target == null)
		{
			Debug.LogError("There's no Player1 in the scene");
		}
	}
}
