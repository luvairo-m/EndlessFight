using System.Text.Json;
using System.IO;

namespace EndlessFight.Controllers
{
    public static class SerializationController
    {
        public static void MakeSerialization()
        {
            if (!File.Exists(Globals.serializationPath))
                File.WriteAllText(Globals.serializationPath, 
                    JsonSerializer.Serialize(new SerializationOptions
                    (ScoreController.Score, ScoreController.Score)));
            else
            {
                SerializationOptions serialization = JsonSerializer
                    .Deserialize<SerializationOptions>(File.ReadAllText(Globals.serializationPath));

                if (ScoreController.Score > serialization.BestScore)
                    serialization.BestScore = ScoreController.Score;

                serialization.AllScore += ScoreController.Score;
                File.WriteAllText(Globals.serializationPath, JsonSerializer.Serialize(serialization));
            }
        }
    }
}