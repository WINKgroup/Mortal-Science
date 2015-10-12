using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum Scientist
{
	Einstein,
	Galilei
}

public class CharacterCollector : MonoBehaviour
{
	public static CharacterCollector Instance {get; private set;}

	public List<Character> characters;


	void Awake()
	{
		if(Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(this.gameObject);
		
		DontDestroyOnLoad(this.gameObject);
	}

	public Character GetCharacter(Scientist hScientist)
	{
		foreach(Character character in this.characters)
		{
			if(character.scientist == hScientist)
				return character;
		}
		return null;
	}
}

[System.Serializable]
public class Character
{
	public Scientist scientist;
	public string name;
	public string turboName;
	public List<string> sentences;
	public Sprite face;
	public GameObject prefab;
}