using GuessingGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameDataService
{
    public class PlayerDataService
    {
        private Dictionary<string, Player> players = new Dictionary<string, Player>();

        public bool RegisterPlayer(string fullName, string userName, string password)
        {
            if (players.ContainsKey(userName))
            {
                return false;
            }

            players[userName] = new Player(fullName, userName, password);
            //players.Add(userName, new Player(fullName, userName, password));
            return true;
        }

        public bool VerifyLogin(string userName, string password)
        {
            if (players.ContainsKey(userName))
            {
                return players[userName].Password == password;
            }
            return false;
        }

        public bool PlayerExists(string userName)
        {
            return players.ContainsKey(userName);
        }

        public int GetPlayerScore(string userName)
        {
            if (players.ContainsKey(userName))
            {
                return players[userName].Scores;
            }
            return 0;
        }

        //public void UpdatePlayerScore(string userName, int newPoints)
        //{
        //    if (players.ContainsKey(userName))
        //    {
        //        players[userName].Scores = newPoints; //Reset the score at the start of a new game session
        //    }
        //}

        public void AddToPlayerScore(string userName, int pointsToAdd)
        {
            if (players.ContainsKey(userName))
            {
                var player = players[userName];

                player.Scores += pointsToAdd;

                if (player.Scores < 0)
                    player.Scores = 0;
             
                if (player.Scores > player.HighScore)
                {
                    player.HighScore = player.Scores;
                }
            }
        }


        public List<KeyValuePair<string, int>> GetLeaderboard()
        {
            List<KeyValuePair<string, int>> leaderboard = new List<KeyValuePair<string, int>>();

            foreach (var player in players)
            {
                if (player.Value.HighScore > 0) // Only include players who have scored
                {
                    leaderboard.Add(new KeyValuePair<string, int>(player.Key, player.Value.HighScore));
                }
            }

            // Sort by score in descending order
            leaderboard.Sort((x, y) => y.Value.CompareTo(x.Value));

            return leaderboard;
        }

        public void UpdatePlayerLevelProgress(string userName, int lastCompletedLevel)
        {
            if (players.ContainsKey(userName))
            {
                if (lastCompletedLevel > players[userName].LastCompletedLevel)
                {
                    players[userName].LastCompletedLevel = lastCompletedLevel; // for loading saved game
                }
            }
        }

        public int GetLastCompletedLevel(string userName)
        {
            if (players.ContainsKey(userName))
                return players[userName].LastCompletedLevel;

            return 0;
        }



    }
}
