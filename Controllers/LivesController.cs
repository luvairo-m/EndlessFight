using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using static EndlessFight.Resources;

namespace EndlessFight.Controllers
{
    public static class LivesController
    {
        private const int iconsSpacing = 15;
        private const int iconWidth = 35;
        
        public static void Draw(SpriteBatch spriteBatch)
        {
            var buffer = iconsSpacing;
            for (var i = 0; i < Globals.Player.CurrentLifes; i++)
            {
                spriteBatch.Draw(PlayerLifeTexture, 
                    new Rectangle(Game1.windowWidth - buffer - iconWidth,
                    Game1.windowHeight - 50, iconWidth, iconWidth),
                    Color.White);
                buffer += iconsSpacing + iconWidth;
            }
        }
    }
}