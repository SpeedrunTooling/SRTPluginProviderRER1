using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SRTPluginProviderRER1.Structs.GameStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x8)]
    [DebuggerDisplay("{_DebuggerDisplay,nq}")]
    public struct GameEnemy
    {
        [FieldOffset(0x0)] private float currentHP;
        [FieldOffset(0x4)] private float maximumHP;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string _DebuggerDisplay
        {
            get
            {
                if (IsTrigger)
                {
                    return string.Format("TRIGGER", CurrentHP, MaximumHP, Percentage);
                }
                else if (IsAlive)
                {
                    return string.Format("{0} / {1} ({2:P1})", CurrentHP, MaximumHP, Percentage);
                }
                return "DEAD / DEAD (0%)";
            }
        }

        public float CurrentHP => currentHP;
        public float MaximumHP => maximumHP;
        public bool IsTrigger => MaximumHP <= 10f || MaximumHP > 100000f;
        public bool IsNaN => MaximumHP.CompareTo(float.NaN) == 0;
        public bool IsAlive => !IsNaN && !IsTrigger && CurrentHP <= MaximumHP;
        public bool IsDamaged => IsAlive && CurrentHP < MaximumHP;
        public float Percentage => ((IsAlive) ? CurrentHP / MaximumHP : 0f);
    }
}