using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitBox : MonoBehaviour
{
	public int power = 10;
	public int ID = 0;
	public List<GameObject> damageParticles;
	private PlayerMovement playerMovement;

	void Awake()
	{
		this.playerMovement = GetComponentInParent<PlayerMovement>();
	}

	void OnTriggerEnter(Collider collider)
	{
		Damageable dmg = collider.GetComponent<Damageable>();
		if(dmg != null)
		{
			HitAnEnvironment(dmg);
		}

		PlayerMovement pm = collider.GetComponent<PlayerMovement>();
		if(pm != null)
		{
			HitACharacter(pm);
		}
	}

	void MakeParticle()
	{	
		Vector3 randomPos = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.1f, 0.7f), 0);
		
		GameObject newParticle = Instantiate(damageParticles[Random.Range(0, this.damageParticles.Count)],
		                                     new Vector3(this.transform.position.x + randomPos.x, this.transform.position.y + randomPos.y, this.transform.position.z - 1),
		                                     Quaternion.identity) as GameObject;
    }

	void HitACharacter(PlayerMovement target)
	{
		switch(this.ID)
		{
		case 0:
			if(target.GetHit(this.power))
			{
				this.playerMovement.turbo.AddTurbo(this.power * 2);
				target.turbo.AddTurbo(this.power / 3);
				MakeParticle();
			}
			else
			{
				this.playerMovement.turbo.AddTurbo(this.power / 2);
				target.turbo.AddTurbo(this.power);
			}
			break;
		case 1:
			if(target.GetHit(this.power))
			{
				this.playerMovement.turbo.AddTurbo(this.power * 2);
				target.turbo.AddTurbo(this.power / 3);
				MakeParticle();
			}
			else
			{
				this.playerMovement.turbo.AddTurbo(this.power / 2);
				target.turbo.AddTurbo(this.power);
			}
			break;
		case 2:
			if(target.GetHit(this.power))
			{
				this.playerMovement.turbo.AddTurbo(this.power * 3);
				target.turbo.AddTurbo(this.power / 3);
				MakeParticle();
			}
			else
			{
				this.playerMovement.turbo.AddTurbo(this.power / 2);
				target.turbo.AddTurbo(this.power);
			}
			break;
		}
	}

	void HitAnEnvironment(Damageable dmg)
	{
		dmg.Damage(this.power, GetComponent<Collider>().transform.position);
	}
}

