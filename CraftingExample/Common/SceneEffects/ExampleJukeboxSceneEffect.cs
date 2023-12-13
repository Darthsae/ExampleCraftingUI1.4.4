using CraftingExample.Common.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace CraftingExample.Common.SceneEffects
{
    public class ExampleJukeboxSceneEffect : ModSceneEffect
    {
        public sealed override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

        public sealed override int Music => Main.LocalPlayer.GetModPlayer<CraftingPlayer>().MusicDisk;

        public sealed override bool IsSceneEffectActive(Player player)
        {
            return player.GetModPlayer<CraftingPlayer>().Jukebox;
        }
    }
}
