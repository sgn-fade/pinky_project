using projectpinky.scripts.Globals;

namespace projectpinky.scripts.consumabels;

public class Consumabel
{
	public ConsumableData Data { get; set; }
	private PlayerData player;
	public double TimeSpend { get; set; }

	public Consumabel(ConsumableData data)
	{
		Data = data;
		player = Global.Player;
	}
	public void Use()
	{
		Global.World.AddEntity(Data.Consumable);
	}

	public bool GetReady()
	{
		return !(TimeSpend < Data.CooldownTime);
	}

	public void Cooldown()
	{
		TimeSpend = 0;
	}

	public double GetCooldownTime()
	{
		return TimeSpend;
	}

	public float GetMaxCooldownTime()
	{
		return Data.CooldownTime;
	}

	public void AddSpendTime(double value)
	{
		TimeSpend += value;
	}
}
