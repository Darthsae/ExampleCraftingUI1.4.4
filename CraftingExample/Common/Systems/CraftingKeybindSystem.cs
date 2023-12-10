using Terraria.ModLoader;

namespace CraftingExample.Common.Systems
{
    // Acts as a container for keybinds registered by this mod.
    // See Common/Players/ExampleKeybindPlayer for usage.
    public class CraftingKeybindSystem : ModSystem
    {
        public static ModKeybind Crafting { get; private set; }

        public override void Load()
        {
            // Registers a new keybind
            // We localize keybinds by adding a Mods.{ModName}.Keybind.{KeybindName} entry to our localization files. The actual name displayed to English users is in en-US.hjson
            Crafting = KeybindLoader.RegisterKeybind(Mod, "Crafting", "U");
        }

        // Please see ExampleMod.cs' Unload() method for a detailed explanation of the unloading process.
        public override void Unload()
        {
            // Not required if your AssemblyLoadContext is unloading properly, but nulling out static fields can help you figure out what's keeping it loaded.
            Crafting = null;
        }
    }
}