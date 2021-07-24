using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SRTPluginProviderRER1.Structs.GameStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0xE44)]
    public struct GameEnemy
    {
        [FieldOffset(0xE28)] private short id;
        [FieldOffset(0xE3C)] private float currentHP;
        [FieldOffset(0xE40)] private float maximumHP;

        public short ID => id;
        public string Name => Enemies.NamesList.ContainsKey(ID) ? string.Format("{0}: ", Enemies.NamesList[ID]) : "";
        public bool IsNaN(float hp) => hp.CompareTo(float.NaN) == 0;
        public float CurrentHP => !IsNaN(currentHP) ? currentHP : 0f;
        public float MaximumHP => !IsNaN(maximumHP) ? maximumHP : 0f;
        public bool IsTrigger => !Enemies.NamesList.ContainsKey(ID);
        public bool IsAlive => !IsTrigger && CurrentHP != 0;
        public bool IsDamaged => IsAlive ? CurrentHP < MaximumHP : false;
        public float Percentage => (IsAlive) ? CurrentHP / MaximumHP : 0f;
    }

    public class Enemies
    {
        public static Dictionary<short, string> NamesList = new Dictionary<short, string>()
        {
            { 0x1000, "Mutated Ooze" },
            { 0x1010, "Claw Ooze" },
            { 0x1020, "Shooter Ooze" },
            { 0x1030, "Explode Ooze" },
            { 0x1060, "Rachael Ooze" },
            { 0x1070, "Mutated Ooze" },
            { 0x1080, "Claw Ooze" },
            { 0x1090, "Shooter Ooze" },
            { 0x1200, "Sea Creeper" },
            { 0x1201, "Sea Creeper" },
            { 0x1210, "Mutated Dog" },
            { 0x1211, "Mutated Dog" },
            { 0x1220, "Fish" },
            { 0x1221, "Blowfish" },
            { 0x1230, "Green Hunter" },
            { 0x1240, "Invisible Hunter" },
            { 0x1300, "Scarmiglione" },
            { 0x1301, "Scarmiglione" },
            { 0x1310, "Beach Blob" },
            { 0x1320, "Beach Blob" },
            { 0x1330, "LowPoly Beach Blob" },
            { 0x1400, "Scragdead" },
            { 0x1401, "Scragdead" },
            { 0x1410, "Wall Blister" },
            { 0x1420, "Draghignazzo" },
            { 0x1421, "Draghignazzo" },
            { 0x1422, "Draghignazzo" },
            { 0x1430, "Malacoda" },
            { 0x1431, "Malacoda" },
            { 0x1440, "Jack" },
            { 0x1442, "Jack" },
            { 0x1450, "Jack" },
            { 0x1910, "Zenobia" },
            { 0x1911, "Zenobia" },
            { 0x1912, "Zenobia" },
            { 0x1913, "Zenobia" },
            { 0x1920, "Zenobia" }
        };
    }
}