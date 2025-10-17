using GuessingGame_BusinessLogic;
using GuessingGameCommon;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuessingGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuessingWordGameController : ControllerBase
    {   
        GuessingGameProcess gameProcess = new GuessingGameProcess();
        //GET, PUT, POST, PATCH, DELETE


        [HttpPost("RegisterPlayer")]
        public bool RegisterPlayer(Player newPlayer)
        {
            if (newPlayer == null ||
                string.IsNullOrWhiteSpace(newPlayer.FullName) || newPlayer.FullName.ToLower() == "string" ||
                string.IsNullOrWhiteSpace(newPlayer.UserName) || newPlayer.UserName.ToLower() == "string" ||
                string.IsNullOrWhiteSpace(newPlayer.Password) || newPlayer.Password.ToLower() == "string") 
            {
                return false;
            }

            bool success = gameProcess.RegisterPlayer(newPlayer.FullName, newPlayer.UserName, newPlayer.Password);
            return success;
        }

        [HttpGet("PlayerExists")]
        public bool PlayerExists(string userName)
        {
            return gameProcess.PlayerExists(userName);
        }

        [HttpGet("PlayerScore")]
        public int GetPlayerScore(string userName)
        {
            return gameProcess.GetPlayerScore(userName);
        }

        [HttpGet("LastCompletedLevel/{userName}")]
        public int GetLastCompletedLevel(string userName)
        {
            return gameProcess.GetLastCompletedLevel(userName);
        }

        [HttpGet("VerifyLogin/{userName}/{password}")]
        public bool GetPlayerAccount(string userName, string password)
        {
            return gameProcess.VerifyLogin(userName, password);
        }

        [HttpGet("Leaderboards")]
        public List<LeaderboardEntry> GetLeaderboardEntries()
        {
            return gameProcess.GetLeaderboard();
        }

        [HttpPatch("players/{username}/score")] 
        public bool AddToPlayerScore(string userName, int points)
        {
            if (string.IsNullOrWhiteSpace(userName) || userName.ToLower() == "string")
            {
                return false;
            }

            if (points == 0)
            {
                return false;
            }

            gameProcess.AddToPlayerScore(userName, points);

            return true;
        }

        [HttpPatch("players/{username}/level")]
        public bool UpdatePlayerLevel(string userName, int level)
        {
            if (string.IsNullOrWhiteSpace(userName) || userName.ToLower() == "string")
            {
                return false;
            } 

            if ( level < 0)
            {
                return false;
            }

            gameProcess.UpdatePlayerLevelProgress(userName, level);
            return true;
        }

        [HttpPatch("players/{username}/reset-score")]
        public bool ResetPlayerScore(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName) || userName.ToLower() == "string")
            {
                return false; 
            }

            gameProcess.ResetPlayerProgress(userName);
            return true;
        }

        [HttpPatch("players/{userName}/reset-progress")]
        public bool ResetPlayerProgress(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName) || userName.ToLower() == "string")
            {
                return false;
            }

            gameProcess.ResetPlayerProgress(userName);
            return true;
        }

        [HttpGet("WordHints")]
        public IEnumerable<WordHint> GetWordHints()
        {
            var wordHints = gameProcess.GetWordHints();
            return wordHints;
        }

        [HttpGet("SearchWord/{word}")]
        public WordHint SearchForWord(string word)
        {
            return gameProcess.SearchForWord(word);
        }

        [HttpPost("NewWord")]
        public bool AddWordHint(WordHint newWordHint) //[FromBody]
        {
            return gameProcess.AddWordHint(newWordHint.Word, newWordHint.Hint, newWordHint.Difficulty);
        }

        [HttpPatch("UpdateWord/{oldWord}")] 
        public bool UpdateWord(string oldWord, WordUpdateRequest updateRequest) // [FromBody]
        {            
            return gameProcess.UpdateWord(oldWord, updateRequest);
        }   

        [HttpDelete("DeleteWord")]
        public bool DeleteWord(string word)
        {
            return gameProcess.DeleteWord(word);
        }

        [HttpGet("AdminAccount")]
        public bool GetAdminAccount(string userName, string passwWord)
        {   
            AdminAccount adminAccount = new AdminAccount { Username = userName, Password = passwWord }; 
            return gameProcess.GetAdminAccount(adminAccount);
        }

        [HttpGet("GetPlayers")]
        public List<Player> GetAllPlayers()
        {
            return gameProcess.GetAllPlayers();
        }

        [HttpGet("SearchPlayer")]
        public Player SearchPlayerByInput(string adminInput)
        {
            return gameProcess.SearchPlayerByInput(adminInput);
        }

        [HttpDelete("DeletePlayer")]
        public bool DeletePlayer(string userName)
        {
            return gameProcess.DeletePlayer(userName);
        }

    }
}
