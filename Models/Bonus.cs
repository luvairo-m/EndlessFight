using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessFight.Models
{
    public abstract class Bonus
    {
        public Rectangle HitBox
            => new((int)Position.X, (int)Position.Y,
            (int)(animation.Size.Width * animation.Scale),
            (int)(animation.Size.Height * animation.Scale));

        public bool IsAlive = true;
        public Vector2 Position;

        protected SpriteAnimation animation;

        private const int speed = 200;
        private Vector2 defaultMovementDirection = new(0, 1);

        public Bonus(Vector2 spawnPosition, TextureDescription bonusTexture)
        {
            Position = spawnPosition;
            animation = new(bonusTexture.Texture, bonusTexture.Frames, bonusTexture.Frames)
            {
                Scale = Globals.BonusScale
            };
        }

        public void Update()
        {
            HandleAnimation();
            HandleMovement();
        }

        public void HandleAnimation()
        {
            animation.Position = Position;
            animation.Update();
        }

        public void Draw(SpriteBatch spriteBatch) => animation.Draw(spriteBatch);

        public void HandleMovement() =>
            Position += Globals.ElapsedSeconds * speed * defaultMovementDirection;

        public abstract void OnBonusApplying();
    }
}