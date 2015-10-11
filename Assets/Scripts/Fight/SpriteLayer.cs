using UnityEngine;
using System.Collections;

public class SpriteLayer : MonoBehaviour
{
	SpriteRenderer renderer;

	void Start ()
	{
		this.renderer = GetComponent<SpriteRenderer>();
	}

	void Update ()
	{
		this.renderer.sortingOrder = (int)(-this.transform.position.z * 100f);
	}
}
