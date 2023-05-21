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
                        (bullet.IsAlive, enemy.IsAlive) = (false, false);
                }

            foreach (var bullet in bullets)
                if (bullet.HitBox.Intersects(Globals.Player.HitBox) && bullet.Owner != BulletOwner.Player)
                {
                    if (Player.CurrentLifes > 0)
                        Player.CurrentLifes--;

                    Globals.HitModel.SetPulsing();
                    bullet.IsAlive = false;
                }
        }
    }
}