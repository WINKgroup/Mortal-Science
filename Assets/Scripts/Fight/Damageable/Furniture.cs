using UnityEngine;
using System.Collections;

public class Furniture : Damageable
{
	public int life = 100;
	public bool breakable = true;

	void Awake()
	{
		base.OnAwake();
	}

	public override void Damage(int damage, Vector3 pos)
	{
		if(this.breakable && this.life > 0)
		{
			this.life -= damage;
			StartCoroutine(base.GetHit(pos));
			
			if(this.life <= 0)
			{
				base.DestroyAnimation();
			}
		}
	}
}

