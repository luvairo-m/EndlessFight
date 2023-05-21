using Microsoft.Xna.Framework;
using System;

namespace EndlessFight.Models
{
    public class Bon : Enemy
    {
        public static TextureDescription SpriteSheet;

        public override Type EnemyType => GetType();

        public Bon(Vector2 spawnPosition, int speed, float shootingFrequency)
            : base(spawnPosition, speed, shootingFrequency)
        {
            animation = new(SpriteSheet.Texture, SpriteSheet.Frames, SpriteSheet.Frames)
            {
                Scale = Globals.EnemyScale
            };
            timer.Tick += () => { };
        }

        public override void HandleMovement()
        {
            var movementDirection = defaultMovementDirection;

            if (Globals.Player.Position.Y > Position.Y)
            {
                movementDirection = Globals.Player.Position - Position;
                movementDirection.Normalize();
            }

            Position += movementDirection * speed * Globals.ElapsedSeconds;
        }
    }
}