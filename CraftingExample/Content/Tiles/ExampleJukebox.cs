using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using CraftingExample.Content.TileEntities;
using CraftingExample.Common.Players;
using Terraria.DataStructures;
using System.Linq;
using Terraria.ID;
using Terraria.Audio;
using CraftingExample.Content.Items;
using Microsoft.Xna.Framework.Audio;

namespace CraftingExample.Content.Tiles
{
    public class ExampleJukebox : ModTile
    {
        public int[] musicDisks = null;
        public IAudioTrack[] music = null;

        SoundStyle musicStyle = new SoundStyle(CraftingExample.AssetPath + "Music/VentusResonat", SoundType.Music);

        public override void SetStaticDefaults()
        {
            musicDisks = new int[] { ModContent.ItemType<MusicDisk1>() };
            music = new IAudioTrack[] { MusicLoader.GetMusic(Mod, "Assets/Music/VentusResonat") };
            Main.tileShine[Type] = 1100;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            ExampleJukeboxTileEntity advancedEntity = ModContent.GetInstance<ExampleJukeboxTileEntity>();
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(advancedEntity.Hook_AfterPlacement, -1, 0, false);

            TileObjectData.addTile(Type);

            AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.MetalBar")); // localized text for "Metal Bar"
        }

        public override void PlaceInWorld(int i, int j, Item item)
        {
            ExampleJukeboxTileEntity exampleCrafterTileEntity = ModContent.GetInstance<ExampleJukeboxTileEntity>();
            exampleCrafterTileEntity.Place(i, j);
        }

        public override bool RightClick(int i, int j)
        {
            ExampleJukeboxTileEntity exampleJukeboxTileEntity = ModContent.GetInstance<ExampleJukeboxTileEntity>();

            if (TileEntity.ByID[exampleJukeboxTileEntity.Find(i, j)] != null)
            {
                Mod.Logger.Info(exampleJukeboxTileEntity.musicDisk.type);

                if (exampleJukeboxTileEntity.musicDisk == null)
                {
                    Item musicDisk = new Item();
                    musicDisk.SetDefaults(0);
                    exampleJukeboxTileEntity.musicDisk = musicDisk;
                }

                if (exampleJukeboxTileEntity.musicDisk.type == ItemID.None)
                {
                    if (musicDisks.Contains(Main.LocalPlayer.HeldItem.type))
                    {
                        exampleJukeboxTileEntity.musicDisk.SetDefaults(Main.LocalPlayer.HeldItem.type);
                        Main.LocalPlayer.HeldItem.TurnToAir();
                        Main.LocalPlayer.GetModPlayer<CraftingPlayer>().Jukebox = true;
                        Main.LocalPlayer.GetModPlayer<CraftingPlayer>().MusicDisk = MusicLoader.GetMusicSlot(Mod, "Assets/Music/VentusResonat");
                        Mod.Logger.Info(Main.LocalPlayer.GetModPlayer<CraftingPlayer>().MusicDisk);
                    }
                }
                else
                {

                    Main.LocalPlayer.GetModPlayer<CraftingPlayer>().Jukebox = false;
                    Main.LocalPlayer.QuickSpawnItemDirect(Main.LocalPlayer.GetSource_TileInteraction(i, j), exampleJukeboxTileEntity.musicDisk);
                    exampleJukeboxTileEntity.musicDisk.SetDefaults(0);
                }
            }
            
            return true;
        }
    }
}
