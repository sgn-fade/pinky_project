using Godot;

namespace projectpinky.scripts.Globals
{
    public partial class EventBus : Node
    {

        [Signal]
        public delegate void PlayerTakeDamageEventHandler(Vector2 playerOffsetDir, int damage);

        [Signal]
        public delegate void ShowDamageValueEventHandler(Node2D damageLabelInstance, int damage);

        [Signal]
        public delegate void SurviveEventStartedEventHandler(Vector2 roomSize, Vector2 roomCenter, float time);

        [Signal]
        public delegate void PullsBodyEventHandler(Node2D body, Vector2 position);

        [Signal]
        public delegate void PushAwayEnemyEventHandler(Node2D body, Vector2 velocity);

        [Signal]
        public delegate void PlayerCastSpellEventHandler(float animationTime, string animationName);

        [Signal]
        public delegate void HandsPlayAnimationEventHandler(string animationName);

        [Signal]
        public delegate void SwitchHandsStanceEventHandler(Node weapon);

        [Signal]
        public delegate void SpellAnimationEndedEventHandler();

        [Signal]
        public delegate void PlayerTeleportEventHandler(Vector2 position);


        [Signal]
        public delegate void WeaponInInventoryChoosedEventHandler(Node weapon);


        [Signal]
        public delegate void AddItemEventHandler(Node item);

        [Signal]
        public delegate void StartSpellCooldownEventHandler(float time, string button);

    }
}
