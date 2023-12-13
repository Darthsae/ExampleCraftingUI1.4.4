using CraftingExample.Common.Systems;
using CraftingExample.Content.TileEntities;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace CraftingExample.Common.Players
{
    public class CraftingPlayer : ModPlayer
    {
        public bool IsCrafting()
        {
            return crafting_ui && (craftingTileEntity != null) && (craftingTileEntity.Position.ToVector2().Distance(Player.Center.ToTileCoordinates16().ToVector2()) < 20);
        }
        public bool quick_update = false;
        public bool quick_save = false;
        public bool crafting_ui = false;
        public ExampleCrafterTileEntity craftingTileEntity = null;
        public int MusicDisk = -1;
        public bool Jukebox = false;

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (CraftingKeybindSystem.Crafting.JustPressed && IsCrafting())
            {
                quick_save = true;
            }
        }
    }
}
