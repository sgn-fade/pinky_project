using Godot;
using System.Collections.Generic;
using projectpinky.scripts.Globals;
namespace projectpinky.scripts.particles;

public partial class FireEye : CharacterBody2D
{
    private Timer _timer = new Timer();
    private Timer _damageTimer = new Timer();
    private Area2D _damageArea;
    private AnimatedSprite2D _eyeAnimation;
    private AnimatedSprite2D _shadowAnimation;
    private CpuParticles2D _areaParticles;
    private float _spellDuration = 9f;
    private float _tickTime = 1f;
    private List<Node> _enemyInside = new List<Node>();

    public override void _Ready()
    {
        _damageArea = GetNode<Area2D>("damage_area");
        _eyeAnimation = GetNode<AnimatedSprite2D>("sprite");
        _shadowAnimation = GetNode<AnimatedSprite2D>("shadow_under");
        _areaParticles = GetNode<CpuParticles2D>("area_particles");

        SetProcess(false);
        _timer.OneShot = false;
        AddChild(_timer);
        _damageTimer.OneShot = false;
        AddChild(_damageTimer);

        PlayOpeningAnimation();
    }

    private async void PlayOpeningAnimation()
    {
        _eyeAnimation.Play("opening");
        await ToSignal(_eyeAnimation, "animation_finished");
        GD.Print("animation_finished");
        SetProcess(true);
        _areaParticles.Emitting = true;
        _eyeAnimation.Play("searching");

        _damageArea.Connect("body_entered", new Callable(this, nameof(OnDamageAreaBodyEntered)));
        _damageArea.Connect("body_exited", new Callable(this, nameof(OnDamageAreaBodyExited)));

        _timer.Start(_spellDuration);
        await ToSignal(_timer, "timeout");

        SetProcess(false);
        _damageArea.Disconnect("body_entered", new Callable(this, nameof(OnDamageAreaBodyEntered)));
        _enemyInside.Clear();

        _eyeAnimation.Play("closing");
        await ToSignal(_eyeAnimation, "animation_finished");

        QueueFree();
    }

    public override void _Process(double delta)
    {
        _eyeAnimation.Position += new Vector2(0, Mathf.Sin(Time.GetTicksMsec() * 0.003f) * 3);
    }

    private async void DealDamage(Node body)
    {
        while (_enemyInside.Contains(body))
        {
            //todo event bus
            Global.EventBus.EmitSignal("damage_to_enemy", body, 2);
            _damageTimer.Start(_tickTime);
            await ToSignal(_damageTimer, "timeout");
        }
    }

    private void OnDamageAreaBodyEntered(Node body)
    {
        _enemyInside.Add(body);
        DealDamage(body);
    }

    private void OnDamageAreaBodyExited(Node body)
    {
        _enemyInside.Remove(body);
        //todo event bus
        Global.EventBus.EmitSignal("damage_to_enemy", body, 5, "burn");
    }
}
