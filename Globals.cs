using EndlessFight.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;

namespace EndlessFight
{
    public struct Globals
    {
        public static float ElapsedSeconds;
        public static Random Randomizer = new();
        public static Pulsation HitPulsation;
        public static Pulsation HealPulsation;
        public static Player Player;
        public const float BulletScale = 3.5f;
        public const float BlasterScale = 2.5f;
        public const float EnemyScale = 4f;
        public const float ExplosionScale = 4.5f;
        public const int PlayerShipSize = 80;
        public const int EnemySpawnOffset = 300;
        public const int PlayerBaseSpeed = 430;
        public const string SerializationPath = "userdata.json";
        public const float TimeToChengeDifficulty = 2;
        public const float MaxDifficult = 0.5f;
        public const float DeltaDifficultChange = 1f;
        public const float BonusScale = 3.5f;
        public static SoundEffect enemyShoot;

        public static void Update(GameTime gameTime) 
            => ElapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
}