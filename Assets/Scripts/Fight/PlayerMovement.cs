using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public int playerID = 0;

	public int maxHealth;
	public int health;

	public Turbo turbo;
	public Transform uiTurboAnimation;

	public float speedX;
	public float speedZ;
	public float maxSpeedX;
	public float maxSpeedZ;
	public float jumpPower;

	public bool canMove;
	public bool canAttack;

	private float speedDiagonalModifier = 0.6f;

	public float inputHorizontal;
	public float inputVertical;
	public bool inputJump;
	public bool inputAttack;
	public bool inputGuard;
	public bool inputTurbo;
	public bool attackIsRunning;
	public bool combo;

	public Arena arena;

	private float scale;
	private bool lookingRight = true;
	private Vector3 movementDirection;
	private Rigidbody rb;
	private GravityObject feet;
	private Animator animator;
	private AIEnemy aiEnemy;
	private CameraShake camShake;
	private Mouth mouth;
	private IFinisher finisher;
	private TrailRenderer trail;

	#region - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Initializer
	void Awake()
	{
		this.rb 		= this.GetComponent<Rigidbody>();
		this.feet 		= this.GetComponentInChildren<GravityObject>();
		this.mouth 		= this.GetComponentInChildren<Mouth>();
		this.animator 	= this.GetComponentInChildren<Animator>();
		this.trail		= this.GetComponentInChildren<TrailRenderer>();
		this.aiEnemy 	= this.GetComponent<AIEnemy>();
		this.finisher	= this.GetComponent<IFinisher>();
		this.camShake 	= Camera.main.GetComponent<CameraShake>();

		this.arena = GameObject.Find("Arena").GetComponent<Arena>();

		this.scale = this.transform.localScale.x;

		this.turbo = new Turbo(100);

		this.trail.enabled = false;
	}

	public void AmICpu()
	{
		if(this.playerID != 0)
		{
			DestroyImmediate(this.aiEnemy);
		}
	}
	#endregion

	#region - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Update
	void Update()
	{
		this.UpdateAnimator();
	}

	void FixedUpdate()
	{
		this.GetInput();

		this.Flip();

		this.Jump();

		this.Move();

		this.FixMaxSpeed();

		this.FixInertia();

		this.Turbo();


	}

	void UpdateAnimator()
	{
		this.animator.SetBool("Grounded", this.feet.grounded);
		
		Vector3 vel = new Vector3(this.rb.velocity.x, 0, this.rb.velocity.z);
		this.animator.SetFloat("Speed", vel.normalized.magnitude);
	}
	#endregion

	#region - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Input
	void GetInput()
	{
		if(this.arena.status == ArenaStatus.Fight)
		{
			// Am I in guard?
			this.Guard();

			if(this.playerID != 0)
			{
				// If I am not in guard I can handle other inputs
				if(!this.inputGuard)
				{
					this.inputHorizontal 	= Input.GetAxis("Horizontal_player" + this.playerID.ToString());
					this.inputVertical 		= Input.GetAxis("Vertical_player" + this.playerID.ToString());
					this.inputJump 			= Input.GetButtonDown("Jump_player" + this.playerID.ToString());
					this.inputAttack		= Input.GetButtonDown("Fire3_player" + this.playerID.ToString());
					this.inputTurbo 		= Input.GetButtonDown("Turbo_player" + this.playerID.ToString());
				}
				else
				{
					this.inputHorizontal 	= 0;
					this.inputVertical 		= 0;
					this.inputJump 			= false;
					this.inputTurbo 		= false;
					//this.inputAttack		= false;
				}
			}
		}
		else
		{
			this.inputHorizontal 	= 0;
			this.inputVertical 		= 0;
			this.inputJump 			= false;
			this.inputGuard			= false;
			this.inputAttack		= false;
			this.inputTurbo 		= false;
		}
	}
	#endregion

	#region - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Fix
	void Flip()
	{
		if(this.inputHorizontal > 0)
		{
			this.LookingRight();
		}
		else if(this.inputHorizontal < 0)
		{
			this.LookingLeft();
		}
	}

	public void LookingRight()
	{
		this.transform.localScale = new Vector3(this.scale, this.scale, this.scale);
		this.lookingRight = true;
	}

	public void LookingLeft()
	{
		this.transform.localScale = new Vector3(-this.scale, this.scale, this.scale);
		this.lookingRight = false;
	}

	void FixMaxSpeed()
	{
		if(this.rb.velocity.x > this.maxSpeedX)
		{
			this.rb.velocity = new Vector3(this.maxSpeedX, this.rb.velocity.y, this.rb.velocity.z);
		}
		else if(this.rb.velocity.x < -this.maxSpeedX)
		{
			this.rb.velocity = new Vector3(-this.maxSpeedX, this.rb.velocity.y, this.rb.velocity.z);
		}
		
		if(this.rb.velocity.z > this.maxSpeedZ)
		{
			this.rb.velocity = new Vector3(this.rb.velocity.x, this.rb.velocity.y, this.maxSpeedZ);
		}
		else if(this.rb.velocity.z < -this.maxSpeedZ)
		{
			this.rb.velocity = new Vector3(this.rb.velocity.x, this.rb.velocity.y, -this.maxSpeedZ);
			
		}
	}
	
	void FixInertia()
	{
		if(Mathf.Abs(this.inputHorizontal) < 0.1f)
		{
			this.rb.velocity = new Vector3(0, this.rb.velocity.y, this.rb.velocity.z);
		}
		if(Mathf.Abs(this.inputVertical) < 0.1f)
		{
			this.rb.velocity = new Vector3(this.rb.velocity.x, this.rb.velocity.y, 0);		
		}
	}
	#endregion

	#region - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - PlayerActions
	void Jump()
	{
		if(this.feet.grounded && this.inputJump)
		{
			this.rb.AddForce(Vector3.up * this.jumpPower);
		}
	}

	void Move()
	{
		if(this.canMove)
		{
			this.movementDirection = new Vector3(this.inputHorizontal * this.speedX, 0f, this.inputVertical * this.speedZ);
			
			if(Mathf.Abs(this.inputHorizontal) > 0.2f && Mathf.Abs(this.inputVertical) > 0.2f)
				this.rb.AddForce(this.movementDirection * this.speedDiagonalModifier);
			else
				this.rb.AddForce(this.movementDirection);
		}
	}

	void Guard()
	{
		if(this.aiEnemy == null)
		{
			this.inputGuard = Input.GetButton("Fire2_player" + this.playerID.ToString());
		}

		if(this.inputGuard && this.turbo.CurrentTurbo > 10 * Time.deltaTime)
		{
			this.turbo.AddTurbo(-10 * Time.deltaTime);
			this.canAttack = false;
		}
		else
		{
			this.inputGuard = false;
			this.canAttack = true;
		}
	}
	
	public void Speak(PlayerPosition hPos)
	{
		this.mouth.SpeakRandomSentence(hPos);
	}

	public void Turbo()
	{
		this.trail.enabled = this.turbo.IsInTurbo();
		if(this.inputTurbo && this.turbo.IsInTurbo())
		{
			this.uiTurboAnimation.gameObject.SetActive(true);
			this.turbo.ResetTurbo();
			this.finisher.Execute();
		}
	}
	#endregion

	#region - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Events
	public bool GetHit(int damage)
	{
		if(this.aiEnemy != null)
		{
			this.inputGuard = (Random.value > this.aiEnemy.guardProbability) ? false : true;
			this.Guard();
		}

		if(this.inputGuard)
		{
			this.turbo.AddTurbo(damage / 3);
			StartCoroutine(this.aiEnemy.ExitGuard(0.2f));
			return false;
		}

		if(this.health > 0)
		{
			this.camShake.Shake();

			this.health -= damage;

			this.turbo.AddTurbo(damage / 4);

			if(this.health > 0)
			{
				this.animator.SetTrigger("Hit");
			}
			else
			{
				this.health = 0;
				this.animator.SetTrigger("KO");
			}
		}

		return true;
	}
	#endregion
}
