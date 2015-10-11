using UnityEngine;
using System.Collections;

public class GravityObject : MonoBehaviour
{
	public bool grounded = false;
	public float gravity = -10f;

	private PlayerMovement player;
	private Rigidbody rigidbody;
	private GameObject objectUnderTheFeet;


	void Start ()
	{
		this.rigidbody = this.GetComponent<Rigidbody>();
		if(this.rigidbody == null)
			this.rigidbody = this.GetComponentInParent<Rigidbody>();

		this.player = this.GetComponent<PlayerMovement>();
	}

	void FixedUpdate ()
	{
		if(this.objectUnderTheFeet == null)
			this.grounded = false;

		if(!this.grounded)
		{
			this.rigidbody.AddForce(Vector3.up * this.gravity * this.rigidbody.mass);
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if(collider.gameObject.tag == "Ground")
		{
			this.objectUnderTheFeet = null;
			this.grounded = false;
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.tag == "Ground")
		{
			this.objectUnderTheFeet = collider.gameObject;
			this.grounded = true;
		}
	}

	void OnTriggerStay(Collider collider)
	{
		if(collider.gameObject.tag == "Ground")
		{
			this.objectUnderTheFeet = collider.gameObject;
			this.grounded = true;
		}
	}
}
