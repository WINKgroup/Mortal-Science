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
	
	public GameObject target;
	
	public bool bounds;
	
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
		if(this.target != null)
		{
			posX = Mathf.SmoothDamp(transform.position.x, target.transform.position.x + offsetX, ref speed.x, smoothX);
			posY = Mathf.SmoothDamp(transform.position.y, target.transform.position.y + offsetY, ref speed.y, smoothY);
			posZ = Mathf.SmoothDamp(transform.position.z, target.transform.position.z + offsetZ, ref speed.z, smoothZ);
			
			transform.position = new Vector3(posX, posY, posZ);

			transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
			                                 Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
			                                 Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
		}
	}

	public void TakeTarget()
	{
		List<GameObject> players = new List<GameObject>();
		players = GameObject.FindGameObjectsWithTag("Player").ToList();
		foreach(GameObject pl in players)
		{
			PlayerMovement pm = pl.GetComponent<PlayerMovement>();
			if(pm != null && pm.playerID == 1)
			{
				this.target = pl;
				break;
			}
		}
		
		if(this.target == null)
		{
			Debug.LogError("There's no Player1 in the scene");
		}
	}
}
