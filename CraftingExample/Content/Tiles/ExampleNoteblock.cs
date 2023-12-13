using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using CraftingExample.Content.TileEntities;
using CraftingExample.Common.Players;
using Terraria.DataStructures;
using Terraria.Audio;
using Terraria.ID;

namespace CraftingExample.Content.Tiles
{
    public class ExampleNoteblock : ModTile
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
            ExampleNoteblockTileEntity advancedEntity = ModContent.GetInstance<ExampleNoteblockTileEntity>();
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(advancedEntity.Hook_AfterPlacement, -1, 0, false);

            TileObjectData.addTile(Type);

            AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.MetalBar")); // localized text for "Metal Bar"
        }

        public override void PlaceInWorld(int i, int j, Item item)
        {
            ExampleNoteblockTileEntity exampleNoteblockTileEntity = ModContent.GetInstance<ExampleNoteblockTileEntity>();
            exampleNoteblockTileEntity.Place(i, j);
        }

        public override void HitWire(int i, int j)
        {
            ExampleNoteblockTileEntity exampleNoteblockTileEntity = ModContent.GetInstance<ExampleNoteblockTileEntity>();

            if (TileEntity.ByID[exampleNoteblockTileEntity.Find(i, j)] != null)
            {
                SoundEngine.PlaySound(SoundID.GuitarC.WithPitchOffset(exampleNoteblockTileEntity.pitch));
            }
            
        }

        public override bool RightClick(int i, int j)
        {
            ExampleNoteblockTileEntity exampleNoteblockTileEntity = ModContent.GetInstance<ExampleNoteblockTileEntity>();

            if (TileEntity.ByID[exampleNoteblockTileEntity.Find(i, j)] != null)
            {
                exampleNoteblockTileEntity.pitch += 0.1f;

                

                if (exampleNoteblockTileEntity.pitch > 1.0f) exampleNoteblockTileEntity.pitch = -1.0f;
            }
            
            return true;
        }
    }
}
