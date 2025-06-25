using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameCommon
{
    public class LeaderboardEntry
    {   
        public int Rank { get; set; }
        public string UserName { get; set; }
        public int HighScore { get; set; }
        public int PlayerId { get; set; }

        public LeaderboardEntry(string userName, int highScore)
        {
            UserName = userName;
            HighScore = highScore;
            Rank = 0;
        }

        public LeaderboardEntry(int playerId, string userName, int highScore)
        {
            PlayerId = playerId;
            UserName = userName;
            HighScore = highScore;
            Rank = 0;
        }
    }
}
