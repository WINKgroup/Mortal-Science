using UnityEngine;
using System.Collections;

public class Furniture : Damageable
{
	public int life = 20;
	public bool breakable = true;
	public string onomatopoeia = "KABOOM";
	public int onomatopoeiaDimension = 60;

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
				this.CreateOnomatopoeia();
				base.DestroyAnimation();
			}
		}
	}

	private void CreateOnomatopoeia()
	{
		Onomatopoeia onom = OnomatopoeiaManager.Instance.GetOnomatopoeia().GetComponent<Onomatopoeia>();
		onom.Initialize(this.transform.position, this.onomatopoeia, this.onomatopoeiaDimension, 1f);
	}
}

