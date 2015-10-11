using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurboBarController : MonoBehaviour
{
	public PlayerMovement character;
	public Image turboSprite;
	
	private Slider slider;

	
	void Awake()
	{
		this.slider = this.GetComponent<Slider>();
	}
	
	void LateUpdate()
	{
		float nextValue = (float)this.character.turbo.CurrentTurbo / (float)this.character.turbo.TurboLimit;
		this.slider.value = Mathf.Lerp (this.slider.value, nextValue, 0.1f);

		this.turboSprite.enabled = this.character.turbo.IsInTurbo();
	}
}
