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
        public static Dictionary<string, Player> players = new Dictionary<string, Player>();

        private void SortLeaderboardByScore(List<LeaderboardEntry> leaderboard)
        {
            for (int outerIndex = 0; outerIndex < leaderboard.Count; outerIndex++)
            {
                for (int currentIndex = 0; currentIndex < leaderboard.Count - outerIndex - 1; currentIndex++)
                {
                    int nextIndex = currentIndex + 1;
                    if (leaderboard[currentIndex].HighScore < leaderboard[nextIndex].HighScore)
                    {
                        var temp = leaderboard[currentIndex];
                        leaderboard[currentIndex] = leaderboard[nextIndex];
                        leaderboard[nextIndex] = temp;
                    }
                }
            }
        }

        private List<LeaderboardEntry> SortPlayerRank(List<LeaderboardEntry> leaderboard)
        {
            if (leaderboard.Count == 0) return leaderboard;
            leaderboard[0].Rank = 1;

            for (int playerIndex = 1; playerIndex < leaderboard.Count; playerIndex++)
            {
                int previousPlayerIndex = playerIndex - 1;

                if (leaderboard[playerIndex].HighScore == leaderboard[previousPlayerIndex].HighScore)
                {
                    leaderboard[playerIndex].Rank = leaderboard[previousPlayerIndex].Rank;
                }
                else
                {
                    leaderboard[playerIndex].Rank = playerIndex + 1;
                }
            }
            return leaderboard;
        }

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

            SortLeaderboardByScore(leaderboard);
            return SortPlayerRank(leaderboard);
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

        public void ResetPlayerScore(string userName)
        {
            if (players.ContainsKey(userName))
            {
                players[userName].Scores = 0;
            }
        }

        public void ResetPlayerProgress(string userName)
        {
            if (players.ContainsKey(userName))
            {
                players[userName].LastCompletedLevel = 0;
                players[userName].Scores = 0;
            }
        }
    }
}
