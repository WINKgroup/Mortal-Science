﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Damageable : MonoBehaviour
{
	public Animator animator;
	protected PlayerMovement lastPlayer;

	protected void OnAwake()
	{
		this.animator = this.GetComponent<Animator>();
		if(this.animator == null)
			this.animator = this.GetComponentInChildren<Animator>();
	}

	public virtual void Damage(PlayerMovement player, int damage, Vector3 pos)
	{
	}

	protected IEnumerator GetHit(Vector3 pos)
	{
		this.animator.SetTrigger("Hit");

		yield return null;
	}

	protected void DestroyAnimation()
	{
		Camera.main.GetComponent<CameraShake>().Shake();
		this.animator.SetTrigger("Destroy");
		if(lastPlayer != null)
			this.lastPlayer.turbo.AddTurbo(20);
	}

	public void DestroyObj()
	{
		Destroy(this.gameObject);
	}
}
