public class Turbo
{
	public int CurrentTurbo {get; private set;}
	public int TurboLimit {get; private set;}

	public Turbo (int iTurboLimit)
	{
		this.TurboLimit = iTurboLimit;
		this.CurrentTurbo = 0;
	}

	public int AddTurbo(int iTurbo)
	{
		this.CurrentTurbo = (this.CurrentTurbo + iTurbo > TurboLimit) ? this.TurboLimit : this.CurrentTurbo + iTurbo;
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

