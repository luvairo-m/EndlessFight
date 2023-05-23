using EndlessFight.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace EndlessFight.Models
{
    public class Lips : Enemy
    {
        public static TextureDescription SpriteSheet;
        public static TextureDescription BulletTexture;

        public Lips(Vector2 spawnPosition, int speed, float shootingFrequency) 
            : base(spawnPosition, speed, shootingFrequency)
        {
            animation = new(SpriteSheet.Texture, SpriteSheet.Frames, SpriteSheet.Frames) 
            { 
                Scale = Globals.EnemyScale
            };
            timer.Tick += () =>
            {
                AudioController.PlayEffect(enemySound);

                BulletsController.CurrentBullets.Add(
                    new(new(Position.X + HitBox.Width / 2
                    - BulletTexture.FrameWidth * Globals.BulletScale / 2,
                    Position.Y + HitBox.Height), 570, defaultMovementDirection,
                    new(BulletTexture.Texture, BulletTexture.Frames, 3)
                    { Scale = Globals.BulletScale }, BulletOwner.Enemy));
                timer.Reset();
            };
        }

        public override void HandleMovement() =>
            Position += defaultMovementDirection * speed * Globals.ElapsedSeconds;
    }
}