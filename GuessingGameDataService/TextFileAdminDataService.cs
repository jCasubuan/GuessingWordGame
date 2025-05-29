using GuessingGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameDataService
{
    public class TextFileAdminDataService : IAdminDataService
    {
        private string adminFilePath = "adminAccount.txt";
        private string accountsFilePath = "accounts.txt"; // <- text file path ng player accounts
        private char Delimiter = '|';
        private int ExpectedFieldCount = 7;

        public TextFileAdminDataService() 
        {
            EnsureFileExists();
        }

        private void EnsureFileExists()
        {
            bool adminFileNeedsInitialization = false;

            if (!File.Exists(adminFilePath))
            {
                adminFileNeedsInitialization = true;
            }
            else
            {
                try
                {
                    string content = File.ReadAllText(adminFilePath);
                    if (string.IsNullOrWhiteSpace(content) || !content.Contains(Delimiter))
                    {
                        adminFileNeedsInitialization = true;
                    }
                }
                catch (Exception ex)
                {
                    adminFileNeedsInitialization = true;
                }
            }

            if (adminFileNeedsInitialization)
            {
                File.WriteAllText(adminFilePath, "Admin_Jayboy" + Delimiter + "password");
            }

            if (!File.Exists(accountsFilePath))
            {
                File.Create(accountsFilePath).Close();
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

        private List<Player> GetAccounts()
        {
            var players = new List<Player>();
            var lines = File.ReadAllLines(accountsFilePath);

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

        private void SaveAllPlayerAccounts(List<Player> players)
        {
            var formattedLines = new List<string>();
            foreach (var player in players)
            {
                formattedLines.Add(FormatPlayerData(player));
            }
            File.WriteAllLines(accountsFilePath, formattedLines);
        }


        //--- READ ---
        public bool GetAdminAccount(AdminAccount adminAccount)
        {
            string adminFileContent = File.ReadAllText(adminFilePath);
            var parts = adminFileContent.Split(Delimiter); // Delimiter is '|'

            if (parts.Length == 2)
            {
                return parts[0].Trim().Equals(adminAccount.Username, StringComparison.Ordinal) && 
                       parts[1].Trim().Equals(adminAccount.Password, StringComparison.Ordinal);
            }
            return false;
        }
       
        public List<Player> GetAllPlayers()
        {
            List<Player> players = new List<Player>();
            try
            {
                string[] lines = File.ReadAllLines(accountsFilePath);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                            continue;

                    Player player = ParsePlayerFromLine(line);
                    if (player != null)
                    {
                        players.Add(player);
                    }
                }
            }
            catch (Exception ex)
            {          
            }
            return players;
        }

        public Player SearchById(int playerId)
        {
            var players = GetAccounts();
            foreach(var player in players)
            {
                if (player.PlayerId == playerId)
                {
                    return player;
                }
            }
            return null;

        }

        public Player SearchByUsername(string userName)
        {
            var players = GetAccounts();
            foreach(var player in players)
            {
                if(player.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase))
                {
                    return player;
                }
            }
            return null;
        }

        //--- DELETE ---
        public bool DeletePlayer(string userName)
        {   
            if (string.IsNullOrWhiteSpace(userName))
            {
                return false;
            }

            var players = GetAccounts();   
            bool playerFoundAndRemoved = false;
            
            for (int i = players.Count - 1; i >= 0; i--)
            {
                if (players[i].UserName.Equals(userName, StringComparison.OrdinalIgnoreCase))
                {
                    players.RemoveAt(i);
                    playerFoundAndRemoved = true;
                }
            }
            if (playerFoundAndRemoved)
            {
                SaveAllPlayerAccounts(players);
                return true;
            }

            else 
            { 
                return false; 
            }
        }

    }
}
