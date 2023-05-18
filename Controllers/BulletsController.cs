using EndlessFight.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace EndlessFight.Controllers
{
    public static class BulletsController
    {
        public static readonly List<Bullet> CurrentBullets = new();

        public static void Update(GameTime gameTime)
        {
            CurrentBullets.ForEach(bullet => bullet.Update(gameTime));
            for (var i = 0; i < CurrentBullets.Count; i++)
            {
                var bullet = CurrentBullets[i];
                if (bullet.Position.Y < -bullet.HitBox.Height
                    || bullet.Position.Y > Game1.windowHeight + bullet.HitBox.Height
                    || bullet.Position.X < Game1.fieldOffset
                    || bullet.Position.X > Game1.windowWidth + Game1.fieldOffset)
                    CurrentBullets.RemoveAt(i);
            }

            DeleteDeadOnes();
        }

        public static void Draw(SpriteBatch spriteBatch) => CurrentBullets.ForEach(bullet => bullet.Draw(spriteBatch));

        private static void DeleteDeadOnes()
        {
            for (var i = 0; i < CurrentBullets.Count; i++)
                if (!CurrentBullets[i].IsAlive)
                    CurrentBullets.RemoveAt(i);
        }
    }
}