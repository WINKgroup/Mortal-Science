using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour
{
	public float timeForDestroy;
	private float timer = 0;

	void Awake()
	{
		this.timer = this.timeForDestroy;
	}

	void Update ()
	{
		this.timer -= Time.deltaTime;

		if(this.timer <= 0)
		{
			Destroy(this.gameObject);
		}
	}
}

