using GuessingGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GuessingGameDataService
{
    public class JsonFileAdminDataService : IAdminDataService
    {
        private List<Player> players;
        private string adminJsonFilePath = "adminAccount.json";
        private string jsonFilePath = "accounts.json";

        public JsonFileAdminDataService()
        {   
            EnsureFileExists();
            players = new List<Player>();
            ReadJsonDataFromFile();
        }

        private void EnsureFileExists()
        {
            bool adminFileNeedsInitialization = false;

            if (!File.Exists(adminJsonFilePath))
            {
                adminFileNeedsInitialization = true;
            }
            else
            {
                string content = File.ReadAllText(adminJsonFilePath).Trim();

                if (string.IsNullOrEmpty(content) || content == "{}" || content == "null" || content == "[]")
                {
                    adminFileNeedsInitialization = true;
                }
                else
                {

                    AdminAccount testAdmin = JsonSerializer.Deserialize<AdminAccount>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (testAdmin == null)
                    {
                        adminFileNeedsInitialization = true;
                    }
                }
            }

            if (adminFileNeedsInitialization)
            {
                var defaultAdmin = new AdminAccount { Username = "Admin_Jayboy", Password = "password" };
                File.WriteAllText(adminJsonFilePath, JsonSerializer.Serialize(defaultAdmin, new JsonSerializerOptions { WriteIndented = true }));
            }

            if (!File.Exists(jsonFilePath))
            {
                File.WriteAllText(jsonFilePath, "[]"); 
            }
        }

        private void ReadJsonDataFromFile()
        {
            EnsureFileExists(); 
            string playerJsonText = File.ReadAllText(jsonFilePath);

            List<Player> deserializedPlayers = null;
            if (!string.IsNullOrEmpty(playerJsonText) && playerJsonText.Trim() != "[]" && 
                playerJsonText.Trim() != "null" && playerJsonText.Trim() != "{}")
            {
                deserializedPlayers = JsonSerializer.Deserialize<List<Player>>(playerJsonText,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            players.Clear();
            if (deserializedPlayers != null)
            {
                foreach (var player in deserializedPlayers)
                {
                    players.Add(player);
                }
            }
        }       

        //--- READ ---
        public bool GetAdminAccount(AdminAccount adminAccount)
        {   
            EnsureFileExists();
            string adminJsonText = File.ReadAllText(adminJsonFilePath);

            if (string.IsNullOrWhiteSpace(adminJsonText) || adminJsonText.Trim() == "{}")
            {
                return false;
            }

            AdminAccount admin = JsonSerializer.Deserialize<AdminAccount>(adminJsonText,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

           return admin != null &&
           admin.Username.Equals(adminAccount.Username, StringComparison.OrdinalIgnoreCase) &&
           admin.Password == adminAccount.Password;

        }

        public List<Player> GetAllPlayers()
        {
            EnsureFileExists();

            string playerJsonText = File.ReadAllText(jsonFilePath);
            List<Player> deserializedPlayers = new List<Player>();

            if (!string.IsNullOrWhiteSpace(playerJsonText))
            {
                deserializedPlayers = JsonSerializer.Deserialize<List<Player>>(playerJsonText,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
            }

            if (deserializedPlayers == null)
            {
                deserializedPlayers = new List<Player>();
            }
            return deserializedPlayers;
        }

        public Player SearchById(int playerId)
        {
            EnsureFileExists();

            string playerJsonText = File.ReadAllText(jsonFilePath);
            List<Player> deserializedPlayers = new List<Player>();

            if (!string.IsNullOrWhiteSpace (playerJsonText))
            {
                deserializedPlayers = JsonSerializer.Deserialize<List<Player>>(playerJsonText,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
            }

            foreach (Player player in deserializedPlayers)
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
            EnsureFileExists();

            string playerJsonText = File.ReadAllText(jsonFilePath);
            List<Player> deserializedPlayers = new List<Player>();

            if (!string.IsNullOrWhiteSpace(playerJsonText))
            {
                deserializedPlayers = JsonSerializer.Deserialize<List<Player>>(playerJsonText,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
            }

            foreach (Player player in deserializedPlayers )
            {
                if (player.UserName == userName)
                {
                    return player;
                }
            }
            return null;
        }
       
        //--- DELETE ---
        public bool DeletePlayer(string userName)
        {
            EnsureFileExists();

            string playerJsonText = File.ReadAllText(jsonFilePath);
            List<Player> deserializedPlayers = new List<Player>();

            if (!string.IsNullOrWhiteSpace(playerJsonText))
            {
                deserializedPlayers = JsonSerializer.Deserialize<List<Player>>(playerJsonText,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
            }

            bool playerFound = false;
            for (int i = 0; i < deserializedPlayers.Count; i++)
            {
                if (deserializedPlayers[i].UserName.Equals(userName, StringComparison.OrdinalIgnoreCase))
                {
                    deserializedPlayers.RemoveAt(i);
                    playerFound = true;
                    break; 
                }
            }

            if (playerFound)
            {
                string updatedJson = JsonSerializer.Serialize(deserializedPlayers, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonFilePath, updatedJson);
            }

            return playerFound;


        }
    }
}
