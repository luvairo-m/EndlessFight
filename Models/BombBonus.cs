﻿using EndlessFight.Controllers;
using Microsoft.Xna.Framework;

namespace EndlessFight.Models
{
    public class BombBonus : Bonus
    {
        public BombBonus(Vector2 spawnPosition, TextureDescription bonusTexture)
            : base(spawnPosition, bonusTexture) { }

        public override void OnBonusApplying() =>
            EnemiesController.CurrentEnemies.ForEach(enemy => enemy.IsAlive = false);
    }
}