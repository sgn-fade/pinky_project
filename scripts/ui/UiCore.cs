using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.ui;

public partial class UiCore : CanvasLayer
{
	private GameUi gameUi;
	private Control inventoryUi;
	private Control loadUi;
	private Control pauseUi;
	private Control currentUi;

	private TextureProgressBar loadBar;
	private EventBus eventBus = Global.EventBus;
	private PlayerData player;
	private Crosshair crosshair;

	public override void _Ready()
	{
		player = Global.Player;
		gameUi = GetNode<GameUi>("game_ui");
		inventoryUi = GetNode<Control>("inventory_ui");
		loadUi = GetNode<Control>("load_ui");
		loadBar = loadUi.GetNode<TextureProgressBar>("TextureProgressBar");
		pauseUi = GetNode<Control>("pause_ui");
		crosshair = GetNode<Crosshair>("crosshair");
		//todo event bus
		//eventBus.Connect("load_game", new Callable(this, nameof(LoadAnimation)));
		currentUi = gameUi;

		SwitchUi(gameUi, "game");
	}

	public async void LoadAnimation()
	{
		loadUi.Visible = true;
		for (int i = 1; i <= 100; i++)
		{
			loadBar.Value = i;
			await ToSignal(GetTree().CreateTimer(1f), "timeout");

		}
		currentUi = loadUi;
		player.GetBody().SetIdleState();
		SwitchUi(gameUi, "game");
	}

	public override void _Process(double delta)
	{
		if (!player.IsReady())
			return;

		if (Input.IsActionJustPressed("open_inventory"))
		{
			if (currentUi != inventoryUi)
				SwitchUi(inventoryUi, "ui");
			else
				SwitchUi(gameUi, "game");
		}

		if (Input.IsActionJustPressed("ui_cancel"))
		{
			if (currentUi != pauseUi)
				SwitchUi(pauseUi, "ui");
			else
				SwitchUi(gameUi, "game");
		}
	}

	public void SwitchUi(Control uiType, string crosshairType)
	{
		GD.Print(uiType);
		crosshair.ChangeCrosshair(crosshairType);
		currentUi.Visible = false;
		uiType.Visible = true;
		currentUi = uiType;
	}

	public void UpdateHpValue(int hp, int maxHp)
	{
		gameUi.UpdateHpValue(hp, maxHp);
	}
	public void UpdateManaValue(int mana, int maxMana)
	{
		gameUi.UpdateManaValue(mana, maxMana);
	}
}
