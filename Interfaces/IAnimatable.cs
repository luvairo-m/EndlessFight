using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessFight.Interfaces
{
    public interface IAnimatable
    {
        public void HandleAnimation(GameTime gameTime);
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
    }
}