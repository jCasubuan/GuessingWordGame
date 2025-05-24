using GuessingGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameDataService
{
    public class InMemoryPlayerDataService : IPlayerDataService
    {
        private int playerIdCounter = 1;
        private Dictionary<string, Player> players = new Dictionary<string, Player>();

        //CREATE
        public bool RegisterPlayer(Player player)
        {
            if (players.ContainsKey(player.UserName))
            {
                return false;
            }
            player.PlayerId = playerIdCounter++;
            players[player.UserName] = player;
            return true;
        }

        //READ
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

        public int GetLastCompletedLevel(string userName)
        {
            if (players.ContainsKey(userName))
                return players[userName].LastCompletedLevel;

            return 0;
        }

        public bool GetPlayerByCredentials(string userName, string password)
        {
            if (players.ContainsKey(userName))
            {
                return players[userName].Password == password;
            }
            return false;
        }

        public List<LeaderboardEntry> GetLeaderboard()
        {
            List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

            foreach (var player in players)
            {
                if (player.Value.HighScore > 0)
                {
                    leaderboard.Add(new LeaderboardEntry(player.Key, player.Value.HighScore));
                }
            }

            // Sort by score in descending order
            leaderboard.Sort((x, y) => y.HighScore.CompareTo(x.HighScore));
            return leaderboard;
        }

        //UPDATE
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

        

        

        

        
        
    }
}
