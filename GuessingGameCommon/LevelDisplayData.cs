using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameCommon
{
    public class LevelDisplayData
    {
        public int PlayerLives { get; set; }
        public List<char> GuessedWord { get; set; }
        public string UserName { get; set; }
        public string Hint { get; set; }
        public string Difficulty { get; set; }
        public int CurrentLevel { get; set; }
        public int PlayerScore { get; set; }
        public string Category { get; } = "IT related terminologies";
    }
}
