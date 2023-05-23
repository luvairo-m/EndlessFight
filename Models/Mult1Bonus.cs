using EndlessFight.Controllers;
using Microsoft.Xna.Framework;

namespace EndlessFight.Models
{
    public class Mult1Bonus : Bonus
    {
        public Mult1Bonus(Vector2 spawnPosition, TextureDescription bonusTexture)
            : base(spawnPosition, bonusTexture) { }

        public override void OnBonusApplying()
        {
            AudioController.PlayEffect(AudioController.getItem);
            Globals.Player.ShootingMultiplier = 1;
        }
    }
}