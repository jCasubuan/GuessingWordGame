﻿using GuessingGameCommon;
using GuessingGameDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame_BusinessLogic
{
    public class GuessingGameProcess
    {
        private int totalLevel = 50;
        private int playerLives = 7;
        private int wrongGuessesInLevel = 0;
        //private int resetScore = 0;


        private GameDataService gameDataService;
        private PlayerDataService playerDataService;
        private AdminDataService adminDataService;

        public GuessingGameProcess()
        {
            gameDataService = new GameDataService();
            playerDataService = new PlayerDataService();
            adminDataService = new AdminDataService();
        }

        public int TotalLevel { get { return totalLevel; } }

        public int PlayerLives { get { return playerLives; } }

        public void ResetLives()
        {
            playerLives = 7;
        }

        public void DecreaseLives()
        {
            playerLives--;
        }

        // Game mechanics methods
        public bool UpdateGuessedWordWithLetter(string wordToGuess, List<char> guessedWord, char guessedLetter)
        {
            bool correctGuess = false;
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (wordToGuess[i] == guessedLetter)
                {
                    guessedWord[i] = guessedLetter;
                    correctGuess = true;
                }
            }
            return correctGuess;
        }

        public List<char> InitializeGuessedWord(string wordToGuess)
        {
            List<char> guessedWord = new List<char>();
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (wordToGuess[i] == ' ')
                {
                    guessedWord.Add(' ');
                }
                else
                {
                    guessedWord.Add('_');
                }

            }
            return guessedWord;
        }

        public bool IsWordGuessed(List<char> guessedWord, string wordToGuess)
        {
            return new string(guessedWord.ToArray()) == wordToGuess;
        }

        public bool ProcessPlayerGuess(string wordToGuess, List<char> guessedWord, char guessedLetter)
        {
            bool correctGuess = UpdateGuessedWordWithLetter(wordToGuess, guessedWord, guessedLetter);
            if (!correctGuess)
            {
                DecreaseLives();
            }
            return correctGuess;
        }

        // logic for scoring
        public int CalculateScoreForLevel()
        {
            int points = 10 - wrongGuessesInLevel;

            if (points < 0)
            {
                return 0;
            }
            else
            {
                return points;
            }

        }

        public void ResetWrongGuesses()
        {
            wrongGuessesInLevel = 0;
        }

        public void ResetPlayerScore(string userName)
        {
            playerDataService.ResetPlayerScore(userName);
        }

        public List<WordHint> GetWordHints()
        {
            return gameDataService.GetAllWordHints().ToList();
        }

        //logic for tracking wrong guesses
        public void IncrementWrongGuesses()
        {
            wrongGuessesInLevel++;
        }

        public int GetWrongGuessesInLevel()
        {
            return wrongGuessesInLevel;
        }

        public List<LeaderboardEntry> GetLeaderboard()
        {
            return playerDataService.GetLeaderboard();
        }

        public int GetPlayerScore(string userName)
        {
            return playerDataService.GetPlayerScore(userName);
        }

        public bool VerifyLogin(string userName, string passWord)
        {
            return playerDataService.GetPlayerByCredentials(userName, passWord);
        }

        public bool PlayerExists (string userName)
        {
            return playerDataService.PlayerExists(userName);
        }

        public bool RegisterPlayer(string fullName, string userName, string password)
        {
            Player newPlayer = new Player(fullName, userName, password);
            return playerDataService.RegisterPlayer(newPlayer);
        }

        public void AddToPlayerScore(string userName, int pointsToAdd)
        {
            playerDataService.AddToPlayerScore(userName, pointsToAdd);
        }

        public void UpdatePlayerLevelProgress(string userName, int lastCompletedLevel)
        {
            playerDataService.UpdatePlayerLevelProgress(userName, lastCompletedLevel);
        }

        public int GetLastCompletedLevel(string userName)
        {
            return playerDataService.GetLastCompletedLevel(userName);
        }

        //Admin login
        public bool ValidateAdminLogin(string username, string password)
        {
            return adminDataService.ValidateAdminLogin(username, password);
        }

        // topic: Player data, for admin data processsing
        public List<Player> GetAllPlayers()
        {
            return adminDataService.GetAllPlayers();
        }

        public Player SearchPlayerByInput(string adminInput)
        {           
           if (int.TryParse(adminInput, out int playerId))
           {
                return adminDataService.SearchById(playerId);
           }
            else
            {   
                return adminDataService.SearchByUsername(adminInput);
            }
        }

        public bool DeletePlayer(string userName)
        {            
            return adminDataService.DeletePlayer(userName);
        }

        public WordHint SearchForWord(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return null;
            }
            return gameDataService.SearchForWord(word);
        }

        public bool AddWordHint(string word, string hint, string diffulty)
        {
            return gameDataService.AddWordHint(word, hint, diffulty);
        }

        public bool UpdateWord(string oldWord, string newWord, string newHint, string newDifficulty)
        {
            if (string.IsNullOrWhiteSpace(oldWord) || string.IsNullOrWhiteSpace(newWord))
            {
                return false;
            }

            newWord = newWord.Trim();
            newHint = newHint.Trim() ?? string.Empty;
            newDifficulty = ValidateDifficulty(newDifficulty);

            return gameDataService.UpdateWord(oldWord, newWord, newHint, newDifficulty);
        }

        private string ValidateDifficulty(string difficulty)
        {
            string defaultDifficulty = difficulty.Trim().ToLower() ?? "medium";

            switch (defaultDifficulty)
            {
                case "easy":
                case "medium":
                case "hard":
                case "extra hard":
                    return defaultDifficulty;
                default:
                    return "medium";
            }
        }

        public bool DeleteWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return false;

            return gameDataService.DeleteWord(word);
        }




    }

}
