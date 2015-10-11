using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurboBarController : MonoBehaviour
{
	public PlayerMovement player;
	public Image turboSprite;
	
	private Slider slider;

	
	void Awake()
	{
		this.slider = this.GetComponent<Slider>();
	}
	
	void LateUpdate()
	{
		float nextValue = (float)this.player.turbo.CurrentTurbo / (float)this.player.turbo.TurboLimit;
		this.slider.value = Mathf.Lerp (this.slider.value, nextValue, 0.1f);

		this.turboSprite.enabled = this.player.turbo.IsInTurbo();
	}
}
