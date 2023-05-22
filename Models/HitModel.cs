using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessFight.Models
{
    public class HitModel
    {
        public Texture2D BlankTexture;

        private Color color = Color.FromNonPremultiplied(255, 0, 0, 1);
        private float pulsingInterval = 0;

        private bool goUp = true;
        private bool goDown;
        private bool IsPulsing;

        public void Draw(SpriteBatch spriteBatch) =>
            spriteBatch.Draw(BlankTexture, new Rectangle(0, 0, Game1.windowWidth, Game1.windowHeight), color);

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

                    color = Color.FromNonPremultiplied(255, 0, 0, (int)pulsingInterval);
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

                    color = Color.FromNonPremultiplied(255, 0, 0, (int)pulsingInterval);
                }
            }
        }

        public void SetPulsing() =>
            (goUp, goDown, IsPulsing, pulsingInterval) = (true, false, true, 0);
    }
}