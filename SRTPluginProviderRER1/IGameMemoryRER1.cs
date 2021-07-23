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
        GameEnemy[] EnemyHealth { get; set; }
        GameEndResults EndResults { get; set; }

        TimeSpan IGTTimeSpan { get; }
        string IGTFormattedString { get; }

    }
}