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
            File.WriteAllLines(FilePath, formattedLines);
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

            SortLeaderboardByScore(leaderboard);
            AssignPlayerRanks(leaderboard);
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


    }
}
