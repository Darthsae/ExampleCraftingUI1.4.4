using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CraftingExample.Content.TileEntities
{
    public class ExampleJukeboxTileEntity : ModTileEntity
    {
        public Item musicDisk = new ();

        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile tile = Main.tile[x, y];
            return tile.HasTile;
        }

        public override void LoadData(TagCompound tag)
        {
            musicDisk = tag.Get<Item>("musicDisk");
        }

        public override void SaveData(TagCompound tag)
        {
            tag["musicDisk"] = musicDisk;
        }

        public override void OnNetPlace()
        {
            Mod.Logger.Info("Place");

            // This hook is only ever called on the server; its purpose is to give more freedom in terms of syncing FROM the server to clients, which we take advantage of
            // by making sure to sync whenever this hook is called:
            NetMessage.SendData(MessageID.TileEntitySharing, number: ID, number2: Position.X, number3: Position.Y);
        }

        public override void NetSend(BinaryWriter writer)
        {
            // We want to make sure that our data is synced properly across clients and server.
            // NetSend is called whenever a TileEntitySharing message is sent, so the game will handle this automatically for us,
            // granted that we send a message when we need to.
            writer.Write(musicDisk.type);
            writer.Write(musicDisk.stack);
        }

        public override void NetReceive(BinaryReader reader)
        {
            Item item = new();
            item.SetDefaults(reader.Read());
            item.stack = reader.Read();
            musicDisk = item;
        }
    }
}
