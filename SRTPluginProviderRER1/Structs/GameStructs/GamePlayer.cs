using System.Runtime.InteropServices;

namespace SRTPluginProviderRER1.Structs.GameStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x8)]

    public struct GamePlayer
    {
        [FieldOffset(0x0)] private float currentHP;
        [FieldOffset(0x4)] private float maxHP;

        public float CurrentHP => currentHP;
        public float MaxHP => maxHP;
        public float Percentage => CurrentHP > 0 ? (float)CurrentHP / (float)MaxHP : 0f;
        public bool IsAlive => CurrentHP != 0 && MaxHP != 0 && CurrentHP > 0 && CurrentHP <= MaxHP;
        public PlayerState HealthState
        {
            get =>
                !IsAlive ? PlayerState.Dead :
                Percentage >= 0.75f ? PlayerState.Fine :
                Percentage >= 0.50f ? PlayerState.FineToo :
                Percentage >= 0.25f ? PlayerState.Caution :
                PlayerState.Danger;
        }
    }

    public enum PlayerState
    {
        Dead,
        Fine,
        FineToo,
        Caution,
        Danger
    }
}