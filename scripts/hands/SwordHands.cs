using projectpinky.scripts.Globals;

namespace projectpinky.scripts.hands;

using Godot;

public partial class SwordHands : MeleeHands
{
    [Signal]
    public delegate void DamageToEnemyEventHandler(Node body, int damage);

    private PlayerData player = Global.Player;
    private void _on_Area2D_body_entered(Node body)
    {
        // int damage = player.GetWeapon().Damage;
        // if (GD.Randf() * 100 < player.GetWeapon().CritChance)
        // {
        //     damage *= 2;
        // }
        // EmitSignal(nameof(DamageToEnemy), body, damage, null);
    }
}