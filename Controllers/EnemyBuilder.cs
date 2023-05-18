using EndlessFight.Models;
using Microsoft.Xna.Framework;
using System;

namespace EndlessFight.Controllers
{
    public static class EnemyBuilder
    {
        public static TextureDescription[] EnemyTextures;
        public static TextureDescription[] EnemyBullets;

        public static Enemy BuildEnemy(EnemyType enemyType, Vector2 position,
            int speed, float shootingInterval)
        {
            return enemyType switch
            {
                EnemyType.Alan => new(position, speed, shootingInterval, EnemyTextures[0], EnemyBullets[1], enemyType),
                EnemyType.Bon => new(position, speed, shootingInterval, EnemyTextures[1], EnemyBullets[0], enemyType),
                EnemyType.Lips => new(position, speed, shootingInterval, EnemyTextures[2], EnemyBullets[0], enemyType),
                _ => throw new ArgumentException("Enemy not found"),
            };
        }
    }
}