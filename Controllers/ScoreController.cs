using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessFight.Controllers
{
    public static class ScoreController
    {
        public static SpriteFont ScoreFont;
        public static int Score;

        public static void Draw(SpriteBatch spriteBatch)
        {
            var line = $"{new string('0', 6 - MeasureNumber(Score))}{Score}";
            var lineSize = ScoreFont.MeasureString(line);
            spriteBatch.DrawString(ScoreFont, line,
                new(Game1.windowWidth - 20 - lineSize.X, 20),
                SerializationController.BestScore >= Score ? Color.White : Color.Gold);
        }

        private static int MeasureNumber(int number)
        {
            if (number == 0) 
                return 1;

            var counter = 0;

            while (number > 0)
            {
                number /= 10;
                counter++;
            }

            return counter;
        }
    }
}