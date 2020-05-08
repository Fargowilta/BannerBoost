using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BannerBoost
{
    internal partial class BannerBoostWorld : ModWorld
    {

        public static Dictionary<int, int> KillCounter { get; set; }

        public override void Initialize()
        {
            KillCounter = new Dictionary<int, int>();

            for (int i = 0; i <= 579; i++)
            {
                KillCounter.Add(i, 0);
            }
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                ["KillCounter.Keys"] = KillCounter.Keys.ToList(),
                ["KillCounter.Values"] = KillCounter.Values.ToList()
            };
        }

        public override void Load(TagCompound tag)
        {
            if (tag.ContainsKey("KillCounter.Keys"))
            {
                var keys = tag.Get<List<int>>("KillCounter.Keys");
                var values = tag.Get<List<int>>("KillCounter.Values");
                KillCounter = keys.Zip(values, (key, value) => new { Key = key, Value = value }).ToDictionary(x => x.Key, x => x.Value);
            }
        }

    }
}