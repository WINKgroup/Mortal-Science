using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BalloonManager : MonoBehaviour
{
	public static BalloonManager Instance {get; private set;}
	
	public GameObject balloonPrefab;
	
	private List<GameObject> balloons = new List<GameObject>();
	private Vector2 pivot_default;
	
	void Awake()
	{
		if(Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(this.gameObject);
		
		DontDestroyOnLoad(this.gameObject);
	}
	
	void Start()
	{
		this.pivot_default = this.balloonPrefab.GetComponent<RectTransform>().pivot;

		for(int i=0; i<4; i++)
		{
			GameObject newBalloon = Instantiate(this.balloonPrefab, new Vector2(0,0), Quaternion.identity) as GameObject;
			newBalloon.SetActive(false);
			this.balloons.Add(newBalloon);
		}
	}
	
	
	/// <summary>
	/// Gets a disabled balloon from a pool
	/// </summary>
	/// <returns>The balloon.</returns>
	public GameObject GetBalloon()
	{
		GameObject bal = this.balloons[0];
		
		if(bal != null)
		{
			this.balloons.RemoveAt(0);
		}
		else
		{
			bal = Instantiate(this.balloonPrefab, new Vector2(0,0), Quaternion.identity) as GameObject;
		}
		
		return bal;
	}
	
	public void RecycleBalloon(GameObject hBalloon)
	{
		this.balloons.Add(hBalloon);
		hBalloon.SetActive(false);
	}
}
