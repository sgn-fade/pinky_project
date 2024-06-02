using Godot;
using projectpinky.scripts.Globals;

namespace projectpinky.scripts.ui;

public partial class UiCore : CanvasLayer
{
    private GameUi gameUi;
    private Control inventoryUi;
    private Control loadUi;
    private Control pauseUi;
    private Control altarUi;
    private Control menuUi;
    private Control currentUi;

    private TextureProgressBar loadBar;
    private EventBus eventBus;
    private PlayerData player;

    public override void _Ready()
    {
        eventBus = GetNode<EventBus>("/root/EventBus");
        player = GetNode<PlayerData>("/root/PlayerData");
        gameUi = GetNode<GameUi>("game_ui");
        inventoryUi = GetNode<Control>("inventory_ui");
        loadUi = GetNode<Control>("load_ui");
        loadBar = loadUi.GetNode<TextureProgressBar>("TextureProgressBar");
        pauseUi = GetNode<Control>("pause_ui");
        altarUi = GetNode<Control>("altar_ui");
        menuUi = GetNode<Control>("menu_ui");
        currentUi = menuUi;

        eventBus.Connect("load_game", new Callable(this, nameof(LoadAnimation)));

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


    public void _Process(float delta)
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
        eventBus.EmitSignal("crosshair_switch", crosshairType);
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