using Microsoft.Xna.Framework;
using System;

namespace EndlessFight.Models
{
    public class Bon : Enemy
    {
        public static TextureDescription SpriteSheet;

        public override Type EnemyType => GetType();

        public Bon(Vector2 spawnPosition, int speed, float shootingFrequency)
            : base(spawnPosition, speed, shootingFrequency) =>
            animation = new(SpriteSheet.Texture, SpriteSheet.Frames, SpriteSheet.Frames)
            {
                Scale = Globals.EnemyScale
            };

        public override void HandleShooting(GameTime gameTime) { }

        public override void HandleMovement(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 movementDirection = defaultMovementDirection;

            if (Globals.Player.Position.Y > Position.Y)
            {
                movementDirection = Globals.Player.Position - Position;
                movementDirection.Normalize();
            }

            Position += movementDirection * speed * delta;
        }
    }
}