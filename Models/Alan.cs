using EndlessFight.Controllers;
using Microsoft.Xna.Framework;

namespace EndlessFight.Models
{
    public class Alan : Enemy
    {
        public static TextureDescription SpriteSheet;
        public static TextureDescription BulletTexture;

        public Alan(Vector2 spawnPosition, int speed, float shootingFrequency)
            : base(spawnPosition, speed, shootingFrequency)
        {
            animation = new(SpriteSheet.Texture, SpriteSheet.Frames, SpriteSheet.Frames)
            {
                Scale = Globals.EnemyScale
            };
            timer.Tick += () =>
            {
                AudioController.PlayEffect(enemySound);

                var shootingDirection = Globals.Player.Position - Position;
                shootingDirection.Normalize();

                BulletsController.CurrentBullets.Add(
                    new(new(Position.X + HitBox.Width / 2
                    - BulletTexture.FrameWidth * Globals.BulletScale / 2,
                    Position.Y + HitBox.Height), 570, shootingDirection,
                    new(BulletTexture.Texture, BulletTexture.Frames, 3)
                    { Scale = Globals.BulletScale }, BulletOwner.Enemy));

                timer.Reset();
            };
        }
            
        public override void HandleMovement() =>
            Position += defaultMovementDirection * speed * Globals.ElapsedSeconds;
    }
}