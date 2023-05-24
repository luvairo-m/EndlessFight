using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessFight
{
    public class SerializationOptions
    {
        public int BestScore { get; set; }

        public SerializationOptions(int bestScore)
        {
            BestScore = bestScore;
        }
    }
}
