using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CraftingExample.Content.TileEntities
{
    public class ExampleCrafterTileEntity : ModTileEntity
    {
        public Item[] items = new Item[] { null, null };

        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile tile = Main.tile[x, y];
            return tile.HasTile;
        }

        public override void LoadData(TagCompound tag)
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = tag.Get<Item>("item" + i);
            }
        }

        public override void SaveData(TagCompound tag)
        {
            for (int i = 0; i < items.Length; i++)
            {
                tag["item" + i] = items[i];
            }
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
            for (int i = 0; i < items.Length;i++)
            {
                writer.Write(items[i].type);
                writer.Write(items[i].stack);
            }
        }

        public override void NetReceive(BinaryReader reader)
        {
            for (int i = 0; i < items.Length; i++)
            {
                Item item = new();
                item.SetDefaults(reader.Read());
                item.stack = reader.Read();
                items[i] = item;
            }
        }
    }
}
