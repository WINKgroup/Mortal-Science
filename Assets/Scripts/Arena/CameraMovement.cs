using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CameraMovement : MonoBehaviour
{
	private Vector3 speed;
	private float posX;
	private float posY;
	private float posZ;
	
	public float smoothX;
	public float smoothY;
	public float smoothZ;
	[Range(0, 10)]
	public float offsetX = 2;
	[Range(-1000, 1000)]
	public float offsetY = 2;
	[Range(-1000, 1000)]
	public float offsetZ = 2;
	
	public GameObject player1;
	public GameObject player2;
	
	public bool bounds;

	private Vector3 target;
	public Vector3 minCameraPos;
	public Vector3 maxCameraPos;
	
	private float startY;
	
	void Awake()
	{
		this.startY = transform.position.y;
		//this.offsetZ = transform.position.z - player.transform.position.z;
		//this.offsetY = transform.position.y - player.transform.position.y;
	}
	
	
	void LateUpdate ()
	{

		if(this.player1 != null && this.player2 != null)
		{
			this.CalculateTarget();

			posX = Mathf.SmoothDamp(transform.position.x, this.target.x + offsetX, ref speed.x, smoothX);
			posY = Mathf.SmoothDamp(transform.position.y, this.target.y + offsetY, ref speed.y, smoothY);
			posZ = Mathf.SmoothDamp(transform.position.z, this.target.z + offsetZ, ref speed.z, smoothZ);
			
			transform.position = new Vector3(posX, posY, posZ);

			transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
			                                 Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
			                                 Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
		}
	}

	private void CalculateTarget()
	{
		this.target = new Vector3((this.player1.transform.position.x + this.player2.transform.position.x) / 2,
								  (this.player1.transform.position.y + this.player2.transform.position.y) / 2,
								  (this.player1.transform.position.z + this.player2.transform.position.z) / 2);
	}

	public void TakePlayers()
	{
		List<GameObject> players = new List<GameObject>();
		players = GameObject.FindGameObjectsWithTag("Player").ToList();
		foreach(GameObject pl in players)
		{
			PlayerMovement pm = pl.GetComponent<PlayerMovement>();
			if 		(pm != null && pm.playerID == 1)
			{
				this.player1 = pl;
			}
			else if (pm != null && (pm.playerID == 2 || pm.playerID == 0))
			{
				this.player2 = pl;
			}
		}
		
		if(this.player1 == null)
		{
			Debug.LogError("There's no Player1 in the scene");
		}
		if(this.player2 == null)
		{
			Debug.LogError("There's no Player2 in the scene");
		}
	}
}
