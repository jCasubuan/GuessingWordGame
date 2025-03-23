using System;

namespace GuessingWordGame
{
     public class Program
    {
       
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my C# version of Hangman. Hope you like it!"); // Header
            Console.WriteLine("_______________________________________________________" + Environment.NewLine);
            // for separator

            string userName = GetUserName("Before we start, please enter your preferred username: "); // method calling
            Console.WriteLine();

            string wordToGuess = "supercalifragilisticexpialidocious"; // pwede to mabago
            int playerLives = 3; // pwede rin to mabago 

            char[] guessedWord = InitializeGuessedWord(wordToGuess); // method calling

            while (playerLives > 0)
            {
                Console.WriteLine($"Your lives remaining: {playerLives}"); // prints the playerLives
                Console.WriteLine("Word to guess: " + new string(guessedWord)); // prints the wordToGuess as an " _ "
                Console.WriteLine();

                Console.WriteLine("Guess a letter: ");
                string inputLetter = Console.ReadLine()?.Trim().ToLower();    
                Console.WriteLine();

                if (string.IsNullOrEmpty(inputLetter))
                {
                    UserEmptyInput();
                    continue;
                    // bawal empty input

                }

                if (inputLetter.Length != 1)
                {
                    ReadOnlySingleChar();
                    continue;
                    // bawal maraming characters

                }

                if (!char.IsLetter(inputLetter[0]))
                {
                    CheckForLetterInput();
                    continue;
                    // bawal numbers at symbols

                }

                char guessedLetter = inputLetter[0];
                bool correctGuess = ProcessGuess(wordToGuess, guessedWord, guessedLetter); // method calling

                if (!correctGuess)
                {
                    playerLives = ValidateUserInput(playerLives); // method calling
                }

                if (new string(guessedWord) == wordToGuess)
                {
                    DisplayWinMessage(userName, wordToGuess);
                    return;
                    // prints this pag tugma sa wordToGuess

                }


            }

            ShowLoseMessage(userName, wordToGuess); // method calling
            // prints if the user uses all their lives without guessing the right word           

        }

        public static string GetUserName(string displayText)
        {
            string userName;
            do
            {
                Console.Write(displayText);
                userName = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(userName))
                {
                    Console.WriteLine("Username CANNOT be empty! Please try again.");
                    Console.WriteLine();

                }
            }
            while (string.IsNullOrEmpty(userName));

            return userName;
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

        public static bool ProcessGuess(string wordToGuess, char[] guessedWord, char guessedLetter)
        {
            bool correctGuess = false;

            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (wordToGuess[i] == guessedLetter)
                {
                    guessedWord[i] = guessedLetter;
                    correctGuess = true;
                    // prints the correct corresponding letter and its position from the wordToGuess
                }
            }

            return correctGuess;
        }

        public static void UserEmptyInput()
        {
            Console.WriteLine("Your input CANNOT be empty! Please enter a letter.");
            Console.WriteLine();
        }

        public static void ReadOnlySingleChar()
        {
            Console.WriteLine("NOT COUNTED! Please enter a single letter only.");
            Console.WriteLine();
        }

        public static void CheckForLetterInput()
        {
            Console.WriteLine("OOPS, only letters are acceptable! Any symbols and numbers are not allowed.");
            Console.WriteLine();
        }

        public static int ValidateUserInput(int playerLives)
        {
            playerLives--;
            if (playerLives > 0)
            {
                Console.WriteLine("INCORRECT GUESS! Please try again.");
                Console.WriteLine();
                // print this pag mali input 

            }
            return playerLives;
        }

        public static void DisplayWinMessage(string userName, string wordToGuess)
        {
            Console.WriteLine($"CONGRATULATIONS, {userName}! You won!!!");
            Console.WriteLine($"The word was: {wordToGuess}");
        }

        public static void ShowLoseMessage(string userName, string wordToGuess)
        {
            Console.WriteLine($"GAME OVER! The word was: {wordToGuess}");
            Console.WriteLine($"Better luck next time, {userName}.");
        }


    }
}
