using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MeteorAnimation : MonoBehaviour
{
	public List<MeteorType> meteorType;
	public MeteorType currentMeteor;

	private SpriteRenderer spriteRenderer;
	
	void Awake()
	{
		this.spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void OnEnable()
	{
		StartCoroutine(this.AnimateTurbo());
	}
	
	public IEnumerator AnimateTurbo()
	{
		this.currentMeteor = this.meteorType[Random.Range(0, this.meteorType.Count)];
		
		for(int i = 0; i < this.currentMeteor.frames.Count; i++)
		{
			this.spriteRenderer.sprite = this.currentMeteor.frames[i];
			
			if(i == this.currentMeteor.frames.Count - 1)
				i = -1;
			
			yield return new WaitForSeconds(0.1f);
		}
	}
}

[System.Serializable]
public class MeteorType
{
	public List<Sprite> frames;
}