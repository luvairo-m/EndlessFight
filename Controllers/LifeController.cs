﻿using EndlessFight.Models;

namespace EndlessFight.Controllers
{
    public static class LifeController
    {
        public static void ControlLifeStatus()
        {
            var bullets = BulletsController.CurrentBullets;
            var enemies = EnemiesController.CurrentEnemies;

            foreach (var bullet in bullets) 
                foreach (var enemy in enemies)
                {
                    if (bullet.HitBox.Intersects(enemy.HitBox) && bullet.Owner != BulletOwner.Enemy)
                    {
                        AudioController.PlayEffect(AudioController.hit);
                        (bullet.IsAlive, enemy.IsAlive) = (false, false);
                    }
                }

            foreach (var bonus in BonusesController.CurrentBonuses)
                if (bonus.HitBox.Intersects(Globals.Player.HitBox))
                    bonus.IsAlive = false;
        }

        public static void OnPlayerDamaged()
        {
            if (Globals.Player.CurrentLifes > 0)
            {
                Globals.Player.CurrentLifes--;
                Globals.HitPulsation.Pulse();
            }

            if (Globals.Player.CurrentLifes <= 0)
            {
                AudioController.mainTheme.soundEffectInstance.Stop();
                Globals.MainGame.ChangeState();
                EnemiesController.CurrentEnemies.Clear();
                BulletsController.CurrentBullets.Clear();
                ExplosionContoller.CurrentExplosions.Clear();
                BonusesController.CurrentBonuses.Clear();
                ScoreController.Score = 0;
                // Сохранить счёт игрока
            }
        }
    }
}