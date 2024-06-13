using Godot;

namespace projectpinky.scripts.Globals
{
    public partial class EventBus : Node
    {

        [Signal]
        public delegate void PlayerCastSpellEventHandler(float animationTime, string animationName);
        [Signal]
        public delegate void PlayerDeadEventHandler();

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
