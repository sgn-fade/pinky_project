using Godot;

namespace projectpinky.scripts.Globals
{
    public partial class EventBus : Node
    {
        public static EventBus Instance { get; private set; }

        [Signal]
        public delegate void PlayerTakeDamageEventHandler(Vector2 playerOffsetDir, int damage);

        [Signal]
        public delegate void ShowDamageValueEventHandler(Node2D damageLabelInstance, int damage);

        [Signal]
        public delegate void SpheresDonatedEventHandler(float time);

        [Signal]
        public delegate void SurviveEventStartedEventHandler(Vector2 roomSize, Vector2 roomCenter, float time);

        [Signal]
        public delegate void PullsBodyEventHandler(Node2D body, Vector2 position);

        [Signal]
        public delegate void PushAwayEnemyEventHandler(Node2D body, Vector2 velocity);

        [Signal]
        public delegate void CrosshairSwitchEventHandler(string type);

        [Signal]
        public delegate void PlayerCastSpellEventHandler(float animationTime, string animationName);

        [Signal]
        public delegate void HandsPlayAnimationEventHandler(string animationName);

        [Signal]
        public delegate void SwitchHandsStanceEventHandler(Node weapon);

        [Signal]
        public delegate void SpellAnimationEndedEventHandler();

        [Signal]
        public delegate void DashCooldownEventHandler();

        [Signal]
        public delegate void PlayerTeleportEventHandler(Vector2 position);

        [Signal]
        public delegate void ShowModuleStatsOnGameScreenEventHandler(Node module);

        [Signal]
        public delegate void HideModuleStatsOnGameScreenEventHandler();

        [Signal]
        public delegate void ShowWeaponStatsOnGameScreenEventHandler(Node weapon);

        [Signal]
        public delegate void HideWeaponStatsOnGameScreenEventHandler();

        [Signal]
        public delegate void WeaponInInventoryChoosedEventHandler(Node weapon);

        [Signal]
        public delegate void SetSpellIconToGameEventHandler(Node moduleIcon, string button);

        [Signal]
        public delegate void RemoveSpellIconFromGameEventHandler(string button);

        [Signal]
        public delegate void ClearSpellIconsEventHandler();

        [Signal]
        public delegate void AddItemEventHandler(Node item);

        [Signal]
        public delegate void StartSpellCooldownEventHandler(float time, string button);

        [Signal]
        public delegate void DamageToEnemyEventHandler(Node2D body, int damage, string status);

        [Signal]
        public delegate void UpdateCharacterHpBarValueEventHandler(int hp, int maxHp);

        [Signal]
        public delegate void UpdateCharacterManaBarValueEventHandler(int mana, int maxMana);

        [Signal]
        public delegate void GenerateDungeonEventHandler();

        [Signal]
        public delegate void GoToHubEventHandler();

        [Signal]
        public delegate void LoadGameEventHandler();

        [Signal]
        public delegate void EnemyKilledEventHandler();

        [Signal]
        public delegate void AddPlayerToBoardEventHandler(string playerName);
    }
}
