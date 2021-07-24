using System.Runtime.InteropServices;

namespace SRTPluginProviderRER1.Structs.GameStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0xD0)]

    public struct GameInventory
    {
        [FieldOffset(0x40)] private uint handgunAmmo;
        [FieldOffset(0x44)] private uint shotgunAmmo;
        [FieldOffset(0x48)] private uint machineGunAmmo;
        [FieldOffset(0x4C)] private uint rifleAmmo;
        [FieldOffset(0x50)] private uint magnumAmmo;

        [FieldOffset(0xA0)] private uint handGrenade;
        [FieldOffset(0xA4)] private uint shockGrenade;
        [FieldOffset(0xA8)] private uint bowDecoy;
        [FieldOffset(0xAC)] private uint pulseGrenade;

        [FieldOffset(0xC8)] private uint greenHerb;
        [FieldOffset(0xCC)] private uint oldKey;

        public uint HandgunAmmo => handgunAmmo;
        public uint ShotgunAmmo => shotgunAmmo;
        public uint MachineGunAmmo => machineGunAmmo;
        public uint RifleAmmo => rifleAmmo;
        public uint MagnumAmmo => magnumAmmo;
        public uint HandGrenade => handGrenade;
        public uint ShockGrenade => shockGrenade;
        public uint BOWDecoy => bowDecoy;
        public uint PulseGrenade => pulseGrenade;
        public uint GreenHerb => greenHerb;
        public uint OldKey => oldKey;
    }
}
