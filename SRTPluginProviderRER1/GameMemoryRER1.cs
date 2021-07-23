using SRTPluginProviderRER1.Structs.GameStructs;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace SRTPluginProviderRER1
{
    public class GameMemoryRER1 : IGameMemoryRER1
    {
        private const string IGT_TIMESPAN_STRING_FORMAT = @"hh\:mm\:ss";
        public string GameName => "REREV1";

        // Versioninfo
        public string VersionInfo => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;

        // Leon Stats
        public GamePlayer Player { get => _player; set => _player = value; }
        internal GamePlayer _player;

        public GameEnemy[] EnemyHealth { get => _enemyHealth; set => _enemyHealth = value; }
        internal GameEnemy[] _enemyHealth;

        public GameEndResults EndResults { get => _endResults; set => _endResults = value; }
        internal GameEndResults _endResults;

        public TimeSpan IGTTimeSpan
        {
            get
            {
                TimeSpan timespanIGT;

                if (EndResults.ClearTime >= 0f)
                    timespanIGT = TimeSpan.FromSeconds(EndResults.ClearTime);
                else
                    timespanIGT = new TimeSpan();

                return timespanIGT;
            }
        }

        public string IGTFormattedString => IGTTimeSpan.ToString(IGT_TIMESPAN_STRING_FORMAT, CultureInfo.InvariantCulture);
    }
}
