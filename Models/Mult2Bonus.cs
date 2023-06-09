﻿using EndlessFight.Controllers;
using Microsoft.Xna.Framework;

namespace EndlessFight.Models
{
    public class Mult2Bonus : Bonus
    {
        public Mult2Bonus(Vector2 spawnPosition, TextureDescription bonusTexture)
            : base(spawnPosition, bonusTexture) { }

        public override void OnBonusApplying()
        {
            AudioController.PlayEffect(AudioController.getItem);
            Globals.Player.ShootingMultiplier = 2;
        }
    }
}