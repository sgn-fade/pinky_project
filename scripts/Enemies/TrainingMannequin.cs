using Godot;
using System;
using projectpinky.scripts.ui;

public partial class TrainingMannequin : Node2D
{
    [Export] private Hurtbox hurtBox;
    [Export] private DamageLabel damageLabel;

    private void OnTakeDamage(int value) => damageLabel.ShowValue(value);
}
