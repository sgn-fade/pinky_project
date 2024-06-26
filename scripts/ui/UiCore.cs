using Godot;
using projectpinky.scripts.drops;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.ui;

public partial class UiCore : CanvasLayer
{
	[Export] private GameUi gameUi;
	[Export] private PlayerInventory inventoryUi;
	[Export] private Control loadUi;
	[Export] private Control pauseUi;
	[Export] private Crosshair crosshair;

	private Control currentUi;
	private EventBus eventBus = Global.EventBus;
	private PlayerData player;

	public override void _Ready()
	{
		player = Global.Player;
		currentUi = gameUi;
		//SwitchUi(gameUi, "game");
	}

	public override void _Process(double delta)
	{
		if (!player.IsReady())
			return;

		if (Input.IsActionJustPressed("I"))
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
		crosshair.ChangeCrosshair(crosshairType);
		currentUi.Visible = false;
		uiType.Visible = true;
		currentUi = uiType;
	}
}
