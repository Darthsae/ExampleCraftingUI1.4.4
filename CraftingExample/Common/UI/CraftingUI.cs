using CraftingExample.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.ModLoader.UI;
using Terraria.ID;

namespace CraftingExample.Common.UI
{
    // This custom UI will show whenever the player is holding the ExampleCustomResourceWeapon item and will display the player's custom resource amounts that are tracked in ExampleResourcePlayer
    internal class CraftingUIState : UIState
    {
        // For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
        // Once this is all set up make sure to go and do the required stuff for most UI's in the ModSystem class.
        private UIElement area;
        private UIImage background;
        private VanillaItemSlotWrapper slotOne;
        private VanillaItemSlotWrapper slotTwo;
        private UIButton<string> button;
        private UIButton<string> close;

        public override void OnInitialize()
        {
            // Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
            // UIElement is invisible and has no padding.
            area = new UIElement();
            area.Left.Set(-area.Width.Pixels - 850, 1f); // Place the resource bar to the left of the hearts.
            area.Top.Set(50, 0f); // Placing it just a bit below the top of the screen.
            area.Width.Set(550, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
            area.Height.Set(500, 0f);

            background = new UIImage(ModContent.Request<Texture2D>("CraftingExample/Common/UI/CraftingBackground"));
            background.Left.Set(0, 0f);
            background.Top.Set(0, 0f);
            background.Width.Set(250, 0f);
            background.Height.Set(250, 0f);

            slotOne = new VanillaItemSlotWrapper();
            slotOne.Width.Set(50, 0f);
            slotOne.Height.Set(50, 0f);
            slotOne.Top.Set(50, 0f);
            slotOne.Left.Set(50, 0f);

            slotTwo = new VanillaItemSlotWrapper();
            slotTwo.Width.Set(50, 0f);
            slotTwo.Height.Set(50, 0f);
            slotTwo.Top.Set(50, 0f);
            slotTwo.Left.Set(150, 0f);

            button = new UIButton<string>("Recycle");
            button.Width.Set(150, 0f);
            button.Height.Set(50, 0f);
            button.Top.Set(125, 0f);
            button.Left.Set(50, 0f);

            close = new UIButton<string>("Close");
            close.Width.Set(150, 0f);
            close.Height.Set(50, 0f);
            close.Top.Set(175, 0f);
            close.Left.Set(50, 0f);

            area.Append(background);
            area.Append(slotOne);
            area.Append(slotTwo);
            area.Append(button);
            area.Append(close);
            Append(area);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // This prevents drawing unless we are using an ExampleCustomResourceWeapon
            if (!Main.LocalPlayer.GetModPlayer<CraftingPlayer>().crafting()) return;

            base.Draw(spriteBatch);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (Main.LocalPlayer.TryGetModPlayer(out CraftingPlayer modPlayer) && !modPlayer.crafting())
                return;

            if (modPlayer.quick_update)
            {
                slotOne.Item = modPlayer.craftingTileEntity.items[0];
                slotTwo.Item = modPlayer.craftingTileEntity.items[1];
                modPlayer.quick_update = false;
            }
            else if (modPlayer.quick_save)
            {
                modPlayer.craftingTileEntity.items[0] = slotOne.Item;
                modPlayer.craftingTileEntity.items[1] = slotTwo.Item;
                modPlayer.quick_save = false;
                modPlayer.crafting_ui = false;
            }

            base.Update(gameTime);
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            if (Main.LocalPlayer.TryGetModPlayer(out CraftingPlayer modPlayer) && (!modPlayer.crafting()))
                return;

            if (button.IsMouseHovering)
            {
                switch (slotOne.Item.type) { 
                    case ItemID.StoneBlock:
                        
                        if (slotTwo.Item.type == ItemID.None)
                        {
                            Item item = new();
                            item.SetDefaults(ItemID.DirtBlock);
                            slotTwo.Item = item;
                        }
                        else if (slotTwo.Item.type == ItemID.DirtBlock)
                        {
                            slotTwo.Item.stack++;
                        }
                        else
                        {
                            break;
                        }
                        slotOne.Item.stack--;
                        modPlayer.craftingTileEntity.items[0] = slotOne.Item;
                        modPlayer.craftingTileEntity.items[1] = slotTwo.Item;
                        break;
                }
            }
            else if (close.IsMouseHovering)
            {
                modPlayer.craftingTileEntity.items[0] = slotOne.Item;
                modPlayer.craftingTileEntity.items[1] = slotTwo.Item;
                modPlayer.crafting_ui = false;
                modPlayer.craftingTileEntity = null;
            }
        }
    }

    // This class will only be autoloaded/registered if we're not loading on a server
    [Autoload(Side = ModSide.Client)]
    internal class CraftingUISystem : ModSystem
    {
        private UserInterface CraftingUserInterface;

        internal CraftingUIState CraftingUIState;

        public override void Load()
        {
            CraftingUIState = new();
            CraftingUserInterface = new();
            CraftingUserInterface.SetState(CraftingUIState);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            CraftingUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "HardToLessHard: Crafting",
                    delegate {
                        CraftingUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
