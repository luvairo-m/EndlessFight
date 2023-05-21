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
            //var enemyType = PickRandomEnemyType();
            //var enemy = EnemyBuilder.BuildEnemy(enemyType,
            //    new(Globals.Randomizer.Next(Globals.EnemySpawnOffset, Game1.windowWidth - Globals.EnemySpawnOffset),
            //    Globals.Randomizer.Next(-Game1.windowHeight, -Globals.EnemySpawnOffset)),
            //    enemyType != EnemyType.Bon ? Globals.Randomizer.Next(100, 500 + 1)
            //    : Globals.Randomizer.Next(300, 350 + 1), 1.3f);
            //CurrentEnemies.Add(enemy);
        }

        public override void HandleMovement(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += defaultMovementDirection * speed * delta;
        }
    }
}