using EndlessFight.Models;
using System;

namespace EndlessFight
{
    public struct Globals
    {
        public static Random Randomizer = new();
        public const float BulletScale = 3.5f;
        public const float BlasterScale = 2.5f;
        public const float EnemyScale = 4f;
        public const float ExplosionScale = 3.7f;
        public const int PlayerShipSize = 80;
        public const int EnemySpawnOffset = 100;
        public static Player Player;
    }
}