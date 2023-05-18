using EndlessFight.Models;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace EndlessFight.Controllers
{
    public static class EnemiesController
    {
        public static float SpawnInterval
        {
            get => spawnInterval;
            set => (spawnInterval, spawnIntefvalBuffer) = (value, value);
        }

        public static readonly List<Enemy> CurrentEnemies = new();

        private static float spawnInterval;
        private static float spawnIntefvalBuffer;

        private static Dictionary<EnemyType, int> enemiesCosts = new()
        {
            { EnemyType.Lips, 10 },
            { EnemyType.Bon, 20 },
            { EnemyType.Alan, 30 }
        };

        public static void Update(GameTime gameTime)
        {
            HandleSpawning(gameTime);
            HandleCollision();

            CurrentEnemies.ForEach(enemy => enemy.Update(gameTime));
            for (var i = 0; i < CurrentEnemies.Count; i++)
                if (CurrentEnemies[i].Position.Y > Game1.windowHeight + Globals.EnemySpawnOffset)
                    CurrentEnemies.RemoveAt(i);

            DeleteDeadOnes();
        }

        public static void Draw(SpriteBatch spriteBatch) => CurrentEnemies.ForEach(enemy => enemy.Draw(spriteBatch));

        private static void HandleCollision()
        {
            foreach (var enemy in CurrentEnemies)
                if (enemy.HitBox.Intersects(Globals.Player.HitBox))
                {
                    enemy.IsAlive = false;
                    if (Player.CurrentLifes > 0)
                        Player.CurrentLifes--;
                }
        }

        private static void HandleSpawning(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            spawnInterval -= delta;

            if (SpawnInterval <= 0)
            {
                spawnInterval = spawnIntefvalBuffer;

                for (var i = 0; i < 2; i++)
                {
                    var enemyType = PickRandomEnemyType();
                    var enemy = EnemyBuilder.BuildEnemy(enemyType,
                        new(Globals.Randomizer.Next(Globals.EnemySpawnOffset, Game1.windowWidth - Globals.EnemySpawnOffset),
                        Globals.Randomizer.Next(-Game1.windowHeight, -Globals.EnemySpawnOffset)),
                        enemyType != EnemyType.Bon ? Globals.Randomizer.Next(100, 500 + 1)
                        : Globals.Randomizer.Next(300, 350 + 1), 1.3f);
                    CurrentEnemies.Add(enemy);
                }
            }
        }

        private static void DeleteDeadOnes()
        {
            for (var i = 0; i < CurrentEnemies.Count; i++)
                if (!CurrentEnemies[i].IsAlive)
                {
                    var enemy = CurrentEnemies[i];
                    ExplosionContoller.CreateExplosion(
                        new(enemy.Position.X + enemy.HitBox.Width / 2,
                        enemy.Position.Y + enemy.HitBox.Height / 2));
                    CurrentEnemies.RemoveAt(i);
                    ScoreController.Score += enemiesCosts[enemy.EnemyType];
                }
        }

        private static EnemyType PickRandomEnemyType()
        {
            var number = Globals.Randomizer.Next(100 + 1);
            if (number < 80) return EnemyType.Lips;
            else if (number > 80 && number <= 95) return EnemyType.Bon;
            else return EnemyType.Alan;
        }
    }
}