using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessFight.Models
{
    public enum BulletOwner { Enemy, Player };

    public class Bullet 
    {
        public Vector2 Position { get => position; set => position = value; }
        public int Speed { get => speed; set => speed = value; }
        public Rectangle HitBox
            => new((int)position.X, (int)position.Y,
                (int)(animation.Size.Width * animation.Scale),
                (int)(animation.Size.Height * animation.Scale));
        public bool IsAlive { get => isAlive; set => isAlive = value; }
        public BulletOwner Owner;

        private Vector2 position;
        private int speed;
        private bool isAlive = true;
        private Vector2 direction;
        private readonly SpriteAnimation animation;

        public Bullet(Vector2 spawnPosition, int speed, Vector2 direction,
            SpriteAnimation animation, BulletOwner owner)
        {
            Owner = owner;
            position = spawnPosition;
            this.direction = direction;
            this.animation = animation;
            this.speed = speed;
        }

        public void Update(GameTime gameTime)
        {
            HandleAnimation(gameTime);
            HandleMovement(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch) => animation.Draw(spriteBatch);

        public void HandleAnimation(GameTime gameTime)
        {
            animation.Update(gameTime);
            animation.Position = position;
        }

        public void HandleMovement(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += direction * delta * speed;
        }
    }
}