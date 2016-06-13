using UnityEngine;
using System.Collections;

public class FinisherExplosion : MonoBehaviour
{
	public int power = 25;
	public PlayerMovement playerMovement;

	void OnTriggerEnter(Collider collider)
	{
		Damageable dmg = collider.GetComponent<Damageable>();
		if(dmg != null)
		{
			HitAnEnvironment(dmg);
		}
		
		PlayerMovement pm = collider.GetComponent<PlayerMovement>();
		if(pm != null && pm != this.playerMovement)
		{
			HitACharacter(pm);
		}
	}

	void HitACharacter(PlayerMovement target)
	{
		target.GetHit(this.power);
		this.playerMovement.turbo.AddTurbo(this.power);
	}

	void HitAnEnvironment(Damageable dmg)
	{
		dmg.Damage(this.playerMovement, this.power, GetComponent<Collider>().transform.position);
	}
}
