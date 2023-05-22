using Microsoft.Xna.Framework;

namespace EndlessFight.Models
{
    public class Mult3Bonus : Bonus
    {
        public Mult3Bonus(Vector2 spawnPosition, TextureDescription bonusTexture)
            : base(spawnPosition, bonusTexture) { }

        public override void OnBonusApplying() => Globals.Player.ShootingMultiplier = 3;
    }
}