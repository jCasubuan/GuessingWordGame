using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame_BusinessLogic
{
    public class GuessingGameProcess
    {
        private static string[] wordsToGuess = { "programming", "debugging", "algorithm", "motherboard", "networking" };
        private static int totalLevel = 5;
        private static int points = 0;

        public static string[] GetWords()
        {
            return wordsToGuess;
        }

        public static int GetTotalLevel()
        {
            return totalLevel;
        }

        public static int GetPoints()
        {
            return points;
        }

        public static void UpdatePoints(int newPoints)
        {
            points += newPoints; 
        }

        public static bool UpdateGuessedWordWithLetter(string wordToGuess, char[] guessedWord, char guessedLetter)
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

        public static char[] InitializeGuessedWord(string wordToGuess)
        {
            char[] guessedWord = new char[wordToGuess.Length];

            for (int i = 0; i < guessedWord.Length; i++)
            {
                guessedWord[i] = '_';
            }

            return guessedWord;
        }

        public static bool IsWordGuessed(char[] guessedWord, string wordToGuess)
        {
            return new string(guessedWord) == wordToGuess;
        }

        public static bool ProcessPlayerGuess(string wordToGuess, char[] guessedWord, char guessedLetter, ref int playerLives)
        {
            bool correctGuess = UpdateGuessedWordWithLetter(wordToGuess, guessedWord, guessedLetter);

            if (!correctGuess)
            {
                playerLives--;   
            }

            return correctGuess;
        }




    }
}
