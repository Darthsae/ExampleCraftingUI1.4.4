using Terraria.ModLoader;

namespace CraftingExample.Content.Items
{
    public class ExampleJukebox : ModItem
    {
        public override void SetDefaults()
        {
            // ModContent.TileType returns the ID of the tile that this item should place when used. ModContent.TileType<T>() method returns an integer ID of the tile provided to it through its generic type argument (the type in angle brackets)
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.ExampleJukebox>());
            Item.width = 20;
            Item.height = 20;
            Item.value = 750; // The cost of the item in copper coins. (1 = 1 copper, 100 = 1 silver, 1000 = 1 gold, 10000 = 1 platinum)
        }
    }
}
