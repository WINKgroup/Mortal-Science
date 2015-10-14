using UnityEngine;
using System.Collections;

public class Oscillation : MonoBehaviour
{
	public float speed = 1f;
	public float angle = 3f;

	private float currentAngle;

	void Start ()
	{
		this.speed *= Random.Range(0.9f, 1.1f);
	}
	

	void Update ()
	{
		float t = (Mathf.Sin (Time.time * speed * Mathf.PI * 2.0f) + 1.0f) / 2.0f;
		this.transform.eulerAngles = Vector3.Lerp (new Vector3(0f, 0f, -angle),
		                                           new Vector3(0f, 0f, angle),
		                                           t);
	}
}
