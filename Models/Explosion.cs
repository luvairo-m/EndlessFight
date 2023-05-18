using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessFight.Models
{
    public class Explosion 
    {
        public Vector2 Position;
        public SpriteAnimation Animation;

        public Explosion(Vector2 position, SpriteAnimation animation)
        {
            Position = position;
            Animation = animation;
        }

        public void Draw(SpriteBatch spriteBatch) => Animation.Draw(spriteBatch);
        public void Update(GameTime gameTime) => HandleAnimation(gameTime);

        public void HandleAnimation(GameTime gameTime)
        {
            Animation.Position = Position;
            Animation.Update(gameTime);
        }
    }
}