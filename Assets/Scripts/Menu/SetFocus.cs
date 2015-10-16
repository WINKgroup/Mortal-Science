using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SetFocus : MonoBehaviour
{
	public GameObject objectFocused;
	private EventSystem eventSystem;
	
	void OnEnable ()
	{
		Debug.Log ("Enabling");
		this.eventSystem = EventSystem.current;
		this.eventSystem.SetSelectedGameObject(this.objectFocused);
	}
}
