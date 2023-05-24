using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessFight.Models
{
    public class Pulsation
    {
        public int alphaValue;

        private Texture2D blankTexture;
        private Color pulseColor;
        private (int r, int g, int b) pusleColorData;

        private bool goUp = true;
        private bool goDown;
        private bool IsPulsing;

        private float realInterval = 0;
        private float duration;

        public Pulsation((int r, int g, int b) colorData, Texture2D blankTexture, float duration)
        {
            this.blankTexture = blankTexture;
            pulseColor = Color.FromNonPremultiplied(colorData.r, colorData.g, colorData.b, 1);
            pusleColorData = colorData;
            this.duration = duration;
        }

        public void Draw(SpriteBatch spriteBatch) =>
            spriteBatch.Draw(blankTexture, new Rectangle(0, 0, Game1.windowWidth, Game1.windowHeight),
                pulseColor);

        public void Update(GameTime gameTime)
        {
            if (IsPulsing)
            {
                var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
                var change = ((3000) / duration) * (delta);

                if (goUp)
                {
                    realInterval += change;
                    if (alphaValue <= 220)
                    {
                        alphaValue = (int) (realInterval + 1);
                    }
                    else
                    {
                        (goUp, goDown) = (false, true);
                        realInterval = 255f;
                    }
                    pulseColor = Color.FromNonPremultiplied(pusleColorData.r, pusleColorData.g, pusleColorData.b, alphaValue);
                }
                else if (goDown)
                {
                    realInterval -= change;
                    if (alphaValue >= 0)
                    {
                        alphaValue = (int)realInterval + 1;
                    }
                    else
                    {
                        (goUp, goDown) = (true, false);
                        realInterval = 0f;
                        IsPulsing = false;
                    }
                    pulseColor = Color.FromNonPremultiplied(pusleColorData.r, pusleColorData.g, pusleColorData.b, alphaValue);
                }
            }
        }

        public void Pulse() =>
            (goUp, goDown, IsPulsing) = (true, false, true);
    }
}