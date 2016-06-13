using UnityEngine;
using System;

public class SpecialTimer
{
	public float Seconds {get; private set;}
	public float Minutes {get; private set;}
	
	public SpecialTimer(float seconds, float minutes)
	{
		Seconds = seconds;
		Minutes = minutes;
	}
	
	public void AddTime(float delta)
	{
		Seconds += delta;
		
		if(Seconds >= 60)
		{
			Seconds -= 60;
			Minutes += 1;
		}
	}
}