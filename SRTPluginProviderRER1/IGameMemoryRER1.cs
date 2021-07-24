using SRTPluginProviderRER1.Structs.GameStructs;
using System;

namespace SRTPluginProviderRER1
{
    public interface IGameMemoryRER1
    {
        // Versioninfo
        string GameName { get; }
        string VersionInfo { get; }

        GamePlayer Player { get; set; }
        GameInventory PlayerInventory { get; set; }
        GameEnemy[] EnemyHealth { get; set; }
        GameEndResults EndResults { get; set; }
        float IGT { get; set; }

        TimeSpan IGTTimeSpan { get; }
        string IGTFormattedString { get; }
        TimeSpan SegmentTimeSpan { get; }
        string SegmentFormattedString { get; }

    }
}