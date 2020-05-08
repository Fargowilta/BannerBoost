using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace BannerBoost
{
    public class FargoGlobalNPC : GlobalNPC
    {
        /*
         * 5% reduced damage taken per level
         * 15% more damage received per level
         * Max level is 20
         * Kills needed to level up is 50
         * */

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        int numToLevel = BannerConfig.Instance.KillsPerLevel;
        int maxLevelAllowed = BannerConfig.Instance.MaxLevel;
        int maxLevel = 20;

        bool firstTick;

        public override bool PreAI(NPC npc)
        {
            if (firstTick)
            {
                int numBanner = BannerBoostWorld.KillCounter[npc.type] / numToLevel;

                if (numBanner == maxLevel)
                {
                    for (int i = 0; i < Main.player.Length; i++)
                    {
                        Player p = Main.player[i];

                        if (p.active && !p.dead)
                        {
                            p.npcTypeNoAggro[npc.type] = true;
                        }
                    }
                }
                else if (numBanner > 0)
                {
                    npc.damage = (int)(npc.damage * (1 - (.05f * numBanner)));
                }

                firstTick = false;
            }

            return true;
        }

        public override void NPCLoot(NPC npc)
        {
            if (npc.type <= 579)
            {

                int numBanner = BannerBoostWorld.KillCounter[npc.type] / numToLevel;

                if (numBanner == maxLevelAllowed)
                {
                    return;
                }

                BannerBoostWorld.KillCounter[npc.type]++;

                if (BannerBoostWorld.KillCounter[npc.type] % numToLevel == 0)
                {
                    if (BannerBoostWorld.KillCounter[npc.type] == (numToLevel * maxLevel))
                    {
                        Main.NewText("Congratulations! You have earned the max Level against " + npc.TypeName + "!", new Color(48, 255, 76));
                    }
                    else if (BannerBoostWorld.KillCounter[npc.type] < (numToLevel * maxLevel))
                    {
                        Main.NewText("Congratulations! You have reached Level " + (BannerBoostWorld.KillCounter[npc.type] / numToLevel) + " against " + npc.TypeName + "!", new Color(48, 255, 76));
                    }
                }
            }
        }

        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (npc.type <= 579)
            {
                int numBanner = BannerBoostWorld.KillCounter[npc.type] / numToLevel;

                if (numBanner >= maxLevel)
                {
                    damage = npc.lifeMax;
                }
                else if (numBanner > 0)
                {
                    damage = (int)(damage * (1 + (.15f * numBanner)));
                }
            }

            return true;
        }
    }
}