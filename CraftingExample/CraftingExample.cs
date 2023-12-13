using Terraria.ModLoader;

namespace CraftingExample
{
	public class CraftingExample : Mod
	{
        public const string AssetPath = $"{nameof(CraftingExample)}/Assets/";

        public override void Load()
        {
            MusicLoader.AddMusic(this, "Assets/Music/VentusResonat");
        }
    }
}