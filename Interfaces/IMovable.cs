using Microsoft.Xna.Framework;

namespace EndlessFight.Interfaces
{
    public interface IMovable
    {
        public Vector2 Position { get; set; }
        public int Speed { get; set; }
        public void HandleMovement(GameTime gameTime);
    }
}