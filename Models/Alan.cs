using EndlessFight.Controllers;
using Microsoft.Xna.Framework;
using System;

namespace EndlessFight.Models
{
    public class Alan : Enemy
    {
        public static TextureDescription SpriteSheet;
        public static TextureDescription BulletTexture;

        public override Type EnemyType => GetType();

        public Alan(Vector2 spawnPosition, int speed, float shootingFrequency)
            : base(spawnPosition, speed, shootingFrequency) =>
            animation = new(SpriteSheet.Texture, SpriteSheet.Frames, SpriteSheet.Frames)
            {
                Scale = Globals.EnemyScale
            };

        public override void HandleShooting(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            shootingFrequency -= delta;

            if (shootingFrequency <= 0)
            {
                shootingFrequency = shootingFrequencyBuffer;

                var shootingDirection = Globals.Player.Position - Position;
                shootingDirection.Normalize();

                BulletsController.CurrentBullets.Add(
                    new(new(Position.X + HitBox.Width / 2
                    - BulletTexture.FrameWidth * Globals.BulletScale / 2,
                    Position.Y + HitBox.Height), 570, shootingDirection,
                    new(BulletTexture.Texture, BulletTexture.Frames, 3)
                    { Scale = Globals.BulletScale }, BulletOwner.Enemy));
            }
        }

        public override void HandleMovement(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += defaultMovementDirection * speed * delta;
        }
    }
}