using GuessingGameCommon;
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
        private GameDataService gameDataService;
        private PlayerDataService playerDataService;
       
        public GuessingGameProcess()
        {
            gameDataService = new GameDataService();
            playerDataService = new PlayerDataService();
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

        public List<KeyValuePair<string, int>> GetLeaderboard()
        {
            return playerDataService.GetLeaderboard();
        }

        public int GetPlayerScore(string userName)
        {
            return playerDataService.GetPlayerScore(userName);
        }

        public bool VerifyLogin(string userName, string passWord)
        {
            return playerDataService.VerifyLogin(userName, passWord);
        }

        public bool PlayerExists (string userName)
        {
            return playerDataService.PlayerExists(userName);
        }

        public bool RegisterPlayer(string fullName, string userName, string password)
        {
            return playerDataService.RegisterPlayer(fullName, userName, password);
        }

        //public void UpdatePlayerScore(string userName, int newPoints)
        //{
        //    playerDataService.UpdatePlayerScore(userName, newPoints);
        //}

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

    }

}
