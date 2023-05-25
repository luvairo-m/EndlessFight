using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using static EndlessFight.Resources;

namespace EndlessFight.Controllers
{
    public static class ScoreController
    {
        public static int Score;
        public static int BestScore = SerializationController.GetBestScore();

        public static void Draw(SpriteBatch spriteBatch)
        {
            var line = $"{new string('0', 6 - MeasureNumber(Score))}{Score}";
            var lineSize = InGameScoreFont.MeasureString(line);
            var bestScoreLine = $"{new string('0', 6 - MeasureNumber(BestScore))}{BestScore}";
            var bestScoreLineSize = BestScoreFont.MeasureString(bestScoreLine);

            if (BestScore <= Score)
                bestScoreLine = line;

            spriteBatch.DrawString(InGameScoreFont, line,
                new(Game1.windowWidth - 20 - lineSize.X, 20),
                SerializationController.BestScore >= Score ? Color.White : Color.Gold);

            spriteBatch.DrawString(BestScoreFont, bestScoreLine,
                new(Game1.windowWidth - 25 - bestScoreLineSize.X, 80),
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