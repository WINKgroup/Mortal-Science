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
		StartCoroutine(SelectContinueButtonLater());
	}

	IEnumerator SelectContinueButtonLater()
	{
		yield return null;
		this.eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		this.eventSystem.SetSelectedGameObject(null);
		this.eventSystem.SetSelectedGameObject(this.objectFocused, null);
		Button button = objectFocused.GetComponent<Button>();
		if(button != null)
		{
			button.Select();
		}
	}
}
