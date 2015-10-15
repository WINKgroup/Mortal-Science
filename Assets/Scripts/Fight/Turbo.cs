[System.Serializable]
public class Turbo
{
	public float CurrentTurbo {get; private set;}
	public int TurboLimit {get; private set;}

	public Turbo (int iTurboLimit)
	{
		this.TurboLimit = iTurboLimit;
		this.CurrentTurbo = 0;
	}

	public float AddTurbo(float fTurbo)
	{
		this.CurrentTurbo = (this.CurrentTurbo + fTurbo > TurboLimit) ? this.TurboLimit : this.CurrentTurbo + fTurbo;
		return this.CurrentTurbo;
	}

	public void ResetTurbo()
	{
		this.CurrentTurbo = 0;
	}

	public bool IsInTurbo()
	{
		return this.CurrentTurbo == this.TurboLimit;
	}
}

