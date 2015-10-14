using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Balloon : MonoBehaviour
{
	public Text label;

	public float lifeTime = 1f;

	private GameObject canvas;

	void Awake()
	{
		this.canvas = GameObject.Find("Canvas");
	}
	
	
	public void SetText(string sText)
	{
		this.label.text = sText.ToUpper();
	}
	
	public void Initialize(Vector3 vPosition, string sText, float fLifeTime)
	{
		this.lifeTime = fLifeTime;
		this.SetText(sText);
		this.transform.SetParent(this.canvas.transform);
		this.transform.position = vPosition;
		this.gameObject.SetActive(true);
	}
	
	
	void Update()
	{
		if(this.lifeTime > 0)
		{
			this.lifeTime -= Time.deltaTime;
		}
		else
		{
			BalloonManager.Instance.RecycleBalloon(this.gameObject);
		}
	}
}
