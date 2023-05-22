using Microsoft.Xna.Framework;

namespace EndlessFight.Models
{
    public class HeartBonus : Bonus
    {
        public HeartBonus(Vector2 spawnPosition, TextureDescription bonusTexture)
            : base(spawnPosition, bonusTexture) { }

        public override void OnBonusApplying()
        {
            var player = Globals.Player;
            if (player.CurrentLifes < 5 && player.CurrentLifes > 0)
                player.CurrentLifes++;
        }
    }
}