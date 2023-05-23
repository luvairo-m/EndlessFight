using System.Text.Json;
using System.IO;

namespace EndlessFight.Controllers
{
    public static class SerializationController
    {
        public static void MakeSerialization()
        {
            if (!File.Exists(Globals.SerializationPath))
                File.WriteAllText(Globals.SerializationPath, 
                    JsonSerializer.Serialize(new SerializationOptions
                    (ScoreController.Score, ScoreController.Score)));
            else
            {
                SerializationOptions serialization = JsonSerializer
                    .Deserialize<SerializationOptions>(File.ReadAllText(Globals.SerializationPath));

                if (ScoreController.Score > serialization.BestScore)
                    serialization.BestScore = ScoreController.Score;

                serialization.AllScore += ScoreController.Score;
                File.WriteAllText(Globals.SerializationPath, JsonSerializer.Serialize(serialization));
            }
        }

        public static int GetBestScore()
        {
            SerializationOptions serialization = JsonSerializer
                    .Deserialize<SerializationOptions>(File.ReadAllText(Globals.SerializationPath));

            return serialization.BestScore;
        }
    }
}