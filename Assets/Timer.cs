using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour 
{
	private Text counterText;
	public SpecialTimer timer;
	
	void Awake()
	{
		counterText = GetComponent<Text>();
		timer = new SpecialTimer(Manager.prevLevelSeconds, Manager.prevLevelSeconds);
	}
	
	void Update()
	{
		timer.AddTime(Time.deltaTime);
		counterText.text = timer.Minutes.ToString ("00") + ":" + timer.Seconds.ToString ("00");
	}

	public void SaveTime()
	{
		Manager.prevLevelSeconds = timer.Seconds;
		Manager.prevLevelMinutes = timer.Minutes;
	}
}
