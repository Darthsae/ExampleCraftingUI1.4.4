using CraftingExample.Common.Systems;
using CraftingExample.Content.TileEntities;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace CraftingExample.Common.Players
{
    public class CraftingPlayer : ModPlayer
    {
        public bool crafting()
        {
            return crafting_ui && craftingTileEntity != null;
        }
        public bool quick_update = false;
        public bool quick_save = false;
        public bool crafting_ui = false;
        public ExampleCrafterTileEntity craftingTileEntity = null;

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (CraftingKeybindSystem.Crafting.JustPressed && crafting())
            {
                quick_save = true;
            }
        }
    }
}
