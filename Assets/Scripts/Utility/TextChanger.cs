using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TextChanger : MonoBehaviour
{
	Text label;
	public float frameLength = 0.15f;
	int frameIndex = 0;
	public List<string> texts;
	
	float time = 0;

	void Awake()
	{
		label = GetComponent<Text>();
	}

	void Start()
	{
		time = frameLength;
	}

	void OnEnable()
	{
		frameIndex = 0;
		if(texts.Count>0)
			label.text = texts[0];
	}

	void Update ()
	{
		if(time <= 0)
		{
			SetNextText();
			time = frameLength;
		}
		else
		{
			time -= Time.deltaTime;
		}
	}

	void SetNextText()
	{
		if(texts.Count == 0 || frameIndex + 1 == texts.Count)
			frameIndex = 0;
		else
			frameIndex++;

		label.text = texts[frameIndex];
	}
}
