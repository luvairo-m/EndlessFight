using EndlessFight.Controllers;
using Microsoft.Xna.Framework;

namespace EndlessFight.Models
{
    public class BombBonus : Bonus
    {
        public BombBonus(Vector2 spawnPosition, TextureDescription bonusTexture)
            : base(spawnPosition, bonusTexture) { }

        public override void OnBonusApplying()
        {
            AudioController.PlayEffect(AudioController.allDestroy);
            EnemiesController.CurrentEnemies.ForEach(enemy => enemy.IsAlive = false);
            for (var i = 0; i <= Game1.windowHeight; i += 100)
                for (var j = 0; j <= Game1.windowWidth; j+= 100)
                    ExplosionContoller.CreateExplosion(new(j, i));
        }
    }
}