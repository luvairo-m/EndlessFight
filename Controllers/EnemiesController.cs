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

        private static readonly GrigoryTimer timer;

        private static readonly Dictionary<Type, int> enemiesCosts = new()
        {
            { typeof(Lips), 10 },
            { typeof(Bon), 20 },
            { typeof(Alan), 30 }
        };

        static EnemiesController()
        {
            timer = new(1.8f);
            timer.Tick += () =>
            {
                var spawnPosition = CurrentEnemies.Count == 0 ? new Vector2(Globals.Randomizer.Next
                    (Globals.EnemySpawnOffset, Game1.windowWidth - Globals.EnemySpawnOffset), -Globals.EnemySpawnOffset)
                    : new Vector2(RandomIntWithoutSegment(10, Game1.windowWidth - 50, ((int)CurrentEnemies[^1].Position.X) - 50,
                    ((int)CurrentEnemies[^1].Position.X) + Globals.EnemySpawnOffset + 50), -Globals.EnemySpawnOffset);

                var enemyType = PickRandomEnemyType();
                var speed = Globals.Randomizer.Next(300, 350 + 1);

                if (enemyType == typeof(Bon))
                    speed = Globals.Randomizer.Next(400, 500 + 1);
                    
                var enemy = (Enemy)Activator.CreateInstance(enemyType, spawnPosition, speed, 1f);

                CurrentEnemies.Add(enemy);

                //timer.Interval = spawnFrequencyBuffer;
                timer.Reset();
            };
        }

        public static void Update(GameTime gameTime)
        {
            HandleSpawning();
            HandleCollision();
            HandleDifficulty();

            CurrentEnemies.ForEach(enemy => enemy.Update());
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
                    AudioController.PlayEffect(AudioController.getDamage);
                    enemy.IsAlive = false;
                    if (Globals.Player.CurrentLifes > 0)
                        Globals.Player.CurrentLifes--;
                    Globals.HitPulsation.Pulse();
                }
        }

        private static void HandleSpawning() => timer.Update();

        private static int RandomIntWithoutSegment(int left, int right, int inLeft, int inRight)
        {
            var randomValue = Globals.Randomizer.Next(left, right - (inRight - inLeft));
            randomValue += (randomValue > inLeft) ? (inRight - inLeft) : 0;
            return randomValue;
        }

        private static void HandleDifficulty()
        {
            difficultyInterval += Globals.ElapsedSeconds;
            if (difficultyInterval > Globals.TimeToChengeDifficulty)
            {
                difficultyInterval = 0;
                spawnFrequencyBuffer -= (spawnFrequencyBuffer <= Globals.MaxDifficult) 
                    ? 0 : Globals.DeltaDifficultChange;
            }
        }

        private static void DeleteDeadOnes()
        {
            for (var i = 0; i < CurrentEnemies.Count; i++)
                if (!CurrentEnemies[i].IsAlive)
                {
                    var enemy = CurrentEnemies[i];
                    var position = new Vector2(enemy.Position.X + enemy.HitBox.Width / 2,
                        enemy.Position.Y + enemy.HitBox.Height / 2);
                    ExplosionContoller.CreateExplosion(position);
                    BonusesController.CreateBonus(position);
                    CurrentEnemies.RemoveAt(i);
                    ScoreController.Score += enemiesCosts[enemy.GetType()];
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