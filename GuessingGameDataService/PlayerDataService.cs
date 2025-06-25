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
        IPlayerDataService playerDataService;

        public PlayerDataService()
        {
            //playerDataService = new InMemoryPlayerDataService();
            //playerDataService = new TextFilePlayerDataService();
            //playerDataService = new JsonFilePlayerDataService();
            playerDataService = new DBPlayerDataService();
        }

        //--- CREATE ---
        public bool RegisterPlayer(Player player)
        {
            return playerDataService.RegisterPlayer(player);
        }

        //--- READ ---
        public bool PlayerExists(string userName)
        {
            return playerDataService.PlayerExists(userName);
        }

        public int GetPlayerScore(string userName)
        {
            return playerDataService.GetPlayerScore(userName);
        }

        public int GetLastCompletedLevel(string userName)
        {
            return playerDataService.GetLastCompletedLevel(userName);
        }

        public bool GetPlayerByCredentials(string userName, string password)
        {
            return playerDataService.GetPlayerByCredentials(userName, password);
        }

        public List<LeaderboardEntry> GetLeaderboard()
        {
            return playerDataService.GetLeaderboard();
        }

        //--- UPDATE ---
        public void AddToPlayerScore(string userName, int pointsToAdd)
        {
            playerDataService.AddToPlayerScore(userName, pointsToAdd);
        }

        public void UpdatePlayerLevelProgress(string userName, int lastCompletedLevel)
        {
            playerDataService.UpdatePlayerLevelProgress(userName, lastCompletedLevel);
        }

        public void ResetPlayerScore(string userName)
        {
            playerDataService.ResetPlayerScore(userName);
        }

        public void ResetPlayerProgress(string userName)
        {
            playerDataService.ResetPlayerProgress(userName);
        }



    }
}
