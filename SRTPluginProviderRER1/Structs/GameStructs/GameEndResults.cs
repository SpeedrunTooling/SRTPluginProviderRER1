using System;
using System.Runtime.InteropServices;

namespace SRTPluginProviderRER1.Structs.GameStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x6E0)]

    public struct GameEndResults
    {
        [FieldOffset(0x34)] private int rank;
        [FieldOffset(0x38)] private int rankScore;
        [FieldOffset(0x6B8)] private int shotsFired;
        [FieldOffset(0x6BC)] private int enemiesHit;
        [FieldOffset(0x6C0)] private int deaths;
        [FieldOffset(0x6DC)] private float clearTime;

        public int Rank => rank;
        public int RankScore => rankScore;
        public int ShotsFired => shotsFired;
        public int EnemiesHit => enemiesHit;
        public float Accuracy => ShotsFired != 0 ? (float)EnemiesHit / (float)ShotsFired : 0f;
        public int Deaths => deaths;
        public float ClearTime => clearTime;
    }
}
