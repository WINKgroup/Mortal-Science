using UnityEngine;
using System.Collections;

[System.Serializable]
public class Player
{
	public int health;
	public int numberController;
	public Character character;


	public Player ()
	{
		this.health = 100;
		this.character = null;
		this.numberController = 0;
	}

	public Player (int iHealth, Scientist hScientist, int iNumber)
	{
		this.health = iHealth;
		this.character = CharacterCollector.Instance.GetCharacter(hScientist);
		this.numberController = iNumber;
	}

	public void SetNumber(int iN)
	{
		this.numberController = iN;
	}

	public void SetScientist(Scientist hScientist)
	{
		this.character = CharacterCollector.Instance.GetCharacter(hScientist);
	}

	public void SetHealth(int iHealth)
	{
		this.health = iHealth;
	}
}

