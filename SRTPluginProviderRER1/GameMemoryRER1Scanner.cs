using ProcessMemory;
using SRTPluginProviderRER1.Structs.GameStructs;
using System;
using System.Diagnostics;

namespace SRTPluginProviderRER1
{
    internal unsafe class GameMemoryRER1Scanner : IDisposable
    {
        private readonly int MAX_ENTITIES = 16;
        // Variables
        private ProcessMemoryHandler memoryAccess;
        private GameMemoryRER1 gameMemoryValues;
        public bool HasScanned;
        public bool ProcessRunning => memoryAccess != null && memoryAccess.ProcessRunning;
        public int ProcessExitCode => (memoryAccess != null) ? memoryAccess.ProcessExitCode : 0;

        // Pointer Address Variables
        private int pointerAddressPlayer;
        private int pointerAddressStats;
        private int pointerAddressEnemy;
        private int pointerAddressIGT;

        // Pointer Classes
        private IntPtr BaseAddress { get; set; }

        private MultilevelPointer[] PointerEnemy { get; set; }
        private MultilevelPointer PointerHP { get; set; }
        private MultilevelPointer PointerStats { get; set; }
        private MultilevelPointer PointerInventory { get; set; }
        private MultilevelPointer PointerIGT { get; set; }

        internal GameMemoryRER1Scanner(Process process = null)
        {
            gameMemoryValues = new GameMemoryRER1();
            if (process != null)
                Initialize(process);
        }

        internal unsafe void Initialize(Process process)
        {
            if (process == null)
                return; // Do not continue if this is null.

            GameHashes.DetectVersion(process.MainModule.FileName);
            SelectPointerAddresses();

            int pid = GetProcessId(process).Value;
            memoryAccess = new ProcessMemoryHandler(pid);
            if (ProcessRunning)
            {
                BaseAddress = NativeWrappers.GetProcessBaseAddress(pid, PInvoke.ListModules.LIST_MODULES_32BIT); // Bypass .NET's managed solution for getting this and attempt to get this info ourselves via PInvoke since some users are getting 299 PARTIAL COPY when they seemingly shouldn't.
                
                //POINTERS
                PointerHP = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressPlayer), 0x44, 0x10);
                PointerInventory = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressPlayer), 0x44, 0xF8);
                PointerStats = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressStats));
                PointerIGT = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressIGT));
                var position = 0;
                PointerEnemy = new MultilevelPointer[MAX_ENTITIES];
                gameMemoryValues._enemyHealth = new GameEnemy[MAX_ENTITIES];
                for (var i = 0; i < MAX_ENTITIES; i++)
                {
                    position = 0x4 * i;
                    PointerEnemy[i] = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressEnemy), 0x838, position);
                    gameMemoryValues._enemyHealth[i] = new GameEnemy();
                }
            }
        }

        private void SelectPointerAddresses()
        {
            pointerAddressEnemy = 0xDE6184;
            pointerAddressPlayer = 0xD6F394;
            pointerAddressStats = 0xD707E4;
            pointerAddressIGT = 0xD70B60;
        }

        internal void UpdatePointers()
        {
            PointerHP.UpdatePointers();
            PointerInventory.UpdatePointers();
            PointerStats.UpdatePointers();
            PointerIGT.UpdatePointers();
            for (var i = 0; i < MAX_ENTITIES; i++)
            {
                PointerEnemy[i].UpdatePointers();
            }
        }

        internal unsafe IGameMemoryRER1 Refresh()
        {
            // Player
            gameMemoryValues._player = PointerHP.Deref<GamePlayer>(0x0);

            // Player Inventory
            gameMemoryValues._playerInventory = PointerInventory.Deref<GameInventory>(0x0);

            // Game Statistics
            gameMemoryValues._endResults = PointerStats.Deref<GameEndResults>(0x0);

            gameMemoryValues._igt = PointerIGT.DerefFloat(0x2C);

            // Enemy HP Array
            for (var i = 0; i < MAX_ENTITIES; i++)
            {
                gameMemoryValues._enemyHealth[i] = PointerEnemy[i].Deref<GameEnemy>(0x0);
            }

            HasScanned = true;
            return gameMemoryValues;
        }

        private int? GetProcessId(Process process) => process?.Id;

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (memoryAccess != null)
                        memoryAccess.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~REmake1Memory() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
