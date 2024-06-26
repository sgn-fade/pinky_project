using Godot;
using System.Threading.Tasks;
using projectpinky.scripts.Enemies;
using projectpinky.scripts.Globals;

public partial class SwordBaseParticle : Node2D
{
    [Export] public NodePath AnimationPath;
    [Export] public NodePath SpritePath;
    [Export] public NodePath Area2DPath;

    private AnimationPlayer _anim;
    private AnimatedSprite2D _sprite;
    private Area2D _area2D;

    public override void _Ready()
    {
        _anim = GetNode<AnimationPlayer>(AnimationPath);
        _sprite = GetNode<AnimatedSprite2D>(SpritePath);
        _area2D = GetNode<Area2D>(Area2DPath);

        LookAt(GetGlobalMousePosition());
        _anim.Play("default");
        _sprite.Play("hit");
        AnimationFinishedAsync().ContinueWith(_ => QueueFree());

        _area2D.Connect("body_entered", new Callable(this, nameof(OnArea2DBodyEntered)));
    }

    private async Task AnimationFinishedAsync()
    {
        await ToSignal(_sprite, "animation_finished");
    }

    private void OnArea2DBodyEntered(Node2D body)
    {
        if (body is Enemy enemy)
        {
            enemy.TakeDamage(10);
        }
    }
}