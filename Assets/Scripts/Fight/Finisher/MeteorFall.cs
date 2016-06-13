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
			Vector3 position = Game.Instance.arena.GetRandomPointOnSurfaceNear(playerMovement.transform.position, 8f);
			GameObject newBomb = Instantiate(this.bombPrefab, position, Quaternion.identity)as GameObject;
			newBomb.GetComponent<FinisherExplosion>().playerMovement = this.playerMovement;
		}
	}
}

