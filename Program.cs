
using System;
using GuessingGame_BusinessLogic;

namespace GuessingWordGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            DisplayWelcomeMessage(); // method calling

            string userName = GetUserName("Before we start, please enter your preferred username: "); // method calling
            Console.WriteLine($"\nHello, {userName}! Let's start the game!");
            Console.WriteLine();

            string[] wordsToGuess = GuessingGameProcess.GetWords(); // business logic
            int totalLevel = GuessingGameProcess.GetTotalLevel(); // business logic
            int points = GuessingGameProcess.GetPoints(); // business logic

            for (int level = 0; level < totalLevel; level++)
            {
                Console.WriteLine($"LEVEL {level + 1}/{totalLevel}");

                int playerLives = 3;
                
                string wordToGuess = wordsToGuess[level];
                bool guessedCorrectly = false;      

                while (!guessedCorrectly)
                {
                    guessedCorrectly = PlayLevel(userName, wordToGuess, ref playerLives); // method calling

                    if (!guessedCorrectly)
                    {
                        Console.Write("\nDo you want to try again? (yes/no): ");
                        string tryAgain = GetYesNoInput();
                        Console.WriteLine();
                        
                        
                        if (tryAgain == "no")
                        {
                            if (level == 0)
                            {
                                ExitOnFirstTry(points, userName); // method calling
                                return;
                            }
                            else
                            {
                                ExitMidGameLosing(points); // method calling
                                return;
                            }
                        }
  
                        playerLives = 3; // Reset lives if they choose to try again
                    }
                }

                if (guessedCorrectly)
                {
                    if (!HandleLevelProgression(ref points, level, totalLevel))
                    {
                        return; // Exit the game if the player chooses not to continue
                    }
                }
            }
           
            EndGameMessage(userName); // Game completed successfully
        }



        static void DisplayWelcomeMessage()
        {
            Console.WriteLine("Welcome to my C# version of Hangman. Hope you like it!");
            Console.WriteLine("_______________________________________________________" + Environment.NewLine);
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
                    Console.WriteLine("Username CANNOT be empty! Please try again.\n");
                }
            }
            while (string.IsNullOrEmpty(userName));

            return userName;
        }

        public static bool PlayLevel(string userName, string wordToGuess, ref int playerLives)
        {
            char[] guessedWord = GuessingGameProcess.InitializeGuessedWord(wordToGuess);

            while (playerLives > 0)
            {
                DisplayLevelInfo(playerLives, guessedWord);

                char guessedLetter = GetValidGuess();
                bool correctGuess = GuessingGameProcess.ProcessPlayerGuess(wordToGuess, guessedWord, guessedLetter, ref playerLives);

                if (GuessingGameProcess.IsWordGuessed(guessedWord, wordToGuess))
                {
                    return CheckLevelResult(guessedWord, wordToGuess);
                }

                if (playerLives > 0)
                {
                    HandleGuessFeedback(correctGuess);
                }
            }

            return CheckLevelResult(guessedWord, wordToGuess);
        }

        public static void HandleGuessFeedback(bool correctGuess)
        {
            if (correctGuess)
            {
                Console.WriteLine("Correct guess! Keep going!\n");
            }
            else
            {
                Console.WriteLine("INCORRECT GUESS! Please try again.\n");
            }
        }

        public static bool CheckLevelResult(char[] guessedWord, string wordToGuess)
        {
            if (GuessingGameProcess.IsWordGuessed(guessedWord, wordToGuess))
            {
                Console.WriteLine($"Congratulations! You guessed the word: {wordToGuess}");
                return true;
            }

            Console.WriteLine("Sorry, you ran out of lives!");
            return false;  
        }

        public static void DisplayLevelInfo(int playerLives, char[] guessedWord)
        {
            Console.WriteLine($"Your lives remaining: {playerLives} | Points: {GuessingGameProcess.GetPoints()}");
            Console.WriteLine($"Word to guess: {new string(guessedWord)}\n");
        }

        public static char GetValidGuess()
        {
            while (true)
            {
                Console.Write("Guess a letter: ");
                string inputLetter = Console.ReadLine()?.Trim().ToLower();
                Console.WriteLine();

                if (string.IsNullOrEmpty(inputLetter))
                {
                    UserEmptyInput(); // method calling
                    continue;
                }

                if (inputLetter.Length != 1)
                {
                    ReadOnlySingleChar(); // method calling
                    continue;
                }

                if (!char.IsLetter(inputLetter[0]))
                {
                    CheckForLetterInput(); // method calling
                    continue;
                }

                return inputLetter[0];
            }
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

        public static string GetYesNoInput()
        {
            while (true)
            {
                string input = Console.ReadLine()?.Trim().ToLower();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input cannot be empty! Please enter 'yes' or 'no'.");
                    Console.WriteLine();
                    continue;
                }

                if (input == "yes" || input == "no")
                {
                    return input;
                }

                Console.WriteLine("Invalid input! Please enter only 'yes' or 'no'.");
                Console.WriteLine();
            }
        }

        public static void ExitOnFirstTry(int points, string userName)
        {
            Console.WriteLine("You gave up on the first level.");
            Console.WriteLine($"Your total score: {points} points.");
            Console.WriteLine($"Better luck next time, {userName}!");
        }

        public static void ExitMidGameLosing(int points)
        {
            Console.WriteLine("You have decided to end the game midway, but why?");
            Console.WriteLine($"Your total score: {GuessingGameProcess.GetPoints()} points.");
            Console.WriteLine("Remember, winners never quit, and quitters never win. ");
        }

        static bool HandleLevelProgression(ref int points, int level, int totalLevel)
        {
            GuessingGameProcess.UpdatePoints(10);  // Award 10 points for correct guess
            Console.WriteLine($"You earned 10 points! Total points: {GuessingGameProcess.GetPoints()}");
            Console.WriteLine();

            if (level < totalLevel - 1)
            {
                Console.Write("Do you want to continue to the next level? (yes/no): ");
                string continueGame = GetYesNoInput(); // method calling
                Console.WriteLine();

                if (continueGame == "no")
                {
                    Console.WriteLine($"Your total score: {GuessingGameProcess.GetPoints()} points.");
                    Console.WriteLine("Thanks for playing!");
                    return false;  // End the game
                }
            }
            return true;
        }

        public static void EndGameMessage(string userName)
        {
            Console.WriteLine("====================================================");
            Console.WriteLine($"CONGRATULATIONS, {userName}!");
            Console.WriteLine($"You completed all levels!");
            Console.WriteLine($"Your total score: {GuessingGameProcess.GetPoints()} points.");
            Console.WriteLine("You're a Hangman Master!");
            Console.WriteLine("====================================================");
        }


    }
}