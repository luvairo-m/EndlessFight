using System.Text.Json;
using System.IO;

namespace EndlessFight.Controllers
{
    public static class SerializationController
    {
        public static int BestScore;

        public static void MakeSerialization()
        {
            if (!File.Exists(Globals.SerializationPath))
                File.WriteAllText(Globals.SerializationPath, 
                    JsonSerializer.Serialize(new SerializationOptions
                    (ScoreController.Score)));
            else
            {
                SerializationOptions serialization = JsonSerializer
                    .Deserialize<SerializationOptions>(File.ReadAllText(Globals.SerializationPath));

                if (ScoreController.Score > serialization.BestScore)
                    serialization.BestScore = ScoreController.Score;

                File.WriteAllText(Globals.SerializationPath, JsonSerializer.Serialize(serialization));
            }
        }

        public static int GetBestScore()
        {
            if (!File.Exists(Globals.SerializationPath))
            {
                return 0;
            }
            else
            {
                SerializationOptions serialization = JsonSerializer
                    .Deserialize<SerializationOptions>(File.ReadAllText(Globals.SerializationPath));
                BestScore = serialization.BestScore;    
                return serialization.BestScore;
            }

        }
    }
}