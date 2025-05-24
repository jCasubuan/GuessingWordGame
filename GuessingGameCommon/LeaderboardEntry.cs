using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameCommon
{
    public class LeaderboardEntry
    {
        public string UserName { get; set; }
        public int HighScore { get; set; }

        public LeaderboardEntry(string userName, int highScore)
        {
            UserName = userName;
            HighScore = highScore;
        }
    }
}
