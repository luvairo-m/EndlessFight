using EndlessFight.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessFight.Controllers
{
    public static class LivesController
    {
        public static Texture2D LifeIconTexture;
        private const int iconsSpacing = 15;
        private const int iconWidth = 35;
        
        public static void Draw(SpriteBatch spriteBatch)
        {
            var buffer = iconsSpacing;
            for (var i = 0; i < Globals.Player.CurrentLifes; i++)
            {
                spriteBatch.Draw(LifeIconTexture, 
                    new Rectangle(Game1.windowWidth - buffer - iconWidth,
                    Game1.windowHeight - 50, iconWidth, iconWidth),
                    Color.White);
                buffer += iconsSpacing + iconWidth;
            }
        }
    }
}