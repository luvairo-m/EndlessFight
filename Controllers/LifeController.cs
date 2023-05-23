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

            foreach (var bullet in bullets)
                if (bullet.HitBox.Intersects(Globals.Player.HitBox) && bullet.Owner != BulletOwner.Player)
                {
                    AudioController.PlayEffect(AudioController.getDamage);

                    if (Globals.Player.CurrentLifes > 0)
                        Globals.Player.CurrentLifes--;

                    Globals.HitPulsation.Pulse();
                    bullet.IsAlive = false;
                }

            foreach (var bonus in BonusesController.CurrentBonuses)
                if (bonus.HitBox.Intersects(Globals.Player.HitBox))
                {
                    bonus.IsAlive = false;
                }
        }
    }
}