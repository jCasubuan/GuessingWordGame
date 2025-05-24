using GuessingGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GuessingGameDataService
{
    public class JsonFilePlayerDataService : IPlayerDataService
    {   
        private List<Player> players = new List<Player>();
        private string jsonFilePath = "accounts.json";

        public JsonFilePlayerDataService()
        {   

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

        //CREATE
        public bool RegisterPlayer(Player player)
        {
            ReadJsonDataFromFile();
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
            ReadJsonDataFromFile();
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
            ReadJsonDataFromFile();
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
            ReadJsonDataFromFile();
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
            ReadJsonDataFromFile();
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
            ReadJsonDataFromFile();

            foreach (var player in players)
            {
                leaderboard.Add(new LeaderboardEntry(player.UserName, player.HighScore));
            }

            for (int i = 0; i < leaderboard.Count; i++)
            {
                for (int j = 0; j < leaderboard.Count - i - 1; j++)
                {
                    if (leaderboard[j].HighScore < leaderboard[j + 1].HighScore)
                    {
                        var temp = leaderboard[j];
                        leaderboard[j] = leaderboard[j + 1];
                        leaderboard[j + 1] = temp;
                    }
                }
            }
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
            ReadJsonDataFromFile();
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


    }
}
