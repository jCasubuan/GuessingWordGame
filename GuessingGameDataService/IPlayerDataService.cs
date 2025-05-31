using GuessingGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameDataService
{
    public interface IPlayerDataService
    {   
        // CREATE
        bool RegisterPlayer(Player player);

        // READ
        bool PlayerExists(string userName);
        int GetPlayerScore(string userName);
        int GetLastCompletedLevel(string userName);
        bool GetPlayerByCredentials(string userName, string password);
        List<LeaderboardEntry> GetLeaderboard();

        // UPDATE
        void AddToPlayerScore(string userName, int pointsToAdd);
        void UpdatePlayerLevelProgress(string usernName, int lastCompletedLevel);
        void ResetPlayerScore(string userName);
        void ResetPlayerProgress(string userName);
    }
}
