using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SRTPluginProviderRER1.Structs.GameStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0xE44)]

    public struct GamePlayer
    {
        [FieldOffset(0xE28)] private short id;
        [FieldOffset(0xE3C)] private float currentHP;
        [FieldOffset(0xE40)] private float maxHP;

        public short ID => id;
        public string Name => Characters.NamesList.ContainsKey(ID) ? string.Format("{0}: ", Characters.NamesList[ID]) : "";
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

    public class Characters
    {
        public static Dictionary<short, string> NamesList = new Dictionary<short, string>()
        {
            { 0x1000, "Chris" },
            { 0x1010, "Jill" },
            { 0x1020, "Parker" },
            { 0x1030, "Jessica" },
            { 0x1040, "Keith" },
            { 0x1050, "Quint" },
            { 0x1060, "O'Brian" },
            { 0x1070, "Raymond" },
            { 0x1080, "Morgan" },
            { 0x1090, "Jack" },
            { 0x1100, "Chris" },
            { 0x1110, "Jill" },
            { 0x1120, "Parker" },
            { 0x1130, "Jessica" },
            { 0x1140, "Keith" },
            { 0x1150, "Quint" },
            { 0x1200, "Chris" },
            { 0x1210, "Jill" },
            { 0x1220, "Parker" },
            { 0x1230, "Jessica" },
            { 0x1240, "Keith" },
            { 0x1250, "Quint" },
            { 0x1300, "Chris" },
            { 0x1310, "Jill" },
            { 0x1320, "NoModel" },
            { 0x1330, "Jessica" },
            { 0x1400, "Chris" },
            { 0x1420, "Parker" }
        };
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