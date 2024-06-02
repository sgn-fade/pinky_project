using Godot;

namespace projectpinky.scripts.ui;

public partial class PlayerBars : Node
{
    private TextureRect hpBar;
    private ShaderMaterial hpBarMaterial;
    private Label currentHp;
    private Label maxHpLabel;

    private TextureRect manaBar;
    private ShaderMaterial manaBarMaterial;
    private Label currentMana;
    private Label maxManaLabel;

    public override void _Ready()
    {
        hpBar = GetNode<TextureRect>("hp_bar");
        hpBarMaterial = hpBar.Material as ShaderMaterial;
        currentHp = GetNode<Label>("current_hp");
        maxHpLabel = GetNode<Label>("max_hp");

        manaBar = GetNode<TextureRect>("mana_bar");
        manaBarMaterial = manaBar.Material as ShaderMaterial;
        currentMana = GetNode<Label>("current_mana");
        maxManaLabel = GetNode<Label>("max_mana");
    }

    public void UpdateHpValue(int hp, int maxHp)
    {
        hpBarMaterial.SetShaderParameter("fV", (float)hp / maxHp);
        currentHp.Text = hp.ToString();
        maxHpLabel.Text = maxHp.ToString();
    }

    public void UpdateManaValue(int mana, int maxMana)
    {
        manaBarMaterial.SetShaderParameter("fV", (float)mana / maxMana);
        currentMana.Text = mana.ToString();
        maxManaLabel.Text = maxMana.ToString();
    }
}