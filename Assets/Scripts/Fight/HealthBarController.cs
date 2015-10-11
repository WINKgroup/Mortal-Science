using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarController : MonoBehaviour
{
	public PlayerMovement character;

	private Slider slider;

	void Awake()
	{
		this.slider = this.GetComponent<Slider>();
	}

	void LateUpdate()
	{
		float nextValue = (float)this.character.health / (float)this.character.maxHealth;
		this.slider.value = Mathf.Lerp (this.slider.value, nextValue, 0.1f);
	}
}
