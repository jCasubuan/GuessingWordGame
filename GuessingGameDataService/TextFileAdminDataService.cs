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
        private string accountsFilePath = "accounts.txt"; // <- textfile ng player accounts
        private char Delimiter = '|';
        private int ExpectedFieldCount = 7;

        public TextFileAdminDataService() 
        {
            EnsureFileExists();
        }

        private void EnsureFileExists()
        {
            if (!File.Exists(adminFilePath))
            {
                File.WriteAllText(adminFilePath, "Admin_Jayboy, password");
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


        //--- READ ---
        public bool ValidateAdminLogin(string username, string password)
        {
            string adminFile = File.ReadAllText(adminFilePath);
            var parts = adminFile.Split(Delimiter);

            if (parts.Length == 2)
            {
                return parts[0].Trim() == username && parts[1].Trim() == password;
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
            throw new NotImplementedException();
        }

        public Player SearchByUsername(string userName)
        {
            throw new NotImplementedException();
        }

        //--- DELETE ---
        public bool DeletePlayer(string userName)
        {
            throw new NotImplementedException();
        }

    }
}
