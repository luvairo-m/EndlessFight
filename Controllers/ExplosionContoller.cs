using EndlessFight.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace EndlessFight.Controllers
{
    public static class ExplosionContoller
    {
        public static TextureDescription[] ExplosionTextures;
        public static List<Explosion> CurrentExplosions = new();

        public static void CreateExplosion(Vector2 explosionPosition)
        {
            var number = Globals.Randomizer.Next(ExplosionTextures.Length);
            var explosion = ExplosionTextures[number];
            CurrentExplosions.Add(new(
                new(explosionPosition.X - explosion.FrameWidth * Globals.ExplosionScale / 2,
                explosionPosition.Y - explosion.FrameWidth * Globals.ExplosionScale / 2),
                new(explosion.Texture, explosion.Frames, 7) { Scale = Globals.ExplosionScale }));
        }

        public static void Update()
        {
            CurrentExplosions.ForEach(explosion => explosion.Update());
            ControlExplosions();
        }

        public static void Draw(SpriteBatch spriteBatch)
            => CurrentExplosions.ForEach(explosion => explosion.Animation.Draw(spriteBatch));

        private static void ControlExplosions()
        {
            for (var i = 0; i < CurrentExplosions.Count; i++)
                if (CurrentExplosions[i].Animation.FrameIndex == 4)
                    CurrentExplosions.RemoveAt(i);
        }
    }
}