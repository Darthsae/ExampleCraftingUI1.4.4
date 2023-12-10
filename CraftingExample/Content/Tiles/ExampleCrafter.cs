using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using CraftingExample.Content.TileEntities;
using CraftingExample.Common.Players;
using Terraria.DataStructures;

namespace CraftingExample.Content.Tiles
{
    public class ExampleCrafter : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileShine[Type] = 1100;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            ExampleCrafterTileEntity advancedEntity = ModContent.GetInstance<ExampleCrafterTileEntity>();
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(advancedEntity.Hook_AfterPlacement, -1, 0, false);

            TileObjectData.addTile(Type);

            AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.MetalBar")); // localized text for "Metal Bar"
        }

        public override void PlaceInWorld(int i, int j, Item item)
        {
            ExampleCrafterTileEntity exampleCrafterTileEntity = ModContent.GetInstance<ExampleCrafterTileEntity>();
            exampleCrafterTileEntity.Place(i, j);
        }

        public override bool RightClick(int i, int j)
        {
            ExampleCrafterTileEntity exampleCrafterTileEntity = ModContent.GetInstance<ExampleCrafterTileEntity>();

            Main.LocalPlayer.GetModPlayer<CraftingPlayer>().crafting_ui = true;
            if (TileEntity.ByID[exampleCrafterTileEntity.Find(i, j)] != null)
            {
                Main.LocalPlayer.GetModPlayer<CraftingPlayer>().craftingTileEntity = (ExampleCrafterTileEntity)TileEntity.ByID[exampleCrafterTileEntity.Find(i, j)];
                CraftingPlayer crafter = Main.LocalPlayer.GetModPlayer<CraftingPlayer>();
                if (crafter.craftingTileEntity.items[0] == null)
                {
                    Item item = new Item();
                    item.SetDefaults(0);
                    crafter.craftingTileEntity.items[0] = item;
                }
                if (crafter.craftingTileEntity.items[1] == null)
                {
                    Item item = new Item();
                    item.SetDefaults(0);
                    crafter.craftingTileEntity.items[1] = item;
                }
                Main.LocalPlayer.GetModPlayer<CraftingPlayer>().quick_update = true;
            }
            
            return true;
        }
    }
}
