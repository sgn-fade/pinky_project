using System.Collections.Generic;
using Godot;
using projectpinky.scripts.Globals;
using projectpinky.scripts.particles;
using projectpinky.scripts.player;
using projectpinky.scripts.spells;
using projectpinky.scripts.weapons;

namespace projectpinky.scripts.hands;

public abstract partial class Hands : Node2D
{
    public Player Player { get; set; }
    public Dictionary<string, Spell> spellsButtons = new();
    public SpellController ParticleToCast { get; set; }
    public abstract void PlayAnimation(string animationName);

    public override void _Ready()
    {
        var cells = Player.PlayerData.Weapon.GetCells();
        var index = 0;
        foreach (var button in Options.ButtonsBinds)
        {
            spellsButtons.Add(button.Value, cells[index].Spell);
            index++;
        }
    }

    public override void _Input(InputEvent @event)
    {
        foreach (var kvp in spellsButtons)
        {
            if (Input.IsActionJustPressed(kvp.Key) && kvp.Value != null)
            {
                CastSpell(kvp.Value);
                return;
            }
        }
    }
    protected void CastSpell(Spell spell)
    {
        if (Player.PlayerData.SetMana(-spell.ManaCost) && spell.GetReady())
        {
            if (spell.Particle != null)
            {
                ParticleToCast = spell.Particle.Instantiate<SpellController>();
                PlayAnimation(ParticleToCast.GetAnim());
            }
            spell.Cooldown();
        }
    }

    protected void CreateSpellParticle()
    {
        ParticleToCast.GlobalPosition = Player.GlobalPosition;
        Global.World.AddEntity(ParticleToCast);
    }
}