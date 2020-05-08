using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using System.ComponentModel;

namespace BannerBoost
{
    internal partial class BannerBoostMod : Mod
    {
        internal static BannerBoostMod Instance
        {
            get; private set;
        }

        public override void Load()
        {
            Instance = this;
        }
        public override void Unload()
        {
            Instance = null;
            BannerBoostWorld.KillCounter = null;
        }
    }

    class BannerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static BannerConfig Instance;

        [Label("Kills Per Level")]
        [Increment(2)]
        [Range(10, 200)]
        [DefaultValue(50)]
        [Slider]
        public int KillsPerLevel;

        [Label("Max Level Achievable")]
        [Increment(1)]
        [Range(2, 20)]
        [DefaultValue(20)]
        [Slider]
        public int MaxLevel;

    }
}