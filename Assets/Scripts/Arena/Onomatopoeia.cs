using UnityEngine;
using System.Collections;

public class Onomatopoeia : MonoBehaviour
{
	public TextMesh foreground;
	public TextMesh background;

	public Vector3 velocity;
	public float lifeTime = 1f;
	

	public void SetText(string sText, int iDimension)
	{
		this.foreground.text = sText;
		this.background.text = sText;

		this.foreground.fontSize = iDimension;
		this.background.fontSize = iDimension;
	}

	public void Initialize(Vector3 vPosition, string sText, int iDimension, float fLifeTime)
	{
		this.lifeTime = fLifeTime;
		this.SetText(sText, iDimension);
		this.transform.position = vPosition;
		this.gameObject.SetActive(true);
	}


	void Update()
	{
		if(this.lifeTime > 0)
		{
			this.lifeTime -= Time.deltaTime;

			this.transform.position = new Vector3(this.transform.position.x + this.velocity.x * Time.deltaTime,
			                                      this.transform.position.y + this.velocity.y * Time.deltaTime,
			                                      this.transform.position.z + this.velocity.z * Time.deltaTime);
		}
		else
		{
			OnomatopoeiaManager.Instance.RecycleOnomatopoeia(this.gameObject);
		}
	}
}

