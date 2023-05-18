using EndlessFight.Controllers;
using EndlessFight.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace EndlessFight.Models
{
    public enum EnemyType { Alan, Bon, Lips }

    public class Enemy : IMovable, IHittable
    {
        public Vector2 Position { get => position; set => position = value; }
        public int Speed { get => speed; set => speed = value; }
        public bool IsAlive { get => isAlive; set => isAlive = value; }
        public EnemyType EnemyType;

        public Rectangle HitBox
            => new((int)position.X, (int)position.Y,
                (int)(animation.Size.Width * animation.Scale),
                (int)(animation.Size.Height * animation.Scale));

        public float ShootingInterval
        {
            get => shootingInterval;
            set => (shootingInterval, shootingIntervalBuffer) = (value, value);
        }

        private Vector2 position;
        private int speed;
        private bool isAlive = true;
        private SpriteAnimation animation;
        private float shootingInterval;
        private float shootingIntervalBuffer;

        private TextureDescription bulletTexture;

        public Enemy(Vector2 spawnPosition, int speed, float shootingInterval,
            TextureDescription modelTexture, TextureDescription bulletTexture,
            EnemyType enemyType)
        {
            this.bulletTexture = bulletTexture;
            this.speed = speed;
            this.EnemyType = enemyType;
            ShootingInterval = shootingInterval;
            position = spawnPosition;
            animation = new(modelTexture.Texture, modelTexture.Frames, modelTexture.Frames)
            {
                Scale = Globals.EnemyScale
            };
        }

        public void HandleAnimation(GameTime gameTime)
        {
            animation.Update(gameTime);
            animation.Position = position;
        }

        public void HandleMovement(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (EnemyType == EnemyType.Bon && position.Y < Globals.Player.Position.Y)
            {
                var direction = Globals.Player.Position - position;
                direction.Normalize();
                position += direction * Math.Abs(speed) * delta;
            } else
                position.Y += delta * speed;
        }

        public void Draw(SpriteBatch spriteBatch) => animation.Draw(spriteBatch);

        public void Update(GameTime gameTime)
        {
            HandleAnimation(gameTime);
            HandleMovement(gameTime);

            if (EnemyType != EnemyType.Bon)
                HandleShooting(gameTime);
        }

        private void HandleShooting(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            shootingInterval -= delta;

            if (shootingInterval <= 0)
            {
                shootingInterval = shootingIntervalBuffer;

                var direction = EnemyType == EnemyType.Alan
                    ? Globals.Player.Position - position
                    : new Vector2(0, 1);
                direction.Normalize();

                BulletsController.CurrentBullets.Add(
                    new(new(position.X + HitBox.Width / 2
                    - bulletTexture.FrameWidth * Globals.BulletScale / 2
                    , position.Y + HitBox.Height), 570, direction,
                    new(bulletTexture.Texture, bulletTexture.Frames, 3)
                    { Scale = Globals.BulletScale },
                    BulletOwner.Enemy));
            }
        }
    }
}