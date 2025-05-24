using GuessingGameCommon;

namespace GuessingGameDataService
{
    public class TextFilePlayerDataService : IPlayerDataService
    {
        private string FilePath = "accounts.txt";
        private char Delimiter = '|';
        private int ExpectedFieldCount = 7;

        public TextFilePlayerDataService()
        {
            EnsureFileExists();
        }

        //private void GetDataFromFile()
        //{
        //    var players = File.ReadAllLines(filePath);
        //}

        private void EnsureFileExists()
        {
            if (!File.Exists(FilePath))
            {
                File.Create(FilePath).Close();
            }
        }

        private string FormatPlayerData(Player player)
        {
            return $"{player.FullName}{Delimiter}{player.UserName}{Delimiter}{player.Password}{Delimiter}" +
                   $"{player.PlayerId}{Delimiter}{player.Scores}{Delimiter}{player.HighScore}{Delimiter}{player.LastCompletedLevel}";
        }

        private Player ParsePlayerFromLine(string line)
        {
            var data = line.Split(Delimiter);

            if (data.Length != ExpectedFieldCount)
                return null;

            try
            {
                return new Player(data[0], data[1], data[2])
                {
                    PlayerId = int.Parse(data[3]),
                    Scores = int.Parse(data[4]),
                    HighScore = int.Parse(data[5]),
                    LastCompletedLevel = int.Parse(data[6])
                };
            }
            catch (FormatException)
            {
                return null;
            }
        }

        private void SaveAllPlayers(List<Player> players)
        {
            var formattedLines = new List<string>();

            foreach (var player in players)
            {
                formattedLines.Add(FormatPlayerData(player));
            }
            File.WriteAllLines(FilePath, formattedLines.ToArray());
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

        private void AddAccount(Player player)
        {
            var playerData = FormatPlayerData(player);
            File.AppendAllText(FilePath, playerData + Environment.NewLine);
        }

        private void UpdatePlayerInFile(Player updatedPlayer)
        {
            var players = GetAccounts();

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].UserName == updatedPlayer.UserName)
                {
                    players[i] = updatedPlayer;
                    break;
                }
            }
            SaveAllPlayers(players);
        }

        private List<Player> GetAccounts()
        {
            var players = new List<Player>();
            var lines = File.ReadAllLines(FilePath);

            if (lines.Length == 0)
                return players;

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                var player = ParsePlayerFromLine(line);
                if (player != null)
                    players.Add(player);
            }
            return players;
        }

        //CREATE
        public bool RegisterPlayer(Player player)
        {
            var existingPlayers = GetAccounts();
            foreach (var existingPlayer in existingPlayers)
            {
                if (existingPlayer.UserName == player.UserName)
                {
                    return false;
                }
            }
            player.PlayerId = GeneratePlayerId(existingPlayers);
            AddAccount(player);
            return true;
        }

        //READ
        public bool PlayerExists(string userName)
        {
            var players = GetAccounts();
            foreach (var player in players)
            {
                if (player.UserName == userName)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetPlayerScore(string userName)
        {
            var players = GetAccounts();
            foreach(var player in players)
            {
                if (player.UserName == userName)
                {
                    return player.Scores;
                }
            }
            return 0;
        }

        public int GetLastCompletedLevel(string userName)
        {
            var players = GetAccounts();
            foreach (var player in players)
            {
                if (player.UserName == userName)
                {
                    return player.LastCompletedLevel;
                }
            }
            return 0;
        } 

        public bool GetPlayerByCredentials(string userName, string password)
        {
            var players = GetAccounts();
            foreach (var player in players)
            {
                if (player.UserName == userName)
                {
                    return player.Password == password;
                }
            }
            return false;
        }

        public List<LeaderboardEntry> GetLeaderboard()
        {
            List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();
            var players = GetAccounts();

            foreach (var player in players)
            {
                if (player.HighScore > 0)
                {
                    leaderboard.Add(new LeaderboardEntry(player.UserName, player.HighScore));
                }
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
            var players = GetAccounts(); 
            foreach (var player in players)
            {
                if (player.UserName == userName)
                {
                    player.Scores += pointsToAdd;
                    if (player.Scores < 0)
                        player.Scores = 0;

                    if (player.Scores > player.HighScore)
                    {
                        player.HighScore = player.Scores;
                    }

                    UpdatePlayerInFile(player);
                    break;
                }
            }
        }

        public void UpdatePlayerLevelProgress(string userName, int lastCompletedLevel)
        {
            var players = GetAccounts(); 
            foreach (var player in players)
            {
                if (player.UserName == userName)
                {
                    if (lastCompletedLevel > player.LastCompletedLevel)
                    {
                        player.LastCompletedLevel = lastCompletedLevel;
                        UpdatePlayerInFile(player);
                    }
                    break;
                }
            }
        }

        public void ResetPlayerScore(string userName)
        {
            var players = GetAccounts();
            foreach (var player in players)
            {
                if (player.UserName == userName)
                {
                    player.Scores = 0;
                    UpdatePlayerInFile(player);
                    break;
                }
            }
        }

        

        
        
        





        // for admin
        public bool DeleteAccounts(string userName)
        {
            var players = GetAccounts();
            bool found = false;

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].UserName == userName)
                {
                    players.RemoveAt(i);
                    found = true;
                    break;
                }
            }

            if (found)
            {
                SaveAllPlayers(players);
                return true;
            }
            return false;
        }



    }
}
