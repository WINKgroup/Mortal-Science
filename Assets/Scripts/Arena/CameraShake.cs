using UnityEngine;
using System.Collections;


public class CameraShake : MonoBehaviour
{
	public float shakeIntensity = 0.025f;
	public float shakeDecay = 0.002f;
	public float multiplier = 0.3f;
	
	private float currentShakeIntensity;
	private Vector3 originPosition;
	private Quaternion originRotation;
	
	void Start()
	{
		SaveOriginData();
	}
	
	void Update ()
	{
		if (this.currentShakeIntensity > 0)
		{
			this.transform.position = this.originPosition + Random.insideUnitSphere * this.shakeIntensity;
			this.transform.rotation = new Quaternion(
				this.originRotation.x + Random.Range (-this.currentShakeIntensity, this.currentShakeIntensity) * this.multiplier,
				this.originRotation.y + Random.Range (-this.currentShakeIntensity, this.currentShakeIntensity) * this.multiplier,
				this.originRotation.z + Random.Range (-this.currentShakeIntensity, this.currentShakeIntensity) * this.multiplier,
				this.originRotation.w + Random.Range (-this.currentShakeIntensity, this.currentShakeIntensity) * this.multiplier);

			this.currentShakeIntensity -= this.shakeDecay;

			if(this.currentShakeIntensity <= 0)
			{
				this.transform.rotation = this.originRotation;
				this.transform.position = this.originPosition;
			}
		}
	}
	
	public void Shake()
	{
		SaveOriginData();

		this.transform.rotation 	= this.originRotation;
		this.transform.position		= this.originPosition;
		this.currentShakeIntensity  = this.shakeIntensity;
	}

	private void SaveOriginData()
	{
		this.originPosition = this.transform.position;
		this.originRotation = this.transform.rotation;
	}
}