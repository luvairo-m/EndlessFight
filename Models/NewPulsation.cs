using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessFight.Models
{
    public class NewPulsation
    {
        private Texture2D blankTexture;
        private Color pulseColor;
        private (int r, int g, int b) pusleColorData;

        private float pulsingInterval;
        private float pulsingIntervalBuffer;

        private bool goUp = true;
        private bool goDown;
        private bool IsPulsing;

        private float alphaValue;
        private float step;

        public NewPulsation((int r, int g, int b) colorData, Texture2D blankTexture, float duration)
        {
            this.blankTexture = blankTexture;
            pulseColor = Color.FromNonPremultiplied(colorData.r, colorData.g, colorData.b, 1);
            pusleColorData = colorData;
            pulsingInterval = duration / (255 * 2);
            pulsingIntervalBuffer = pulsingInterval;
            step = 255 / 2 / (duration / 0.016f);
        }

        public void Draw(SpriteBatch spriteBatch) =>
            spriteBatch.Draw(blankTexture, new Rectangle(0, 0, Game1.windowWidth, Game1.windowHeight),
                pulseColor);

        public void Update(GameTime gameTime)
        {
            if (IsPulsing)
            {
                var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
                pulsingInterval -= delta;

                pulsingInterval = pulsingIntervalBuffer;

                if (goUp)
                {
                    //pulsingInterval += delta;

                    if (alphaValue >= 255)
                        (goUp, goDown) = (false, true);

                    alphaValue += step;
                    pulseColor = Color.FromNonPremultiplied(pusleColorData.r, pusleColorData.g, pusleColorData.b, (int)alphaValue);
                }
                else if (goDown)
                {
                    //pulsingInterval -= delta;

                    if (alphaValue <= 0)
                    {
                        //pulsingInterval = 0;
                        (goUp, goDown) = (true, false);
                        IsPulsing = false;
                    }

                    alphaValue -= step;
                    pulseColor = Color.FromNonPremultiplied(pusleColorData.r, pusleColorData.g, pusleColorData.b, (int)alphaValue);
                }
            }
        }

        public void Pulse() =>
            (goUp, goDown, IsPulsing, pulsingInterval) = (true, false, true, 0);
    }
}