using EndlessFight.Controllers;
using Microsoft.Xna.Framework;

namespace EndlessFight.Models
{
    public class Mult3Bonus : Bonus
    {
        public Mult3Bonus(Vector2 spawnPosition, TextureDescription bonusTexture)
            : base(spawnPosition, bonusTexture) { }

        public override void OnBonusApplying()
        {
            AudioController.PlayEffect(AudioController.getItem);
            Globals.Player.ShootingMultiplier = 3;
        } 
    }
}