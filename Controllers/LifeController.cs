using EndlessFight.GameStates;
using EndlessFight.Models;

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
            var player = Globals.Player;

            if (player.CurrentLifes > 0)
            {
                player.CurrentLifes--;
                Globals.HitPulsation.Pulse();
            }

            if (player.CurrentLifes <= 0)
            {
                AudioController.mainTheme.soundEffectInstance.Stop();
                AudioController.PlayEffect(AudioController.loose);
                GameState.IsGameOver = true;
            }
        }
    }
}