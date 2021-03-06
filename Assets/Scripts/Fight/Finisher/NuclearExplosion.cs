using UnityEngine;
using System.Collections;

public class NuclearExplosion : MonoBehaviour, IFinisher
{
	public GameObject bombPrefab;
	public int numberOfBombs = 5;

	private PlayerMovement playerMovement;

	void Awake()
	{
		this.playerMovement = this.GetComponent<PlayerMovement>();
	}

	public void Execute()
	{
		for(int i=0; i<this.numberOfBombs; i++)
		{
			Vector3 position = Game.Instance.arena.GetRandomPointOnSurfaceNear(playerMovement.transform.position, 9f);
			GameObject newBomb = Instantiate(this.bombPrefab, position, Quaternion.identity)as GameObject;
			newBomb.GetComponent<FinisherExplosion>().playerMovement = this.playerMovement;
		}
	}
}

