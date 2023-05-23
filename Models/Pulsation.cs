using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessFight.Models
{
    public class Pulsation
    {
        private Texture2D blankTexture;
        private Color pulseColor;
        private (int r, int g, int b) pusleColorData;
        private float pulsingInterval = 0;

        private bool goUp = true;
        private bool goDown;
        private bool IsPulsing;

        public Pulsation((int r, int g, int b) colorData, Texture2D blankTexture)
        {
            this.blankTexture = blankTexture;
            pulseColor = Color.FromNonPremultiplied(colorData.r, colorData.g, colorData.b, 1);
            pusleColorData = colorData;
        }

        public void Draw(SpriteBatch spriteBatch) =>
            spriteBatch.Draw(blankTexture, new Rectangle(0, 0, Game1.windowWidth, Game1.windowHeight),
                pulseColor);

        public void Update(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (IsPulsing)
            {
                if (goUp)
                {
                    pulsingInterval += delta;

                    if (pulsingInterval >= 200)
                        (goUp, goDown) = (false, true);

                    pulseColor = Color.FromNonPremultiplied(pusleColorData.r, pusleColorData.g, pusleColorData.b, (int)pulsingInterval);
                }
                else if (goDown)
                {
                    pulsingInterval -= delta;

                    if (pulsingInterval <= 0)
                    {
                        pulsingInterval = 0;
                        (goUp, goDown) = (true, false);
                        IsPulsing = false;
                    }

                    pulseColor = Color.FromNonPremultiplied(pusleColorData.r, pusleColorData.g, pusleColorData.b, (int)pulsingInterval);
                }
            }
        }

        public void Pulse() =>
            (goUp, goDown, IsPulsing, pulsingInterval) = (true, false, true, 0);
    }
}