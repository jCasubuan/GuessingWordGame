using GuessingGameCommon;
using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace GuessingGameDataService
{
    public class JsonFilePlayerDataService : IPlayerDataService
    {   
        private static List<Player> players;
        //private string jsonFilePath = "accounts.json";
        private static string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "accounts.json");

        public JsonFilePlayerDataService()
        {
            players = new List<Player>();
            ReadJsonDataFromFile();
        }

        private void EnsureFileExists()
        {
            if (!File.Exists(jsonFilePath))
                File.WriteAllText(jsonFilePath, "[]");
        }

        private void ReadJsonDataFromFile()
        {
            EnsureFileExists();
            string jsonText = File.ReadAllText(jsonFilePath);

            if (string.IsNullOrEmpty(jsonText))
            {
                players = new List<Player>();
            }
            else
            {
                List<Player> deserializedPlayers = JsonSerializer.Deserialize<List<Player>>(jsonText,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (deserializedPlayers == null)
                {
                    players = new List<Player>();
                }
                else
                {
                    players = deserializedPlayers;
                }
            }
            
        }

        private void WriteJsonDataToFile()
        {
            string jsonString = JsonSerializer.Serialize(players, new JsonSerializerOptions
            { WriteIndented = true});

            File.WriteAllText(jsonFilePath, jsonString);    
        }

        private int GeneratePlayerId(List<Player> existingPlayers)
        {
            int maxId = 0;

            for (int i = 0; i < existingPlayers.Count; i++)
            {
                if (existingPlayers[i].PlayerId > maxId)
                {
                    maxId = existingPlayers[i].PlayerId;
                }
            }

            return maxId + 1;
        }

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

        private void AssignPlayerRanks(List<LeaderboardEntry> leaderboard)
        {
            if (leaderboard.Count == 0)
            {
                return;
            }

            leaderboard[0].Rank = 1; 

            for (int playerIndex = 1; playerIndex < leaderboard.Count; playerIndex++)
            {
                if (leaderboard[playerIndex].HighScore == leaderboard[playerIndex - 1].HighScore)
                {
                    leaderboard[playerIndex].Rank = leaderboard[playerIndex - 1].Rank;
                }
                else 
                {
                    leaderboard[playerIndex].Rank = playerIndex + 1;
                }
            }
        }

        //CREATE
        public bool RegisterPlayer(Player player)
        {
            foreach (var existingPlayer  in players)
            {
                if (existingPlayer.UserName.Equals(player.UserName, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            player.PlayerId = GeneratePlayerId(players);
            players.Add(player);
            WriteJsonDataToFile();
            return true;
        }

        //READ
        public bool PlayerExists(string userName)
        {
            for (int i = 0;i < players.Count;i++)
            {
                if (string.Equals(players[i].UserName, userName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public int GetPlayerScore(string userName)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (string.Equals(players[i].UserName, userName, StringComparison.OrdinalIgnoreCase))
                {
                    return players[i].Scores;
                }
            }
            return 0;
        }

        public int GetLastCompletedLevel(string userName)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (string.Equals(players[i].UserName, userName, StringComparison.OrdinalIgnoreCase))
                {
                    return players[i].LastCompletedLevel; 
                }
            }
            return 0; 
        }

        public bool GetPlayerByCredentials(string userName, string password)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (string.Equals(players[i].UserName, userName, StringComparison.OrdinalIgnoreCase))
                {
                    return players[i].Password == password;
                }
            }
            return false; 
        }

        public List<LeaderboardEntry> GetLeaderboard()
        {
            List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

            foreach (var player in players)
            {
                if (player.HighScore > 0)
                {
                    leaderboard.Add(new LeaderboardEntry(player.UserName, player.HighScore));
                }
            }

            SortLeaderboardByScore(leaderboard);
            AssignPlayerRanks(leaderboard);

            return leaderboard;
        }

        //UPDATE
        public void AddToPlayerScore(string userName, int pointsToAdd)
        {
            foreach (var player in players)
            {
                if (player.UserName == userName)
                {
                    player.Scores += pointsToAdd;
                    if (player.Scores < 0)
                    {
                        player.Scores = 0;
                    }

                    if (player.Scores > player.HighScore)
                    {
                        player.HighScore = player.Scores;
                    }
                    WriteJsonDataToFile();
                    break;
                }

            }
        }

        public void UpdatePlayerLevelProgress(string userName, int lastCompletedLevel)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (string.Equals(players[i].UserName, userName, StringComparison.OrdinalIgnoreCase))
                {
                    if (lastCompletedLevel > players[i].LastCompletedLevel)
                    {
                        players[i].LastCompletedLevel = lastCompletedLevel;
                        WriteJsonDataToFile(); 
                    }
                    break; 
                }
            }
        }

        public void ResetPlayerScore(string userName)
        {
            foreach (var player in players)
            {
                if (player.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase))
                {
                    player.Scores = 0;
                    WriteJsonDataToFile();
                    break;
                }
            }
        }

        public void ResetPlayerProgress(string userName)
        {
            foreach (var player in players)
            {
                if (player.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase))
                {
                    player.LastCompletedLevel = 0;
                    player.Scores = 0;
                    WriteJsonDataToFile();
                    break;
                }
            }    
        }
    }
}
