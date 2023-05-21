using EndlessFight.Models;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace EndlessFight.Controllers
{
    public static class EnemiesController
    {
        public static float SpawnFrequency
        {
            get => spawnFrequency;
            set => (spawnFrequency, spawnFrequencyBuffer) = (value, value);
        }

        public static readonly List<Enemy> CurrentEnemies = new();

        private static float spawnFrequencyBuffer;
        private static float spawnFrequency;
        private static float difficultyInterval = 0;

        private static readonly Dictionary<Type, int> enemiesCosts = new()
        {
            { typeof(Lips), 10 },
            { typeof(Bon), 20 },
            { typeof(Alan), 30 }
        };

        public static void Update(GameTime gameTime)
        {
            HandleSpawning(gameTime);
            HandleCollision();
            HandleDifficulty(gameTime);

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
                    Globals.HitModel.SetPulsing();
                }
        }

        private static void HandleSpawning(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            spawnFrequency -= delta;

            if (SpawnFrequency <= 0)
            {
                spawnFrequency = spawnFrequencyBuffer;

                var spawnPosition = CurrentEnemies.Count == 0 ? new Vector2(Globals.Randomizer.Next
                    (Globals.EnemySpawnOffset, Game1.windowWidth - Globals.EnemySpawnOffset), -Globals.EnemySpawnOffset)
                    : new(RandomIntWithoutSegment(10, Game1.windowWidth - 50, ((int)CurrentEnemies[^1].Position.X) - 50, 
                    ((int)CurrentEnemies[^1].Position.X) + Globals.EnemySpawnOffset + 50), -Globals.EnemySpawnOffset);
                var speed = Globals.Randomizer.Next(300, 350 + 1);
                var enemy = (Enemy)Activator.CreateInstance(PickRandomEnemyType(), spawnPosition, speed, 1.3f);
                CurrentEnemies.Add(enemy);
            }
        }

        private static int RandomIntWithoutSegment(int left, int right, int inLeft, int inRight)
        {
            int rnd = Globals.Randomizer.Next(left, right - (inRight - inLeft));
            rnd += (rnd > inLeft) ? (inRight - inLeft) : 0;
            return rnd;
        }

        private static void HandleDifficulty(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            difficultyInterval += delta;
            if (difficultyInterval > Globals.TimeToChengeDifficulty)
            {
                difficultyInterval = 0;
                spawnFrequencyBuffer -= (spawnFrequencyBuffer <= Globals.MaxDifficult) ? 0 : Globals.DeltaDifficultChange;
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

        private static Type PickRandomEnemyType()
        {
            var number = Globals.Randomizer.Next(100 + 1);
            if (number <= 70) return typeof(Lips);
            else if (number > 70 && number <= 85) return typeof(Bon);
            else return typeof(Alan);
        }
    }
}