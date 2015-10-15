using UnityEngine;
using System.Collections;

public class MeteorFall : MonoBehaviour, IFinisher
{
	public GameObject bombPrefab;
	public int numberOfBombs = 7;
	
	private PlayerMovement playerMovement;
	
	void Awake()
	{
		this.playerMovement = this.GetComponent<PlayerMovement>();
	}
	
	public void Execute()
	{
		for(int i=0; i<this.numberOfBombs; i++)
		{
			GameObject newBomb = Instantiate(this.bombPrefab, Game.Instance.arena.GetRandomPointOnSurface(), Quaternion.identity)as GameObject;
			newBomb.GetComponent<FinisherExplosion>().playerMovement = this.playerMovement;
		}
	}
}

