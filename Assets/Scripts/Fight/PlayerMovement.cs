using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public int playerID = 0;

	public int maxHealth;
	public int health;

	public Turbo turbo;

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
	public float inputJump;
	public bool inputAttack;
	public bool inputGuard;
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

	void Awake()
	{
		this.rb 		= this.GetComponent<Rigidbody>();
		this.feet 		= this.GetComponentInChildren<GravityObject>();
		this.animator 	= this.GetComponentInChildren<Animator>();
		this.aiEnemy 	= this.GetComponent<AIEnemy>();
		this.camShake 	= Camera.main.GetComponent<CameraShake>();

		this.arena = GameObject.Find("Arena").GetComponent<Arena>();

		this.scale = this.transform.localScale.x;

		this.turbo = new Turbo(100);

		this.AmICpu();
	}

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
	}

	void GetInput()
	{
		if(this.arena.status == ArenaStatus.Fight)
		{
			if(this.playerID != 0)
			{
				// Am I in guard?
				this.inputGuard = Input.GetButton("Fire2_player" + this.playerID);

				// If I am not in guard I can handle other inputs
				if(!this.inputGuard)
				{
					this.inputHorizontal 	= Input.GetAxis("Horizontal_player" + this.playerID);
					this.inputVertical 		= Input.GetAxis("Vertical_player" + this.playerID);
					this.inputJump 			= Input.GetAxis("Jump_player" + this.playerID);
					this.inputAttack		= Input.GetButtonDown("Fire3_player" + this.playerID);
				}
				else
				{
					this.inputHorizontal 	= 0;
					this.inputVertical 		= 0;
					this.inputJump 			= 0;
					this.inputAttack		= false;
				}
			}
		}
		else
		{
			this.inputHorizontal 	= 0;
			this.inputVertical 		= 0;
			this.inputJump 			= 0;
			this.inputGuard			= false;
			this.inputAttack		= false;
		}
	}

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

	void Jump()
	{
		if(this.feet.grounded && this.inputJump > 0)
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

	void UpdateAnimator()
	{
		this.animator.SetBool("Grounded", this.feet.grounded);

		Vector3 vel = new Vector3(this.rb.velocity.x, 0, this.rb.velocity.z);
		this.animator.SetFloat("Speed", vel.normalized.magnitude);
	}

	public bool GetHit(int damage)
	{
		if(this.inputGuard)
			return false;

		if(this.health > 0)
		{
			this.camShake.Shake();

			this.health -= damage;
			
			if(this.health > 0)
			{
				this.animator.SetTrigger("Hit");
			}
			else
			{
				this.health = 0;
				this.animator.SetTrigger("Hit");
			}
		}

		return true;
	}

	public void AmICpu()
	{
		if(this.playerID != 0)
		{
			DestroyImmediate(this.aiEnemy);
		}
	}
}
