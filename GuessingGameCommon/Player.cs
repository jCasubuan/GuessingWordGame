using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameCommon
{
    public class Player
    {
        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int PlayerId { get; set; }

        public int Scores { get; set; }

        public int HighScore { get; set; }

        public int LastCompletedLevel { get; set; } = 0;

        public Player() { }

        public Player(string fullName, string userName, string password)
        {
            FullName = fullName;
            UserName = userName;
            Password = password;
            Scores = 0;
            HighScore = 0;
        }

    }

}
