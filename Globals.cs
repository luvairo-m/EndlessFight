using EndlessFight.Models;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace EndlessFight
{
    public struct Globals
    {
        public static Random Randomizer = new();
        public static HitModel HitModel;
        public const float BulletScale = 3.5f;
        public const float BlasterScale = 2.5f;
        public const float EnemyScale = 4f;
        public const float ExplosionScale = 3.7f;
        public const int PlayerShipSize = 80;
        public const int EnemySpawnOffset = 100;
        public const int PlayerBaseSpeed = 430;
        public static Player Player;
        public const string serializationPath = "userdata.json";
        public const float TimeToChengeDifficulty = 2;
        public const float MaxDifficult = 0.5f;
        public const float DeltaDifficultChange = 0.01f;
    }
}