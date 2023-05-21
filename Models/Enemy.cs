using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace EndlessFight.Models
{
    public enum EnemyType { Alan, Bon, Lips }

    public abstract class Enemy
    {
        public float ShootingFrequency
        {
            get => shootingFrequency;
            set => (shootingFrequency, shootingFrequencyBuffer) = (value, value);
        }

        public Rectangle HitBox
            => new((int)Position.X, (int)Position.Y, 
                (int)(animation.Size.Width * animation.Scale),
                (int)(animation.Size.Height * animation.Scale));

        #region Fields usable by controllers
        public abstract Type EnemyType { get; }
        public Vector2 Position;
        public bool IsAlive = true;
        #endregion

        protected Vector2 defaultMovementDirection = new(0, 1);
        protected int speed;
        protected float shootingFrequency;
        protected float shootingFrequencyBuffer;
        protected SpriteAnimation animation;

        public Enemy(Vector2 spawnPosition, int speed, float shootingFrequency)
        {
            Position = spawnPosition;
            ShootingFrequency = shootingFrequency;
            this.speed = speed;
        }

        public void HandleAnimation(GameTime gameTime)
        {
            animation.Update(gameTime);
            animation.Position = Position;
        }

        public void Draw(SpriteBatch spriteBatch) => animation.Draw(spriteBatch);

        public void Update(GameTime gameTime)
        {
            HandleAnimation(gameTime);
            HandleMovement(gameTime);
            HandleShooting(gameTime);
        }

        public abstract void HandleShooting(GameTime gameTime);
        public abstract void HandleMovement(GameTime gameTime);
    }
}