using EndlessFight.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace EndlessFight.Controllers
{
    public static class BonusesController
    {
        public static Dictionary<Type, TextureDescription> BonusesTextures;
        public static List<Bonus> CurrentBonuses = new();

        public static void Update()
        {
            CurrentBonuses.ForEach(bonus => bonus.Update());
            for (var i = 0; i < CurrentBonuses.Count; i++)
            {
                var bonus = CurrentBonuses[i];
                if (bonus.Position.Y > Game1.windowHeight + bonus.HitBox.Height)
                    CurrentBonuses.RemoveAt(i);
            }
            DeleteDeadOnes();
        }

        public static void Draw(SpriteBatch spriteBatch)
            => CurrentBonuses.ForEach(bonus => bonus.Draw(spriteBatch));

        public static void CreateBonus(Vector2 spawnPosition)
        {
            var bonusType = PickRandomBonusType();
            if (bonusType != null)
            {
                var bonus = (Bonus)Activator.CreateInstance(bonusType, spawnPosition, BonusesTextures[bonusType]);
                var position = new Vector2(
                    spawnPosition.X - bonus.HitBox.Width / 2,
                    spawnPosition.Y - bonus.HitBox.Height / 2);
                bonus.Position = position;
                CurrentBonuses.Add(bonus);
            }
        }

        private static Type PickRandomBonusType()
        {
            var number = Globals.Randomizer.Next(200 + 1);
            if (number > 160 && number <= 170) return typeof(Mult2Bonus);
            else if (number > 170 && number <= 180) return typeof(Mult3Bonus);
            else if (number > 180 && number <= 190) return typeof(BombBonus);
            else if (number > 190 && number <= 200) return typeof(HeartBonus);
            else return null;
        }

        private static void DeleteDeadOnes()
        {
            for (var i = 0; i < CurrentBonuses.Count; i++)
                if (!CurrentBonuses[i].IsAlive)
                {
                    CurrentBonuses[i].OnBonusApplying();
                    CurrentBonuses.RemoveAt(i);
                }
        }
    }
}